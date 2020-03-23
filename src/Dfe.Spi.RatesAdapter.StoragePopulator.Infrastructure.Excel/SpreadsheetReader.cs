namespace Dfe.Spi.RatesAdapter.StoragePopulator.Infrastructure.Excel
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using CsvHelper;
    using Dfe.Spi.Common.Logging.Definitions;
    using Dfe.Spi.RatesAdapter.StoragePopulator.Domain.Definitions;
    using Dfe.Spi.RatesAdapter.StoragePopulator.Domain.Models.ConfigurationFileModels;
    using Dfe.Spi.RatesAdapter.StoragePopulator.Infrastructure.Excel.Models;
    using ExcelDataReader;
    using DomainModels = Dfe.Spi.RatesAdapter.Domain.Models;

    /// <summary>
    /// Implements <see cref="ISpreadsheetReader" />.
    /// </summary>
    public class SpreadsheetReader : ISpreadsheetReader
    {
        private static Type nullableBool = typeof(bool?);
        private static Type nullableShort = typeof(short?);
        private static Type nullableLong = typeof(long?);

        private readonly ILoggerWrapper loggerWrapper;

        private readonly Type[] allDomainModelTypes;
        private readonly List<ExcelParseFailureLogEntry> excelParseFailureLogEntries;

        /// <summary>
        /// Initialises a new instance of the <see cref="SpreadsheetReader" />
        /// class.
        /// </summary>
        /// <param name="loggerWrapper">
        /// An instance of type <see cref="ILoggerWrapper" />.
        /// </param>
        public SpreadsheetReader(ILoggerWrapper loggerWrapper)
        {
            this.loggerWrapper = loggerWrapper;

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            Type type = typeof(DomainModels.ModelsBase);

            Assembly assembly = type.Assembly;

            this.allDomainModelTypes = assembly.GetTypes();

            this.excelParseFailureLogEntries =
                new List<ExcelParseFailureLogEntry>();
        }

        /// <inheritdoc />
        public Task<IEnumerable<DomainModels.ModelsBase>> ReadAsync(
            ConfigurationFile configurationFile,
            string spreadsheetFile,
            CancellationToken cancellationToken)
        {
            List<DomainModels.ModelsBase> toReturn =
                new List<DomainModels.ModelsBase>();

            if (configurationFile == null)
            {
                throw new ArgumentNullException(nameof(configurationFile));
            }

            FileInfo fileInfo = new FileInfo(spreadsheetFile);

            long firstRow = configurationFile.FirstRow;
            long lastRow = configurationFile.LastRow;

            DomainModels.ModelsBase rowInstance = null;
            using (FileStream fileStream = fileInfo.Open(FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                using (IExcelDataReader excelDataReader = ExcelReaderFactory.CreateReader(fileStream))
                {
                    // Read the spreadsheet into memory and...
                    DataSet dataSet = excelDataReader.AsDataSet();

                    // Select the right 'sheet'...
                    string sheetName = configurationFile.SheetName;
                    DataTable dataTable = dataSet.Tables[sheetName];

                    DataRow dataRow = null;

                    // 2) While we're not on the last row...
                    for (int i = 0; (i < dataTable.Rows.Count) && (i <= lastRow); i++)
                    {
                        dataRow = dataTable.Rows[i];

                        // 1) Go to the first row.
                        if (i >= (firstRow - 1))
                        {
                            rowInstance = this.ReadRowAsync(
                                configurationFile,
                                i,
                                dataRow);

                            toReturn.Add(rowInstance);
                        }
                    }
                }
            }

            this.SaveExcelParseFailureLog();

            return Task.FromResult<IEnumerable<DomainModels.ModelsBase>>(
                toReturn);
        }

        private void SaveExcelParseFailureLog()
        {
            string filename =
                $"{DateTime.Now.ToString("yyyyMMdd-HHmmss", CultureInfo.InvariantCulture)}.csv";

            using (StreamWriter streamWriter = new StreamWriter(filename))
            {
                using (CsvWriter csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
                {
                    csvWriter.WriteRecords(this.excelParseFailureLogEntries);
                }
            }

            this.loggerWrapper.Info($"Log file: \"{filename}\".");
        }

        private DomainModels.ModelsBase ReadRowAsync(
            ConfigurationFile configurationFile,
            int row,
            DataRow dataRow)
        {
            DomainModels.ModelsBase toReturn = null;

            List<DomainModels.ModelsBase> modelParts =
                new List<DomainModels.ModelsBase>();

            // 3) Iterate through the configuration file and...
            DomainModels.ModelsBase modelsBase = null;
            foreach (ColumnMappingConfiguration columnMappingConfiguration in configurationFile.ColumnMappingConfigurations)
            {
                // .. instantiate new instances, based on the requested types.
                modelsBase = this.CreateAndPopulateModelsBaseInstance(
                    columnMappingConfiguration,
                    row,
                    dataRow);

                modelParts.Add(modelsBase);
            }

            // Join the models up, based on the instance type present.
            // It can only be one.
            modelsBase = modelParts
                .SingleOrDefault(x => x is DomainModels.SchoolInformation);

            if (modelsBase != null)
            {
                DomainModels.SchoolInformation schoolInformation =
                    modelsBase as DomainModels.SchoolInformation;

                schoolInformation.BaselineFunding = modelParts
                    .Select(x => x as DomainModels.Rates.BaselineFunding)
                    .Single(x => x != null);

                schoolInformation.IllustrativeFunding = modelParts
                    .Select(x => x as DomainModels.Rates.IllustrativeFunding)
                    .Single(x => x != null);

                schoolInformation.NotionalFunding = modelParts
                    .Select(x => x as DomainModels.Rates.NotionalFunding)
                    .Single(x => x != null);

                toReturn = schoolInformation;
            }
            else
            {
                throw new NotImplementedException(
                    $"No model of type " +
                    $"{nameof(DomainModels.SchoolInformation)} present. Is " +
                    $"there a different high-level entity we need to " +
                    $"implement?");
            }

            return toReturn;
        }

        private DomainModels.ModelsBase CreateAndPopulateModelsBaseInstance(
            ColumnMappingConfiguration columnMappingConfiguration,
            int row,
            DataRow dataRow)
        {
            DomainModels.ModelsBase toReturn = null;

            // 4) Use reflection to cycle through the properties, check if
            //    it's in the configuration, if it is, read from the specified
            //    column. Then set it.
            string modelType = columnMappingConfiguration.ModelType;

            Type type = this.allDomainModelTypes
                .Single(x => x.FullName == modelType);

            toReturn = (DomainModels.ModelsBase)Activator.CreateInstance(type);

            // Now stitch it up.
            Dictionary<string, int> columnMappings =
                columnMappingConfiguration.ColumnMappings;

            PropertyInfo propertyInfo = null;
            object value = null;
            Type actualModelType = null;
            Type actualValueType = null;
            int column;
            foreach (KeyValuePair<string, int> keyValuePair in columnMappings)
            {
                propertyInfo = type.GetProperty(keyValuePair.Key);
                actualModelType = propertyInfo.PropertyType;

                // We're at the mercy of ExcelDataReader with regards to the
                // types that come out. Therefore, we may need to perfrom some
                // parsing. If the types match, we're good, though.
                column = keyValuePair.Value;

                value = dataRow[column];

                actualValueType = value.GetType();

                if (actualModelType != actualValueType)
                {
                    value = this.TryParsingUnboxedValue(
                        actualModelType,
                        row,
                        column,
                        value);
                }
                else
                {
                    // Do nothing - the value is the required type.
                }

                if (value != null)
                {
                    propertyInfo.SetValue(toReturn, value);
                }
            }

            return toReturn;
        }

        private object TryParsingUnboxedValue(
            Type destinationType,
            int row,
            int column,
            object value)
        {
            object toReturn = null;

            string valueStr = value.ToString();

            try
            {
                if (destinationType == nullableLong)
                {
                    toReturn = long.Parse(
                        valueStr,
                        CultureInfo.InvariantCulture);
                }
                else if (destinationType == nullableShort)
                {
                    toReturn = short.Parse(
                        valueStr,
                        CultureInfo.InvariantCulture);
                }
                else if (destinationType == nullableBool)
                {
                    // We want to return null if the value is empty.
                    if (!string.IsNullOrEmpty(valueStr))
                    {
                        // Otherwise..
                        toReturn = valueStr == "Yes";
                    }
                }
                else
                {
                    throw new NotImplementedException(
                        $"Please implement type {destinationType.Name}.");
                }
            }
            catch (FormatException)
            {
                // Because in Excel, indexes start at 1.
                row++;
                column++;

                this.loggerWrapper.Warning(
                    $"Row #{row}, Column #{column}: Attempted to parse " +
                    $"\"{valueStr}\" to a {destinationType.Name}, but was " +
                    $"unable to.");

                ExcelParseFailureLogEntry excelParseFailureLogEntry =
                    new ExcelParseFailureLogEntry()
                    {
                        Row = row,
                        Column = column,
                        RequiredType = destinationType.FullName,
                        StringValue = valueStr,
                    };

                this.excelParseFailureLogEntries.Add(
                    excelParseFailureLogEntry);
            }

            return toReturn;
        }
    }
}
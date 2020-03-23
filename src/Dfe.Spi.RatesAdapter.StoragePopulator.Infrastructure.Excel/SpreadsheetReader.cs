namespace Dfe.Spi.RatesAdapter.StoragePopulator.Infrastructure.Excel
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Dfe.Spi.RatesAdapter.StoragePopulator.Domain.Definitions;
    using Dfe.Spi.RatesAdapter.StoragePopulator.Domain.Models.ConfigurationFileModels;
    using ExcelDataReader;
    using DomainModels = Dfe.Spi.RatesAdapter.Domain.Models;

    /// <summary>
    /// Implements <see cref="ISpreadsheetReader" />.
    /// </summary>
    public class SpreadsheetReader : ISpreadsheetReader
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="SpreadsheetReader" />
        /// class.
        /// </summary>
        public SpreadsheetReader()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<DomainModels.ModelsBase>> ReadAsync(
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

            IEnumerable<DomainModels.ModelsBase> rowInstances = null;
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
                            rowInstances = await this.ReadRowAsync(dataRow)
                                .ConfigureAwait(false);

                            toReturn.AddRange(rowInstances);
                        }
                    }
                }
            }

            return toReturn;
        }

        private async Task<IEnumerable<DomainModels.ModelsBase>> ReadRowAsync(
            DataRow dataRow)
        {
            List<DomainModels.ModelsBase> toReturn =
                new List<DomainModels.ModelsBase>();

            // TODO:
            // 3) Iterate through the configuration file and
            //      instantiate new instances, based on the requested types.
            // 4) Use reflection to cycle through the properties, check if
            //    it's in the configuration, if it is, read from the specified
            //    column. Then set it.
            return toReturn;
        }
    }
}
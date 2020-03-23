namespace Dfe.Spi.RatesAdapter.StoragePopulator.Application
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Dfe.Spi.Common.Logging.Definitions;
    using Dfe.Spi.RatesAdapter.Domain.Definitions;
    using Dfe.Spi.RatesAdapter.StoragePopulator.Application.Definitions;
    using Dfe.Spi.RatesAdapter.StoragePopulator.Application.Definitions.Processors;
    using Dfe.Spi.RatesAdapter.StoragePopulator.Application.Models.SpreadsheetProcessorModels;
    using Dfe.Spi.RatesAdapter.StoragePopulator.Domain.Definitions;
    using Dfe.Spi.RatesAdapter.StoragePopulator.Domain.Models.ConfigurationFileModels;
    using DomainModels = Dfe.Spi.RatesAdapter.Domain.Models;

    /// <summary>
    /// Implements <see cref="ISpreadsheetProcessor" />.
    /// </summary>
    public class SpreadsheetProcessor : ISpreadsheetProcessor
    {
        private readonly IConfigurationFileReader configurationFileReader;
        private readonly ILoggerWrapper loggerWrapper;
        private readonly ISchoolInformationStorageAdapter schoolInformationStorageAdapter;
        private readonly ISpreadsheetReader spreadsheetReader;

        /// <summary>
        /// Initialises a new instance of the
        /// <see cref="SpreadsheetProcessor" /> class.
        /// </summary>
        /// <param name="configurationFileReader">
        /// An instance of type <see cref="IConfigurationFileReader" />.
        /// </param>
        /// <param name="loggerWrapper">
        /// An instance of type <see cref="ILoggerWrapper" />.
        /// </param>
        /// <param name="schoolInformationStorageAdapter">
        /// An instance of type
        /// <see cref="ISchoolInformationStorageAdapter" />.
        /// </param>
        /// <param name="spreadsheetReader">
        /// An instance of type <see cref="ISpreadsheetReader" />.
        /// </param>
        public SpreadsheetProcessor(
            IConfigurationFileReader configurationFileReader,
            ILoggerWrapper loggerWrapper,
            ISchoolInformationStorageAdapter schoolInformationStorageAdapter,
            ISpreadsheetReader spreadsheetReader)
        {
            this.configurationFileReader = configurationFileReader;
            this.loggerWrapper = loggerWrapper;
            this.schoolInformationStorageAdapter = schoolInformationStorageAdapter;
            this.spreadsheetReader = spreadsheetReader;
        }

        /// <inheritdoc />
        public async Task<ProcessResponse> ProcessAsync(
            ProcessRequest processRequest,
            CancellationToken cancellationToken)
        {
            ProcessResponse toReturn = null;

            if (processRequest == null)
            {
                throw new ArgumentNullException(nameof(processRequest));
            }

            // 1) Deserialise config.
            string configFile = processRequest.ConfigFile;

            ConfigurationFile configurationFile =
                await this.configurationFileReader.ReadAsync(
                    configFile,
                    cancellationToken)
                    .ConfigureAwait(false);

            // 2) Take mappings and call spreadsheet parser.
            string spreadsheetFile = processRequest.SpreadsheetFile;

            IEnumerable<DomainModels.ModelsBase> modelsBases =
                await this.spreadsheetReader.ReadAsync(
                    configurationFile,
                    spreadsheetFile,
                    cancellationToken)
                    .ConfigureAwait(false);

            // 3) Take each entity and store in storage.
            // Clear storage first.
            await this.schoolInformationStorageAdapter
                .ClearStorageAsync(cancellationToken)
                .ConfigureAwait(false);

            DomainModels.ModelsBase[] modelsBaseArray = modelsBases.ToArray();

            int length = modelsBaseArray.Length;

            DomainModels.ModelsBase modelsBase = null;
            DomainModels.SchoolInformation schoolInformation = null;
            for (int i = 0; i < length; i++)
            {
                modelsBase = modelsBaseArray[i];

                if (modelsBase is DomainModels.SchoolInformation)
                {
                    schoolInformation =
                        modelsBase as DomainModels.SchoolInformation;

                    if (schoolInformation.Urn.HasValue)
                    {
                        await this.schoolInformationStorageAdapter.CreateAsync(
                            schoolInformation,
                            cancellationToken)
                            .ConfigureAwait(false);

                        this.loggerWrapper.Info(
                            $"Created " +
                            $"{nameof(DomainModels.SchoolInformation)} " +
                            $"{i}/{length}.");

                        decimal percentage = (decimal)i / (decimal)length;

                        percentage *= 100;

                        Console.Title =
                            $"Progress: {i}/{length} " +
                            $"({percentage.ToString("#.##", CultureInfo.InvariantCulture)}%)...";
                    }
                    else
                    {
                        this.loggerWrapper.Warning(
                            $"A {nameof(DomainModels.SchoolInformation)} " +
                            $"instance could not be inserted, as there " +
                            $"wasn't a URN.");
                    }
                }
                else
                {
                    throw new NotImplementedException(
                        "Storage not configured for this type. Please " +
                        "implement.");
                }
            }

            return toReturn;
        }
    }
}
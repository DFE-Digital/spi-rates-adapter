namespace Dfe.Spi.RatesAdapter.StoragePopulator.Application
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Dfe.Spi.RatesAdapter.StoragePopulator.Application.Definitions;
    using Dfe.Spi.RatesAdapter.StoragePopulator.Application.Definitions.Processors;
    using Dfe.Spi.RatesAdapter.StoragePopulator.Application.Models.SpreadsheetProcessorModels;
    using Dfe.Spi.RatesAdapter.StoragePopulator.Domain.Definitions;
    using Dfe.Spi.RatesAdapter.StoragePopulator.Domain.Models.ConfigurationFileModels;

    /// <summary>
    /// Implements <see cref="ISpreadsheetProcessor" />.
    /// </summary>
    public class SpreadsheetProcessor : ISpreadsheetProcessor
    {
        private readonly IConfigurationFileReader configurationFileReader;
        private readonly ISpreadsheetReader spreadsheetReader;

        /// <summary>
        /// Initialises a new instance of the
        /// <see cref="SpreadsheetProcessor" /> class.
        /// </summary>
        /// <param name="configurationFileReader">
        /// An instance of type <see cref="IConfigurationFileReader" />.
        /// </param>
        /// <param name="spreadsheetReader">
        /// An instance of type <see cref="ISpreadsheetReader" />.
        /// </param>
        public SpreadsheetProcessor(
            IConfigurationFileReader configurationFileReader,
            ISpreadsheetReader spreadsheetReader)
        {
            this.configurationFileReader = configurationFileReader;
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

            IEnumerable<RatesAdapter.Domain.Models.ModelsBase> modelsBases =
                await this.spreadsheetReader.ReadAsync(
                    configurationFile,
                    spreadsheetFile,
                    cancellationToken)
                    .ConfigureAwait(false);

            // TODO:
            // 3) Take each entity and store in storage.
            return toReturn;
        }
    }
}
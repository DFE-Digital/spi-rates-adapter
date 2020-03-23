namespace Dfe.Spi.RatesAdapter.StoragePopulator.Application
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Dfe.Spi.RatesAdapter.StoragePopulator.Application.Definitions;
    using Dfe.Spi.RatesAdapter.StoragePopulator.Application.Definitions.Processors;
    using Dfe.Spi.RatesAdapter.StoragePopulator.Application.Models.ConfigurationFileModels;
    using Dfe.Spi.RatesAdapter.StoragePopulator.Application.Models.SpreadsheetProcessorModels;

    /// <summary>
    /// Implements <see cref="ISpreadsheetProcessor" />.
    /// </summary>
    public class SpreadsheetProcessor : ISpreadsheetProcessor
    {
        private readonly IConfigurationFileReader configurationFileReader;

        /// <summary>
        /// Initialises a new instance of the
        /// <see cref="SpreadsheetProcessor" /> class.
        /// </summary>
        /// <param name="configurationFileReader">
        /// An instance of type <see cref="IConfigurationFileReader" />.
        /// </param>
        public SpreadsheetProcessor(
            IConfigurationFileReader configurationFileReader)
        {
            this.configurationFileReader = configurationFileReader;
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

            // TODO:
            // 2) Take mappings and call spreadsheet parser.
            // 3) Take each entity and store in storage.
            return toReturn;
        }
    }
}
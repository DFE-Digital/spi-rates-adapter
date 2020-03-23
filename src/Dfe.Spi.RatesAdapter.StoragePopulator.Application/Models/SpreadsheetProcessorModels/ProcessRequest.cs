namespace Dfe.Spi.RatesAdapter.StoragePopulator.Application.Models.SpreadsheetProcessorModels
{
    using System.Threading;
    using Dfe.Spi.RatesAdapter.StoragePopulator.Application.Definitions.Processors;

    /// <summary>
    /// Request object for the
    /// <see cref="ISpreadsheetProcessor.ProcessAsync(ProcessRequest, CancellationToken)" />
    /// method.
    /// </summary>
    public class ProcessRequest : ModelsBase
    {
        /// <summary>
        /// Gets or sets the storage connection string.
        /// </summary>
        public string StorageConnectionString
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the table name in which to populate.
        /// </summary>
        public string TableName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a path to the spreadsheet file.
        /// </summary>
        public string SpreadsheetFile
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a path to the config file.
        /// </summary>
        public string ConfigFile
        {
            get;
            set;
        }
    }
}
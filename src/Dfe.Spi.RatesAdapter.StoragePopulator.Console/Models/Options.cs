namespace Dfe.Spi.RatesAdapter.StoragePopulator.ConsoleApp.Models
{
    using System.Diagnostics.CodeAnalysis;
    using CommandLine;

    /// <summary>
    /// The options class, as used by the <see cref="CommandLine" />.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Options : ModelsBase
    {
        /// <summary>
        /// Gets or sets the storage connection string.
        /// </summary>
        [Option(
            "storage-connection-string",
            Required = true,
            HelpText = "The storage connection string.")]
        public string StorageConnectionString
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the table name in which to populate.
        /// </summary>
        [Option(
            "table-name",
            Required = true,
            HelpText = "The table name in which to populate.")]
        public string TableName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a path to the spreadsheet file.
        /// </summary>
        [Option(
            "spreadsheet-file",
            Required = true,
            HelpText = "A path to the spreadsheet file.")]
        public string SpreadsheetFile
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a path to the config file.
        /// </summary>
        [Option(
            "config-file",
            Required = true,
            HelpText = "A path to the config file.")]
        public string ConfigFile
        {
            get;
            set;
        }
    }
}
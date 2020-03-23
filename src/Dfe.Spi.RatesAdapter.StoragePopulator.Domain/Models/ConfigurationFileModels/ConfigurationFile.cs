namespace Dfe.Spi.RatesAdapter.StoragePopulator.Domain.Models.ConfigurationFileModels
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents a configuration file, containing rules on how to parse a
    /// spreadsheet.
    /// </summary>
    public class ConfigurationFile : ModelsBase
    {
        /// <summary>
        /// Gets or sets the year of the spreadsheet.
        /// </summary>
        public int Year
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the row in which to begin reading from.
        /// </summary>
        public long FirstRow
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the row in which to finish reading from.
        /// </summary>
        public long LastRow
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the sheet name in which to read from.
        /// </summary>
        public string SheetName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a set of <see cref="ColumnMappingConfiguration" />
        /// instances.
        /// </summary>
        public IEnumerable<ColumnMappingConfiguration> ColumnMappingConfigurations
        {
            get;
            set;
        }
    }
}
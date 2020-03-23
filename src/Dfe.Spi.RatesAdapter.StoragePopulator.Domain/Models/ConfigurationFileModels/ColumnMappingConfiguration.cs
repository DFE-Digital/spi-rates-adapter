namespace Dfe.Spi.RatesAdapter.StoragePopulator.Domain.Models.ConfigurationFileModels
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Represents column mapping configuration, containing detail on how to
    /// map columns in the spreadsheet to our own local models.
    /// </summary>
    public class ColumnMappingConfiguration : ModelsBase
    {
        /// <summary>
        /// Gets or sets the model type, as a string. This should be fully
        /// namespaced.
        /// </summary>
        public string ModelType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets how the properties in <see cref="ModelType" />
        /// map up to which columns in the spreadsheet, with the key being the
        /// property name, and the value being the column identifier.
        /// </summary>
        [SuppressMessage(
            "Microsoft.Usage",
            "CA2227",
            Justification = "This is a DTO.")]
        public Dictionary<string, int> ColumnMappings
        {
            get;
            set;
        }
    }
}
namespace Dfe.Spi.RatesAdapter.StoragePopulator.Infrastructure.Excel.Models
{
    /// <summary>
    /// Represents a parsing error for a particular cell/value.
    /// </summary>
    public class ExcelParseFailureLogEntry : ModelsBase
    {
        /// <summary>
        /// Gets or sets the row.
        /// </summary>
        public int Row
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the column.
        /// </summary>
        public int Column
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type that was required, according to the model.
        /// </summary>
        public string RequiredType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the value that failed to parse.
        /// </summary>
        public string StringValue
        {
            get;
            set;
        }
    }
}
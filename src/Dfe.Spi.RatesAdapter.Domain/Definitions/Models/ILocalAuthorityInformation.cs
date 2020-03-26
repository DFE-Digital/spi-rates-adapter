namespace Dfe.Spi.RatesAdapter.Domain.Definitions.Models
{
    /// <summary>
    /// Describes the properties of the local authority information model.
    /// </summary>
    public interface ILocalAuthorityInformation
    {
        /// <summary>
        /// Gets or sets the <c>LaNumber</c> value.
        /// Spreadsheets: 2018, 2019, 2020.
        /// </summary>
        short? LaNumber
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>LaName</c> value.
        /// Spreadsheets: 2018, 2019, 2020.
        /// </summary>
        string LaName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>Region</c> value.
        /// Spreadsheets: 2018, 2019, 2020.
        /// </summary>
        string Region
        {
            get;
            set;
        }
    }
}
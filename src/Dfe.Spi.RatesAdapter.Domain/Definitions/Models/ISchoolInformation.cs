namespace Dfe.Spi.RatesAdapter.Domain.Definitions.Models
{
    /// <summary>
    /// Describes the properties of a school information model.
    /// </summary>
    public interface ISchoolInformation
    {
        /// <summary>
        /// Gets or sets the Urn.
        /// Spreadsheets: 2018, 2019, 2020.
        /// </summary>
        long? Urn
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

        /// <summary>
        /// Gets or sets the <c>LaEstab</c> value.
        /// Spreadsheets: 2018, 2019, 2020.
        /// </summary>
        long? LaEstab
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
        /// Gets or sets the <c>LaNumber</c> value.
        /// Spreadsheets: 2018, 2019, 2020.
        /// </summary>
        short? LaNumber
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>NewAndGrowing</c> value.
        /// Spreadsheets: 2018, 2019.
        /// </summary>
        bool? NewAndGrowing
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>PartiallyOpen</c> value.
        /// Spreadsheets: 2018, 2019, 2020.
        /// </summary>
        bool? PartiallyOpen
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>Phase</c> value.
        /// Spreadsheets: 2018, 2019, 2020.
        /// </summary>
        string Phase
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>SchoolName</c> value.
        /// Spreadsheets: 2018, 2019, 2020.
        /// </summary>
        string SchoolName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>TheoreticalBaselineExists</c> value.
        /// Spreadsheets: 2020.
        /// </summary>
        bool? TheoreticalBaselineExists
        {
            get;
            set;
        }
    }
}
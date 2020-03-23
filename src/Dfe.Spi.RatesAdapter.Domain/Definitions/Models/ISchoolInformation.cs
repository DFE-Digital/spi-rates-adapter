namespace Dfe.Spi.RatesAdapter.Domain.Definitions.Models
{
    /// <summary>
    /// Describes the properties of a school information model.
    /// </summary>
    public interface ISchoolInformation
    {
        /// <summary>
        /// Gets or sets the urn.
        /// </summary>
        long? Urn
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>Region</c> value.
        /// </summary>
        string Region
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>LaEstab</c> value.
        /// </summary>
        long? LaEstab
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>LaName</c> value.
        /// </summary>
        string LaName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>LaNumber</c> value.
        /// </summary>
        short? LaNumber
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>NewAndGrowing</c> value.
        /// </summary>
        bool? NewAndGrowing
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>PartiallyOpen</c> value.
        /// </summary>
        bool? PartiallyOpen
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>Phase</c> value.
        /// </summary>
        string Phase
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>SchoolName</c> value.
        /// </summary>
        string SchoolName
        {
            get;
            set;
        }
    }
}
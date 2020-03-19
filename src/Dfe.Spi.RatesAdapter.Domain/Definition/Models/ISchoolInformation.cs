namespace Dfe.Spi.RatesAdapter.Domain.Definition.Models
{
    /// <summary>
    /// Describes the properties of a school information model.
    /// </summary>
    public interface ISchoolInformation
    {
        /// <summary>
        /// Gets or sets the <c>Region</c> value.
        /// </summary>
        string Region
        {
            get;
            set;
        }
    }
}
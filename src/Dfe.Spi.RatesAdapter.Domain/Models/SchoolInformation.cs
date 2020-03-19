namespace Dfe.Spi.RatesAdapter.Domain.Models
{
    using Dfe.Spi.RatesAdapter.Domain.Definition.Models;
    using Dfe.Spi.RatesAdapter.Domain.Models.Rates;

    /// <summary>
    /// Represents school information.
    /// </summary>
    public class SchoolInformation : ModelsBase, ISchoolInformation
    {
        /// <summary>
        /// Gets or sets the region.
        /// </summary>
        public string Region
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets an instance of <see cref="Rates.BaselineFunding" />.
        /// </summary>
        public BaselineFunding BaselineFunding
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets an instance of <see cref="Rates.NotionalFunding" />.
        /// </summary>
        public NotionalFunding NotionalFunding
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets an instance of
        /// <see cref="Rates.IllustrativeFunding" />.
        /// </summary>
        public IllustrativeFunding IllustrativeFunding
        {
            get;
            set;
        }
    }
}
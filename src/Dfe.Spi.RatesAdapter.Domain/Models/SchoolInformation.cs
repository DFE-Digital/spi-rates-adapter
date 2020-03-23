namespace Dfe.Spi.RatesAdapter.Domain.Models
{
    using Dfe.Spi.RatesAdapter.Domain.Definitions.Models;
    using Dfe.Spi.RatesAdapter.Domain.Models.Rates;

    /// <summary>
    /// Represents school information.
    /// </summary>
    public class SchoolInformation : ModelsBase, ISchoolInformation
    {
        /// <inheritdoc />
        public long? Urn
        {
            get;
            set;
        }

        /// <inheritdoc />
        public string Region
        {
            get;
            set;
        }

        /// <inheritdoc />
        public long? LaEstab
        {
            get;
            set;
        }

        /// <inheritdoc />
        public string LaName
        {
            get;
            set;
        }

        /// <inheritdoc />
        public short? LaNumber
        {
            get;
            set;
        }

        /// <inheritdoc />
        public bool? NewAndGrowing
        {
            get;
            set;
        }

        /// <inheritdoc />
        public bool? PartiallyOpen
        {
            get;
            set;
        }

        /// <inheritdoc />
        public string Phase
        {
            get;
            set;
        }

        /// <inheritdoc />
        public string SchoolName
        {
            get;
            set;
        }

        /// <inheritdoc />
        public bool? TheoreticalBaselineExists
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
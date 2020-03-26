namespace Dfe.Spi.RatesAdapter.Domain.Models
{
    using Dfe.Spi.RatesAdapter.Domain.Definitions.Models;
    using Dfe.Spi.RatesAdapter.Domain.Models.Rates;

    /// <summary>
    /// Implements <see cref="ILocalAuthorityInformation" />.
    /// </summary>
    public class LocalAuthorityInformation
        : ModelsBase, ILocalAuthorityInformation
    {
        /// <inheritdoc />
        public short? LaNumber
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
        public string Region
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets an instance of
        /// <see cref="Rates.ProvisionalFunding" />.
        /// </summary>
        public ProvisionalFunding ProvisionalFunding
        {
            get;
            set;
        }
    }
}
namespace Dfe.Spi.RatesAdapter.Domain.Models
{
    using Dfe.Spi.RatesAdapter.Domain.Definitions.Models;

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
    }
}
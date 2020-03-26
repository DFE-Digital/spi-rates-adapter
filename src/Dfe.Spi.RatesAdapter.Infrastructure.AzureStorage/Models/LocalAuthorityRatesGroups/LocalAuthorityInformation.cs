namespace Dfe.Spi.RatesAdapter.Infrastructure.AzureStorage.Models.LocalAuthorityRatesGroups
{
    using Dfe.Spi.RatesAdapter.Domain.Definitions.Models;
    using Microsoft.WindowsAzure.Storage.Table;

    /// <summary>
    /// Implements <see cref="ILocalAuthorityInformation" />.
    /// </summary>
    public class LocalAuthorityInformation
        : LocalAuthorityRatesGroupsBase, ILocalAuthorityInformation
    {
        /// <inheritdoc />
        [IgnoreProperty]
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
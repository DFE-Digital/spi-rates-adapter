namespace Dfe.Spi.RatesAdapter.Infrastructure.AzureStorage.Models.SchoolRatesGroups
{
    using Dfe.Spi.RatesAdapter.Domain.Definitions.Models.Rates;

    /// <summary>
    /// Implements <see cref="INotionalFunding" />.
    /// </summary>
    public class NotionalFunding : SchoolRatesGroupsBase, INotionalFunding
    {
        /// <inheritdoc />
        public long? NotionalTotalNffFunding
        {
            get;
            set;
        }
    }
}

namespace Dfe.Spi.RatesAdapter.Infrastructure.AzureStorage.Models.SchoolRatesGroups
{
    using Dfe.Spi.RatesAdapter.Domain.Definition.Models.Rates;

    /// <summary>
    /// Implements <see cref="INotionalFunding" />.
    /// </summary>
    public class NotionalFunding : SchoolRatesGroupsBase, INotionalFunding
    {
        /// <inheritdoc />
        public decimal NotionalTotalNFFFunding
        {
            get;
            set;
        }
    }
}

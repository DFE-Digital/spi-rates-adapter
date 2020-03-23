namespace Dfe.Spi.RatesAdapter.Infrastructure.AzureStorage.Models.SchoolRatesGroups
{
    using Dfe.Spi.RatesAdapter.Domain.Definition.Models.Rates;

    /// <summary>
    /// Implements <see cref="IBaselineFunding" />.
    /// </summary>
    public class BaselineFunding : SchoolRatesGroupsBase, IBaselineFunding
    {
        /// <inheritdoc />
        public long? Value
        {
            get;
            set;
        }
    }
}
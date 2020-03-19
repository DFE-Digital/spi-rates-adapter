namespace Dfe.Spi.RatesAdapter.Infrastructure.AzureStorage.Models.SchoolRatesGroups
{
    using Dfe.Spi.RatesAdapter.Domain.Definition.Models.Rates;

    /// <summary>
    /// Implements <see cref="IIllustrativeFunding" />.
    /// </summary>
    public class IllustrativeFunding
        : SchoolRatesGroupsBase, IIllustrativeFunding
    {
        /// <inheritdoc />
        public decimal IllustrativeTotalNFFFunding
        {
            get;
            set;
        }
    }
}
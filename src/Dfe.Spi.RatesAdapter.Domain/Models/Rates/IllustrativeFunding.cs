namespace Dfe.Spi.RatesAdapter.Domain.Models.Rates
{
    using Dfe.Spi.RatesAdapter.Domain.Definition.Models.Rates;

    /// <summary>
    /// Implements <see cref="IIllustrativeFunding" />.
    /// </summary>
    public class IllustrativeFunding : RatesBase, IIllustrativeFunding
    {
        /// <inheritdoc />
        public decimal IllustrativeTotalNFFFunding
        {
            get;
            set;
        }
    }
}

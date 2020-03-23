namespace Dfe.Spi.RatesAdapter.Domain.Models.Rates
{
    using Dfe.Spi.RatesAdapter.Domain.Definitions.Models.Rates;

    /// <summary>
    /// Implements <see cref="IIllustrativeFunding" />.
    /// </summary>
    public class IllustrativeFunding : RatesBase, IIllustrativeFunding
    {
        /// <inheritdoc />
        public long? IllustrativeTotalNffFunding
        {
            get;
            set;
        }
    }
}

namespace Dfe.Spi.RatesAdapter.Domain.Models.Rates
{
    using Dfe.Spi.RatesAdapter.Domain.Definition.Models.Rates;

    /// <summary>
    /// Implements <see cref="IBaselineFunding" />.
    /// </summary>
    public class BaselineFunding : RatesBase, IBaselineFunding
    {
        /// <inheritdoc />
        public decimal Value
        {
            get;
            set;
        }
    }
}
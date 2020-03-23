namespace Dfe.Spi.RatesAdapter.Domain.Models.Rates
{
    using Dfe.Spi.RatesAdapter.Domain.Definitions.Models.Rates;

    /// <summary>
    /// Implements <see cref="IBaselineFunding" />.
    /// </summary>
    public class BaselineFunding : RatesBase, IBaselineFunding
    {
        /// <inheritdoc />
        public long? Value
        {
            get;
            set;
        }
    }
}
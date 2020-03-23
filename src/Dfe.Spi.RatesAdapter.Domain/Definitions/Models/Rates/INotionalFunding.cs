namespace Dfe.Spi.RatesAdapter.Domain.Definitions.Models.Rates
{
    /// <summary>
    /// Describes the properties of a notional funding model.
    /// </summary>
    public interface INotionalFunding
    {
        /// <summary>
        /// Gets or sets the <c>NotionalTotalNFFFunding</c> value.
        /// </summary>
        long? NotionalTotalNffFunding
        {
            get;
            set;
        }
    }
}
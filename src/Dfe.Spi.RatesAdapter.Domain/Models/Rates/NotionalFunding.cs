﻿namespace Dfe.Spi.RatesAdapter.Domain.Models.Rates
{
    using Dfe.Spi.RatesAdapter.Domain.Definition.Models.Rates;

    /// <summary>
    /// Implements <see cref="INotionalFunding" />.
    /// </summary>
    public class NotionalFunding : RatesBase, INotionalFunding
    {
        /// <inheritdoc />
        public long NotionalTotalNFFFunding
        {
            get;
            set;
        }
    }
}
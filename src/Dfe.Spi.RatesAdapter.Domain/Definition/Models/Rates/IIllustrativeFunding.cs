﻿namespace Dfe.Spi.RatesAdapter.Domain.Definition.Models.Rates
{
    /// <summary>
    /// Describes the properties of a illustrative funding model.
    /// </summary>
    public interface IIllustrativeFunding
    {
        /// <summary>
        /// Gets or sets the <c>IllustrativeTotalNFFFunding</c> value.
        /// </summary>
        long? IllustrativeTotalNffFunding
        {
            get;
            set;
        }
    }
}
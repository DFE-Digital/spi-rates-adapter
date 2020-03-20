﻿namespace Dfe.Spi.RatesAdapter.Domain.Definition.Models.Rates
{
    /// <summary>
    /// Describes the properties of a baseline funding model.
    /// </summary>
    public interface IBaselineFunding
    {
        /// <summary>
        /// Gets or sets the <c>BaselineFunding</c> value.
        /// </summary>
        long Value
        {
            get;
            set;
        }
    }
}
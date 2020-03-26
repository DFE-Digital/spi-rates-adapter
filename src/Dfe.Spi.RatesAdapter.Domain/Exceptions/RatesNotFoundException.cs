namespace Dfe.Spi.RatesAdapter.Domain.Exceptions
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Thrown when a requested rate is requested, but it does not exist.
    /// </summary>
    [SuppressMessage(
        "Microsoft.Design",
        "CA1032",
        Justification = "Not a public library.")]
    public class RatesNotFoundException : Exception
    {
        /// <summary>
        /// Initialises a new instance of the
        /// <see cref="RatesNotFoundException" /> class.
        /// </summary>
        public RatesNotFoundException()
        {
            // Nothing.
        }
    }
}
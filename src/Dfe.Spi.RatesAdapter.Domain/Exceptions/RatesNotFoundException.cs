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
        /// <param name="identifierName">
        /// The name of the identifier used.
        /// </param>
        /// <param name="identifierValue">
        /// The value of the identifier used.
        /// </param>
        public RatesNotFoundException(
            string identifierName,
            object identifierValue)
        {
            this.IdentifierName = identifierName;
            this.IdentifierValue = identifierValue;
        }

        /// <summary>
        /// Gets the name of the identifier used.
        /// </summary>
        public string IdentifierName
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value of the identifier used.
        /// </summary>
        public object IdentifierValue
        {
            get;
            private set;
        }
    }
}
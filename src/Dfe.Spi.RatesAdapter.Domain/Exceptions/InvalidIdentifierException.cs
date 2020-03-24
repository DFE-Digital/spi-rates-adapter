namespace Dfe.Spi.RatesAdapter.Domain.Exceptions
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Thrown when an identifier is supplied, that is not of the required
    /// type.
    /// </summary>
    [SuppressMessage(
        "Microsoft.Design",
        "CA1032",
        Justification = "Not a public library.")]
    public class InvalidIdentifierException : InvalidOperationException
    {
        /// <summary>
        /// Initialises a new instance of the
        /// <see cref="InvalidIdentifierException" /> class.
        /// </summary>
        /// <param name="name">
        /// The name of the identifier.
        /// </param>
        /// <param name="value">
        /// The value of the identifier.
        /// </param>
        /// <param name="expectedType">
        /// The expected type.
        /// </param>
        public InvalidIdentifierException(
            string name,
            string value,
            Type expectedType)
        {
            if (expectedType == null)
            {
                throw new ArgumentNullException(nameof(expectedType));
            }

            this.Name = name;
            this.Value = value;
            this.ExpectedTypeName = expectedType.Name;
        }

        /// <summary>
        /// Gets the name of the identifier.
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the value of the identifier.
        /// </summary>
        public string Value
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the name of the expected type, as a <see cref="string" />.
        /// </summary>
        public string ExpectedTypeName
        {
            get;
            private set;
        }
    }
}
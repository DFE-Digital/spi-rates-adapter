namespace Dfe.Spi.RatesAdapter.Domain.Exceptions
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Thrown when rates for an entity that has not been implemented is
    /// requested.
    /// </summary>
    [SuppressMessage(
        "Microsoft.Design",
        "CA1032",
        Justification = "Not a public library.")]
    public class EntityNotImplementedException : NotImplementedException
    {
        /// <summary>
        /// Initialises a new instance of the
        /// <see cref="EntityNotImplementedException" /> class.
        /// </summary>
        /// <param name="entityName">
        /// The name of the entity requested.
        /// </param>
        public EntityNotImplementedException(string entityName)
        {
            this.EntityName = entityName;
        }

        /// <summary>
        /// Gets the name of the entity requested.
        /// </summary>
        public string EntityName
        {
            get;
            private set;
        }
    }
}
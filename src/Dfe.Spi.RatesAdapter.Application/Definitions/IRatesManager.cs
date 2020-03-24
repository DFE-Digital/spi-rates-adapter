namespace Dfe.Spi.RatesAdapter.Application.Definitions
{
    using System.Threading;
    using System.Threading.Tasks;
    using Dfe.Spi.Models.Entities;

    /// <summary>
    /// Describes the operations of the <see cref="Rates" /> manager.
    /// </summary>
    public interface IRatesManager
    {
        /// <summary>
        /// Gets an instance of <see cref="Rates" /> by its
        /// <paramref name="identifier" />.
        /// </summary>
        /// <param name="year">
        /// The year of the <see cref="Rates" /> in which to return.
        /// </param>
        /// <param name="entityName">
        /// The name of the entity in which to return <see cref="Rates" />
        /// for.
        /// </param>
        /// <param name="identifier">
        /// The identifier of the <see cref="Rates" /> instance, according
        /// to the <paramref name="entityName" />.
        /// </param>
        /// <param name="cancellationToken">
        /// An instance of <see cref="CancellationToken" />.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Rates" />.
        /// </returns>
        Task<Rates> GetRatesAsync(
            int year,
            string entityName,
            string identifier,
            CancellationToken cancellationToken);
    }
}
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
        /// <paramref name="id" />.
        /// </summary>
        /// <param name="id">
        /// The id of the <see cref="Rates" /> instance.
        /// </param>
        /// <param name="cancellationToken">
        /// An instance of <see cref="CancellationToken" />.
        /// </param>
        /// <returns>
        /// An instance of <see cref="Rates" />.
        /// </returns>
        Task<Rates> GetRatesAsync(
            string id,
            CancellationToken cancellationToken);
    }
}
namespace Dfe.Spi.RatesAdapter.Domain.Definitions
{
    using System.Threading;
    using System.Threading.Tasks;
    using Dfe.Spi.RatesAdapter.Domain.Models;

    /// <summary>
    /// Describes the operations of the
    /// <see cref="LocalAuthorityInformation" /> storage adapter.
    /// </summary>
    public interface ILocalAuthorityInformationStorageAdapter
        : IStorageAdapter<LocalAuthorityInformation>
    {
        /// <summary>
        /// Gets an individual <see cref="LocalAuthorityInformation" />
        /// instance from the underlying storage.
        /// </summary>
        /// <param name="year">
        /// The year of the <see cref="LocalAuthorityInformation" /> in which
        /// to return.
        /// </param>
        /// <param name="laNumber">
        /// The LA number of the <see cref="LocalAuthorityInformation" /> to
        /// return.
        /// </param>
        /// <param name="cancellationToken">
        /// An instance of type <see cref="CancellationToken" />.
        /// </param>
        /// <returns>
        /// An instance of <see cref="LocalAuthorityInformation" />.
        /// </returns>
        Task<LocalAuthorityInformation> GetLocalAuthorityInformationAsync(
            int year,
            short laNumber,
            CancellationToken cancellationToken);
    }
}
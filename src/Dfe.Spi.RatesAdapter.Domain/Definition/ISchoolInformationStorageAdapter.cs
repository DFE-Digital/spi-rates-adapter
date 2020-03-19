namespace Dfe.Spi.RatesAdapter.Domain.Definition
{
    using System.Threading;
    using System.Threading.Tasks;
    using Dfe.Spi.RatesAdapter.Domain.Models;

    /// <summary>
    /// Describes the operations of the <see cref="SchoolInformation" />
    /// storage adapter.
    /// </summary>
    public interface ISchoolInformationStorageAdapter
    {
        /// <summary>
        /// Gets an individual <see cref="SchoolInformation" /> instance from
        /// the underlying storage.
        /// </summary>
        /// <param name="urn">
        /// The URN of the <see cref="SchoolInformation" /> to return.
        /// </param>
        /// <param name="cancellationToken">
        /// An instance of type <see cref="CancellationToken" />.
        /// </param>
        /// <returns>
        /// An instance of <see cref="SchoolInformation" />.
        /// </returns>
        Task<SchoolInformation> GetSchoolInformationAsync(
            long urn,
            CancellationToken cancellationToken);
    }
}
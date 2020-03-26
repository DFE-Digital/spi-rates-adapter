namespace Dfe.Spi.RatesAdapter.Application.Definitions
{
    using System.Threading;
    using System.Threading.Tasks;
    using Dfe.Spi.Models.Entities;

    /// <summary>
    /// Describes the operations of the <see cref="ManagementGroupRates" />
    /// manager.
    /// </summary>
    public interface IManagementGroupRatesManager
    {
        /// <summary>
        /// Gets an instance of <see cref="ManagementGroupRates" /> by its
        /// <paramref name="laNumber" />.
        /// </summary>
        /// <param name="year">
        /// The year of the <see cref="ManagementGroupRates" /> in which to
        /// return.
        /// </param>
        /// <param name="laNumber">
        /// The LA number of the <see cref="ManagementGroupRates" /> instance.
        /// </param>
        /// <param name="cancellationToken">
        /// An instance of <see cref="CancellationToken" />.
        /// </param>
        /// <returns>
        /// An instance of <see cref="ManagementGroupRates" />.
        /// </returns>
        Task<ManagementGroupRates> GetManagementGroupRatesAsync(
            int year,
            short laNumber,
            CancellationToken cancellationToken);
    }
}
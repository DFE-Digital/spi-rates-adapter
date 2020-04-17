using Dfe.Spi.RatesAdapter.Application.Models;

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
        /// <param name="fields">
        /// Fields to select. Null to take all
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
            string fields,
            CancellationToken cancellationToken);
        
        /// <summary>
        /// Get an array of <see cref="ManagementGroupRates" /> for specified laNumbers
        /// </summary>
        /// <param name="managementGroupYearPointers">
        /// Array of <see cref="ManagementGroupYearPointer" /> in which to return.
        /// </param>
        /// <param name="fields">
        /// Fields to select. Null to take all
        /// </param>
        /// <param name="cancellationToken">
        /// An instance of <see cref="CancellationToken" />.
        /// </param>
        /// <returns>
        /// An array of <see cref="ManagementGroupRates" />.
        /// </returns>
        Task<ManagementGroupRates[]> GetManagementGroupsRatesAsync(
            ManagementGroupYearPointer[] managementGroupYearPointers,
            string[] fields,
            CancellationToken cancellationToken);
    }
}
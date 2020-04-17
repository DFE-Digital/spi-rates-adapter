using System.Collections.Generic;
using Dfe.Spi.RatesAdapter.Application.Models;

namespace Dfe.Spi.RatesAdapter.Application.Definitions
{
    using System.Threading;
    using System.Threading.Tasks;
    using Dfe.Spi.Models.Entities;

    /// <summary>
    /// Describes the operations of the <see cref="LearningProviderRates" />
    /// manager.
    /// </summary>
    public interface ILearningProviderRatesManager
    {
        /// <summary>
        /// Gets an instance of <see cref="LearningProviderRates" /> by its
        /// <paramref name="urn" />.
        /// </summary>
        /// <param name="year">
        /// The year of the <see cref="LearningProviderRates" /> in which to
        /// return.
        /// </param>
        /// <param name="urn">
        /// The urn of the <see cref="LearningProviderRates" /> instance.
        /// </param>
        /// <param name="cancellationToken">
        /// An instance of <see cref="CancellationToken" />.
        /// </param>
        /// <returns>
        /// An instance of <see cref="LearningProviderRates" />.
        /// </returns>
        Task<LearningProviderRates> GetLearningProviderRatesAsync(
            int year,
            long urn,
            string fields,
            CancellationToken cancellationToken);
        
        /// <summary>
        /// Gets an instance of <see cref="LearningProviderRates" /> by its
        /// <paramref name="urn" />.
        /// </summary>
        /// <param name="year">
        /// The year of the <see cref="LearningProviderRates" /> in which to
        /// return.
        /// </param>
        /// <param name="urn">
        /// The urn of the <see cref="LearningProviderRates" /> instance.
        /// </param>
        /// <param name="cancellationToken">
        /// An instance of <see cref="CancellationToken" />.
        /// </param>
        /// <returns>
        /// An instance of <see cref="LearningProviderRates" />.
        /// </returns>
        Task<LearningProviderRates[]> GetLearningProvidersRatesAsync(
            LearningProviderYearPointer[] learningProviderYearPointers,
            string[] fields,
            CancellationToken cancellationToken);
    }
}
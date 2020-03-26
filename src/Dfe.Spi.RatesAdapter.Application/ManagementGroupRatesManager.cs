namespace Dfe.Spi.RatesAdapter.Application
{
    using System.Threading;
    using System.Threading.Tasks;
    using Dfe.Spi.Models.Entities;
    using Dfe.Spi.RatesAdapter.Application.Definitions;

    /// <summary>
    /// Implements <see cref="IManagementGroupRatesManager" />.
    /// </summary>
    public class ManagementGroupRatesManager : IManagementGroupRatesManager
    {
        /// <inheritdoc />
        public Task<ManagementGroupRates> GetManagementGroupRatesAsync(
            int year,
            short laNumber,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
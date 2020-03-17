namespace Dfe.Spi.RatesAdapter.Application
{
    using System.Threading;
    using System.Threading.Tasks;
    using Dfe.Spi.Models.Entities;
    using Dfe.Spi.RatesAdapter.Application.Definitions;

    /// <summary>
    /// Implements <see cref="IRatesManager" />.
    /// </summary>
    public class RatesManager : IRatesManager
    {
        /// <inheritdoc />
        public Task<Rates> GetRatesAsync(
            string id,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
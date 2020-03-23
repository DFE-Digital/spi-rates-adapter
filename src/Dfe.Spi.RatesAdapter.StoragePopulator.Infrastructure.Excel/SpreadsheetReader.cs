namespace Dfe.Spi.RatesAdapter.StoragePopulator.Infrastructure.Excel
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Dfe.Spi.RatesAdapter.StoragePopulator.Domain.Definitions;
    using Dfe.Spi.RatesAdapter.StoragePopulator.Domain.Models.ConfigurationFileModels;

    /// <summary>
    /// Implements <see cref="ISpreadsheetReader" />.
    /// </summary>
    public class SpreadsheetReader : ISpreadsheetReader
    {
        /// <inheritdoc />
        public async Task<IEnumerable<RatesAdapter.Domain.Models.ModelsBase>> ReadAsync(
            ConfigurationFile configurationFile,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException("TODO");
        }
    }
}
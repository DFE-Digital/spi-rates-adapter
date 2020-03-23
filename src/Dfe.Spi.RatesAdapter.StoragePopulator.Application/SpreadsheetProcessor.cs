namespace Dfe.Spi.RatesAdapter.StoragePopulator.Application
{
    using System;
    using System.Threading.Tasks;
    using Dfe.Spi.RatesAdapter.StoragePopulator.Application.Definitions.Processors;
    using Dfe.Spi.RatesAdapter.StoragePopulator.Application.Models.SpreadsheetProcessorModels;

    /// <summary>
    /// Implements <see cref="ISpreadsheetProcessor" />.
    /// </summary>
    public class SpreadsheetProcessor : ISpreadsheetProcessor
    {
        /// <inheritdoc />
        public Task<ProcessResponse> ProcessAsync(
            ProcessRequest processRequest)
        {
            throw new NotImplementedException("TODO");
        }
    }
}
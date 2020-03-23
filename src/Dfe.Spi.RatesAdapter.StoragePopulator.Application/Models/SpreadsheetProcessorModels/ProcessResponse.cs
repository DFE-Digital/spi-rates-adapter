namespace Dfe.Spi.RatesAdapter.StoragePopulator.Application.Models.SpreadsheetProcessorModels
{
    using System.Threading;
    using Dfe.Spi.RatesAdapter.StoragePopulator.Application.Definitions.Processors;

    /// <summary>
    /// Response object for the
    /// <see cref="ISpreadsheetProcessor.ProcessAsync(ProcessRequest, CancellationToken)" />
    /// method.
    /// </summary>
    public class ProcessResponse : ModelsBase
    {
        // Nothing.
    }
}
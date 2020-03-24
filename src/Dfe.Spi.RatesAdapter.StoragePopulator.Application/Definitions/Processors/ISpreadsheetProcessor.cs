namespace Dfe.Spi.RatesAdapter.StoragePopulator.Application.Definitions.Processors
{
    using System.Threading;
    using System.Threading.Tasks;
    using Dfe.Spi.RatesAdapter.StoragePopulator.Application.Models.SpreadsheetProcessorModels;

    /// <summary>
    /// Describes the operations of the spreadsheet processor.
    /// </summary>
    public interface ISpreadsheetProcessor
    {
        /// <summary>
        /// Parses a spreadsheet, taking the contents and a configuration file,
        /// and then inserts the newly created entities in the specified Azure
        /// storage table.
        /// </summary>
        /// <param name="processRequest">
        /// An instance of <see cref="ProcessRequest" />.
        /// </param>
        /// <param name="cancellationToken">
        /// An instance of <see cref="CancellationToken" />.
        /// </param>
        /// <returns>
        /// An instance of <see cref="ProcessResponse" />.
        /// </returns>
        Task<ProcessResponse> ProcessAsync(
            ProcessRequest processRequest,
            CancellationToken cancellationToken);
    }
}
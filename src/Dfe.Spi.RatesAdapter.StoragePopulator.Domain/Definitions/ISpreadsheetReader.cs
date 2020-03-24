namespace Dfe.Spi.RatesAdapter.StoragePopulator.Domain.Definitions
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Dfe.Spi.RatesAdapter.StoragePopulator.Domain.Models.ConfigurationFileModels;

    /// <summary>
    /// Describes the operations of the spreadsheet reader.
    /// </summary>
    public interface ISpreadsheetReader
    {
        /// <summary>
        /// Reads a spreadsheet, and returns a set of instances of type
        /// <see cref="RatesAdapter.Domain.Models.ModelsBase" />.
        /// </summary>
        /// <param name="configurationFile">
        /// An instance of <see cref="ConfigurationFile" />.
        /// </param>
        /// <param name="spreadsheetFile">
        /// A path to the spreadsheet file.
        /// </param>
        /// <param name="cancellationToken">
        /// An instance of <see cref="CancellationToken" />.
        /// </param>
        /// <returns>
        /// A set of instances of type
        /// <see cref="RatesAdapter.Domain.Models.ModelsBase" />.
        /// </returns>
        Task<IEnumerable<RatesAdapter.Domain.Models.ModelsBase>> ReadAsync(
            ConfigurationFile configurationFile,
            string spreadsheetFile,
            CancellationToken cancellationToken);
    }
}
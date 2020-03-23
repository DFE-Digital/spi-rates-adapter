namespace Dfe.Spi.RatesAdapter.StoragePopulator.Application.Definitions
{
    using System.Threading;
    using System.Threading.Tasks;
    using Dfe.Spi.RatesAdapter.StoragePopulator.Application.Models.ConfigurationFileModels;

    /// <summary>
    /// Describes the operations of the <see cref="ConfigurationFile" />
    /// reader.
    /// </summary>
    public interface IConfigurationFileReader
    {
        /// <summary>
        /// Reads a configuration file from the file system, deserialises it
        /// and returns it as a <see cref="ConfigurationFile" /> instance.
        /// </summary>
        /// <param name="filePath">
        /// A file path to a configuration file.
        /// </param>
        /// <param name="cancellationToken">
        /// An instance of <see cref="CancellationToken" />.
        /// </param>
        /// <returns>
        /// An instance of <see cref="ConfigurationFile" />.
        /// </returns>
        Task<ConfigurationFile> ReadAsync(
            string filePath,
            CancellationToken cancellationToken);
    }
}
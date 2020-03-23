namespace Dfe.Spi.RatesAdapter.StoragePopulator.Application
{
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Dfe.Spi.RatesAdapter.StoragePopulator.Application.Definitions;
    using Dfe.Spi.RatesAdapter.StoragePopulator.Application.Models.ConfigurationFileModels;
    using Newtonsoft.Json;

    /// <summary>
    /// Implements <see cref="IConfigurationFileReader" />.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ConfigurationFileReader : IConfigurationFileReader
    {
        /// <inheritdoc />
        public async Task<ConfigurationFile> ReadAsync(
            string filePath,
            CancellationToken cancellationToken)
        {
            ConfigurationFile toReturn = null;

            FileInfo fileInfo = new FileInfo(filePath);

            // 1) Read from the filesystem.
            string configurationFileStr = null;
            using (FileStream fileStream = fileInfo.Open(FileMode.Open, FileAccess.Read))
            {
                using (StreamReader streamReader = new StreamReader(fileStream))
                {
                    configurationFileStr = await streamReader.ReadToEndAsync()
                        .ConfigureAwait(false);
                }
            }

            // 2) Deserialise.
            toReturn = JsonConvert.DeserializeObject<ConfigurationFile>(
                configurationFileStr);

            return toReturn;
        }
    }
}
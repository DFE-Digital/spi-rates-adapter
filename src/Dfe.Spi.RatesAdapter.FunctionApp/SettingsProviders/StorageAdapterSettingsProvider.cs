namespace Dfe.Spi.RatesAdapter.FunctionApp.SettingsProviders
{
    using Dfe.Spi.RatesAdapter.Domain.Definitions.SettingsProviders;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Implements <see cref="IStorageAdapterSettingsProvider" />.
    /// </summary>
    public abstract class StorageAdapterSettingsProvider
        : IStorageAdapterSettingsProvider
    {
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initialises a new instance of the
        /// <see cref="StorageAdapterSettingsProvider" /> class.
        /// </summary>
        /// <param name="configuration">
        /// An instance of type <see cref="IConfiguration" />.
        /// </param>
        public StorageAdapterSettingsProvider(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <inheritdoc />
        public string StorageConnectionString
        {
            get
            {
                string toReturn =
                    this.configuration["StorageConnectionString"];

                return toReturn;
            }
        }

        /// <inheritdoc />
        public abstract string TableName
        {
            get;
        }
    }
}
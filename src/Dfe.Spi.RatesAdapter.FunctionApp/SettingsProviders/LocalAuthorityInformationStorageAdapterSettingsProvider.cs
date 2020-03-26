namespace Dfe.Spi.RatesAdapter.FunctionApp.SettingsProviders
{
    using Dfe.Spi.RatesAdapter.Domain.Definitions.SettingsProviders;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Implements
    /// <see cref="ILocalAuthorityInformationStorageAdapterSettingsProvider" />.
    /// </summary>
    public class LocalAuthorityInformationStorageAdapterSettingsProvider
        : StorageAdapterSettingsProvider, ILocalAuthorityInformationStorageAdapterSettingsProvider
    {
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initialises a new instance of the
        /// <see cref="LocalAuthorityInformationStorageAdapterSettingsProvider" />
        /// class.
        /// </summary>
        /// <param name="configuration">
        /// An instance of type
        /// <see cref="IConfiguration" />.
        /// </param>
        public LocalAuthorityInformationStorageAdapterSettingsProvider(
            IConfiguration configuration)
            : base(configuration)
        {
            this.configuration = configuration;
        }

        /// <inheritdoc />
        public override string TableName
        {
            get
            {
                string toReturn =
                    this.configuration["LocalAuthorityInformationTableName"];

                return toReturn;
            }
        }
    }
}
namespace Dfe.Spi.RatesAdapter.StoragePopulator.ConsoleApp.SettingsProvider
{
    using Dfe.Spi.RatesAdapter.Domain.Definitions.SettingsProviders;

    /// <summary>
    /// Implements
    /// <see cref="ILocalAuthorityInformationStorageAdapterSettingsProvider" />.
    /// </summary>
    public class LocalAuthorityInformationStorageAdapterSettingsProvider
        : StorageAdapterSettingsProvider, ILocalAuthorityInformationStorageAdapterSettingsProvider
    {
        /// <summary>
        /// Initialises a new instance of the
        /// <see cref="LocalAuthorityInformationStorageAdapterSettingsProvider" />
        /// class.
        /// </summary>
        /// <param name="storageConnectionString">
        /// the connection string to the storage account.
        /// </param>
        /// <param name="tableName">
        /// The storage table name.
        /// </param>
        public LocalAuthorityInformationStorageAdapterSettingsProvider(
            string storageConnectionString,
            string tableName)
            : base(storageConnectionString, tableName)
        {
            // Nothing.
        }
    }
}
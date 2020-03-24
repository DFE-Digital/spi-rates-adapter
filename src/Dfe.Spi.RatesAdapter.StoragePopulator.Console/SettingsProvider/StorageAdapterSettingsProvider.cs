namespace Dfe.Spi.RatesAdapter.StoragePopulator.ConsoleApp.SettingsProvider
{
    using Dfe.Spi.RatesAdapter.Domain.Definitions.SettingsProviders;

    /// <summary>
    /// Implements <see cref="IStorageAdapterSettingsProvider" />.
    /// </summary>
    public abstract class StorageAdapterSettingsProvider
        : IStorageAdapterSettingsProvider
    {
        /// <summary>
        /// Initialises a new instance of the
        /// <see cref=" StorageAdapterSettingsProvider" /> class.
        /// </summary>
        /// <param name="storageConnectionString">
        /// the connection string to the storage account.
        /// </param>
        /// <param name="tableName">
        /// The storage table name.
        /// </param>
        public StorageAdapterSettingsProvider(
            string storageConnectionString,
            string tableName)
        {
            this.StorageConnectionString = storageConnectionString;
            this.TableName = tableName;
        }

        /// <inheritdoc />
        public string StorageConnectionString
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public string TableName
        {
            get;
            private set;
        }
    }
}

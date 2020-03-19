namespace Dfe.Spi.RatesAdapter.Domain.Definition.SettingsProviders
{
    /// <summary>
    /// Describes the operations of the storage provider settings provider.
    /// </summary>
    public interface IStorageAdapterSettingsProvider
    {
        /// <summary>
        /// Gets the connection string to the storage account.
        /// </summary>
        string StorageConnectionString
        {
            get;
        }

        /// <summary>
        /// Gets the storage table name.
        /// </summary>
        string TableName
        {
            get;
        }
    }
}
namespace Dfe.Spi.RatesAdapter.Infrastructure.AzureStorage
{
    using System;
    using Dfe.Spi.RatesAdapter.Domain.Definition.SettingsProviders;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Table;

    /// <summary>
    /// Base class for all storage adapters.
    /// </summary>
    public abstract class StorageAdapterBase
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="StorageAdapterBase" />
        /// class.
        /// </summary>
        /// <param name="storageAdapterSettingsProvider">
        /// An instance of type <see cref="IStorageAdapterSettingsProvider" />.
        /// </param>
        public StorageAdapterBase(
            IStorageAdapterSettingsProvider storageAdapterSettingsProvider)
        {
            if (storageAdapterSettingsProvider == null)
            {
                throw new ArgumentNullException(
                    nameof(storageAdapterSettingsProvider));
            }

            string storageConnectionString =
                storageAdapterSettingsProvider.StorageConnectionString;

            CloudStorageAccount cloudStorageAccount =
                CloudStorageAccount.Parse(storageConnectionString);

            CloudTableClient cloudTableClient =
                cloudStorageAccount.CreateCloudTableClient();

            string enumerationsStorageTableName =
                storageAdapterSettingsProvider.TableName;

            this.CloudTable = cloudTableClient.GetTableReference(
                enumerationsStorageTableName);
        }

        /// <summary>
        /// Gets an instance of
        /// <see cref="Microsoft.WindowsAzure.Storage.Table.CloudTable" />.
        /// </summary>
        protected CloudTable CloudTable
        {
            get;
            private set;
        }
    }
}
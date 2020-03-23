namespace Dfe.Spi.RatesAdapter.Infrastructure.AzureStorage
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using Dfe.Spi.Common.Logging.Definitions;
    using Dfe.Spi.RatesAdapter.Domain.Definitions;
    using Dfe.Spi.RatesAdapter.Domain.Definitions.SettingsProviders;
    using Dfe.Spi.RatesAdapter.Infrastructure.AzureStorage.Models;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Table;

    /// <summary>
    /// Base class for all storage adapters.
    /// </summary>
    /// <typeparam name="TModelsBase">
    /// A type deriving from <see cref="ModelsBase" />.
    /// </typeparam>
    public abstract class StorageAdapterBase<TModelsBase>
        : IStorageAdapter<TModelsBase>
        where TModelsBase : Domain.Models.ModelsBase
    {
        private readonly ILoggerWrapper loggerWrapper;

        /// <summary>
        /// Initialises a new instance of the
        /// <see cref="StorageAdapterBase{TModelBase}" /> class.
        /// </summary>
        /// <param name="loggerWrapper">
        /// An instance of type <see cref="ILoggerWrapper" />.
        /// </param>
        /// <param name="storageAdapterSettingsProvider">
        /// An instance of type <see cref="IStorageAdapterSettingsProvider" />.
        /// </param>
        public StorageAdapterBase(
            ILoggerWrapper loggerWrapper,
            IStorageAdapterSettingsProvider storageAdapterSettingsProvider)
        {
            if (storageAdapterSettingsProvider == null)
            {
                throw new ArgumentNullException(
                    nameof(storageAdapterSettingsProvider));
            }

            this.loggerWrapper = loggerWrapper;

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

        /// <inheritdoc />
        public async Task CreateTableAsync(
            CancellationToken cancellationToken)
        {
            this.loggerWrapper.Debug(
                $"Creating \"{this.CloudTable.Name}\"...");

            await this.CloudTable.CreateIfNotExistsAsync()
                .ConfigureAwait(false);

            this.loggerWrapper.Info(
                $"Table \"{this.CloudTable.Name}\" created.");
        }

        /// <inheritdoc />
        public abstract Task CreateAsync(
            int year,
            TModelsBase modelsBase,
            CancellationToken cancellationToken);

        /// <summary>
        /// Creates a record in storage, accepting a generic
        /// <see cref="ModelsBase" /> instance.
        /// </summary>
        /// <param name="modelsBases">
        /// A set of instances of type <see cref="ModelsBase" />.
        /// </param>
        /// <param name="cancellationToken">
        /// An instance of <see cref="CancellationToken" />.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        protected Task CreateAsync(
            IEnumerable<ModelsBase> modelsBases,
            CancellationToken cancellationToken)
        {
            Task toReturn = null;

            if (modelsBases == null)
            {
                throw new ArgumentNullException(nameof(modelsBases));
            }

            TableBatchOperation tableBatchOperation =
                new TableBatchOperation();

            TableOperation tableOperation = null;
            foreach (ModelsBase modelsBase in modelsBases)
            {
                tableOperation = TableOperation.Insert(modelsBase);

                tableBatchOperation.Add(tableOperation);
            }

            int count = tableBatchOperation.Count;
            this.loggerWrapper.Debug($"Inserting batch of size {count}...");

            // Fire and forget.
            this.CloudTable.ExecuteBatchAsync(tableBatchOperation)
                .ConfigureAwait(false);

            toReturn = Task.CompletedTask;

            return toReturn;
        }
    }
}
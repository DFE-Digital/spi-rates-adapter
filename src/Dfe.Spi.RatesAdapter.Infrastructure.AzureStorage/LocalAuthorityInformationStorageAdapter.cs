namespace Dfe.Spi.RatesAdapter.Infrastructure.AzureStorage
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Dfe.Spi.Common.AzureStorage;
    using Dfe.Spi.Common.Logging.Definitions;
    using Dfe.Spi.RatesAdapter.Domain.Definitions;
    using Dfe.Spi.RatesAdapter.Domain.Definitions.SettingsProviders;
    using Dfe.Spi.RatesAdapter.Domain.Exceptions;
    using Dfe.Spi.RatesAdapter.Infrastructure.AzureStorage.Models;
    using Dfe.Spi.RatesAdapter.Infrastructure.AzureStorage.Models.LocalAuthorityRatesGroups;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Table;
    using DomainModels = Dfe.Spi.RatesAdapter.Domain.Models;

    /// <summary>
    /// Implements <see cref="ILocalAuthorityInformationStorageAdapter" />.
    /// </summary>
    public class LocalAuthorityInformationStorageAdapter
        : StorageAdapterBase<DomainModels.LocalAuthorityInformation>, ILocalAuthorityInformationStorageAdapter
    {
        private readonly ILoggerWrapper loggerWrapper;
        private readonly IMapper mapper;

        /// <summary>
        /// Initialises a new instance of the
        /// <see cref="LocalAuthorityInformationStorageAdapter" /> class.
        /// </summary>
        /// <param name="loggerWrapper">
        /// An instance of type <see cref="ILoggerWrapper" />.
        /// </param>
        /// <param name="localAuthorityInformationStorageAdapterSettingsProvider">
        /// An instance of type
        /// <see cref="ILocalAuthorityInformationStorageAdapterSettingsProvider" />.
        /// </param>
        public LocalAuthorityInformationStorageAdapter(
            ILoggerWrapper loggerWrapper,
            ILocalAuthorityInformationStorageAdapterSettingsProvider localAuthorityInformationStorageAdapterSettingsProvider)
            : base(
                  loggerWrapper,
                  localAuthorityInformationStorageAdapterSettingsProvider)
        {
            this.loggerWrapper = loggerWrapper;

            MapperConfiguration mapperConfiguration = new MapperConfiguration(
                x =>
                {
                    x.CreateMap<LocalAuthorityInformation, DomainModels.LocalAuthorityInformation>().ReverseMap();

                    x.CreateMap<ProvisionalFunding, DomainModels.Rates.ProvisionalFunding>().ReverseMap();
                });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        /// <inheritdoc />
        public async override Task CreateAsync(
            int year,
            DomainModels.LocalAuthorityInformation localAuthorityInformation,
            CancellationToken cancellationToken)
        {
            if (localAuthorityInformation == null)
            {
                throw new ArgumentNullException(
                    nameof(localAuthorityInformation));
            }

            if (!localAuthorityInformation.LaNumber.HasValue)
            {
                throw new DataException(
                    $"A {nameof(localAuthorityInformation.LaNumber)} is " +
                    $"required in order to insert.");
            }

            long laNumber = localAuthorityInformation.LaNumber.Value;

            List<ModelsBase> modelsBases = new List<ModelsBase>()
            {
                this.Map<DomainModels.LocalAuthorityInformation, LocalAuthorityInformation>(
                    year,
                    laNumber,
                    localAuthorityInformation),

                this.Map<DomainModels.Rates.ProvisionalFunding, ProvisionalFunding>(
                    year,
                    laNumber,
                    localAuthorityInformation.ProvisionalFunding),
            };

            try
            {
                await this.CreateAsync(
                    modelsBases,
                    cancellationToken)
                    .ConfigureAwait(false);
            }
            catch (StorageException storageException)
            {
                this.loggerWrapper.Error(
                    $"Note! Exception thrown whilst trying to insert record " +
                    $"for {nameof(year)} {year}, {nameof(laNumber)} " +
                    $"{laNumber}.",
                    storageException);
            }
        }

        /// <inheritdoc />
        public async Task<DomainModels.LocalAuthorityInformation> GetLocalAuthorityInformationAsync(
            int year,
            short laNumber,
            CancellationToken cancellationToken)
        {
            DomainModels.LocalAuthorityInformation toReturn = null;

            string identifier =
                $"{year.ToString(CultureInfo.InvariantCulture)}-{laNumber.ToString(CultureInfo.InvariantCulture)}";

            TableQuery tableQuery = new TableQuery();

            string filter = TableQuery.GenerateFilterCondition(
                "PartitionKey",
                QueryComparisons.Equal,
                identifier);

            tableQuery.Where(filter);

            IList<LocalAuthorityRatesGroupsBase> localAuthorityRatesGroupsBase =
                await this.CloudTable.ExecuteQueryAsync(
                    tableQuery,
                    this.EntityResolver,
                    cancellationToken)
                    .ConfigureAwait(false);

            if (localAuthorityRatesGroupsBase.Count == 0)
            {
                throw new RatesNotFoundException();
            }

            toReturn = this.Map(localAuthorityRatesGroupsBase);

            return toReturn;
        }

        private DomainModels.LocalAuthorityInformation Map(
            IList<LocalAuthorityRatesGroupsBase> localAuthorityRatesGroupsBases)
        {
            DomainModels.LocalAuthorityInformation toReturn = null;

            toReturn =
                this.ExtractAndMap<DomainModels.LocalAuthorityInformation, LocalAuthorityInformation>(
                    localAuthorityRatesGroupsBases);

            toReturn.ProvisionalFunding =
                this.ExtractAndMap<DomainModels.Rates.ProvisionalFunding, ProvisionalFunding>(
                    localAuthorityRatesGroupsBases);

            return toReturn;
        }

        private TModelsBase ExtractAndMap<TModelsBase, TSchoolRatesGroupsBase>(
            IList<LocalAuthorityRatesGroupsBase> localAuthorityRatesGroupsBases)
            where TSchoolRatesGroupsBase : LocalAuthorityRatesGroupsBase
            where TModelsBase : DomainModels.ModelsBase
        {
            TModelsBase toReturn = null;

            LocalAuthorityRatesGroupsBase localAuthorityRatesGroupsBase =
                localAuthorityRatesGroupsBases.SingleOrDefault(x => x is TSchoolRatesGroupsBase);

            if (localAuthorityRatesGroupsBase != null)
            {
                TSchoolRatesGroupsBase schoolRatesGroupsBaseTyped =
                    localAuthorityRatesGroupsBase as TSchoolRatesGroupsBase;

                toReturn = this.mapper.Map<TSchoolRatesGroupsBase, TModelsBase>(
                    schoolRatesGroupsBaseTyped);
            }

            return toReturn;
        }

        private TLocalAuthorityRatesGroupsBase Map<TModelsBase, TLocalAuthorityRatesGroupsBase>(
            int year,
            long laNumber,
            TModelsBase modelsBase)
            where TLocalAuthorityRatesGroupsBase : LocalAuthorityRatesGroupsBase
            where TModelsBase : DomainModels.ModelsBase
        {
            TLocalAuthorityRatesGroupsBase toReturn =
                this.mapper.Map<TLocalAuthorityRatesGroupsBase>(modelsBase);

            toReturn.PartitionKey =
                $"{year.ToString(CultureInfo.InvariantCulture)}-{laNumber.ToString(CultureInfo.InvariantCulture)}";

            Type type = typeof(TLocalAuthorityRatesGroupsBase);
            toReturn.RowKey = type.Name;

            return toReturn;
        }

        private LocalAuthorityRatesGroupsBase EntityResolver(
            string partitionKey,
            string rowKey,
            DateTimeOffset timestamp,
            IDictionary<string, EntityProperty> properties,
            string etag)
        {
            LocalAuthorityRatesGroupsBase toReturn = null;

            Type baseType = typeof(LocalAuthorityRatesGroupsBase);

            string baseTypeNamespace = baseType.Namespace;

            // rowKey is the entity type.
            string typeName = $"{baseTypeNamespace}.{rowKey}";

            Type type = Type.GetType(typeName);

            toReturn = (LocalAuthorityRatesGroupsBase)Activator.CreateInstance(type);

            toReturn.PartitionKey = partitionKey;
            toReturn.RowKey = rowKey;
            toReturn.Timestamp = timestamp;
            toReturn.ETag = etag;

            toReturn.ReadEntity(properties, null);

            return toReturn;
        }
    }
}
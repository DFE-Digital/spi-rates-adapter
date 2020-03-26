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
    using Dfe.Spi.RatesAdapter.Infrastructure.AzureStorage.Models.SchoolRatesGroups;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Table;
    using DomainModels = Dfe.Spi.RatesAdapter.Domain.Models;

    /// <summary>
    /// Implements <see cref="ISchoolInformationStorageAdapter" />.
    /// </summary>
    public class SchoolInformationStorageAdapter
        : StorageAdapterBase<DomainModels.SchoolInformation>, ISchoolInformationStorageAdapter
    {
        private readonly ILoggerWrapper loggerWrapper;
        private readonly IMapper mapper;

        /// <summary>
        /// Initialises a new instance of the
        /// <see cref="SchoolInformationStorageAdapter" /> class.
        /// </summary>
        /// <param name="loggerWrapper">
        /// An instance of type <see cref="ILoggerWrapper" />.
        /// </param>
        /// <param name="schoolInformationStorageAdapterSettingsProvider">
        /// An instance of type
        /// <see cref="ISchoolInformationStorageAdapterSettingsProvider" />.
        /// </param>
        public SchoolInformationStorageAdapter(
            ILoggerWrapper loggerWrapper,
            ISchoolInformationStorageAdapterSettingsProvider schoolInformationStorageAdapterSettingsProvider)
            : base(
                loggerWrapper,
                schoolInformationStorageAdapterSettingsProvider)
        {
            this.loggerWrapper = loggerWrapper;

            MapperConfiguration mapperConfiguration = new MapperConfiguration(
                x =>
                {
                    x.CreateMap<SchoolInformation, DomainModels.SchoolInformation>().ReverseMap();

                    x.CreateMap<BaselineFunding, DomainModels.Rates.BaselineFunding>().ReverseMap();
                    x.CreateMap<NotionalFunding, DomainModels.Rates.NotionalFunding>().ReverseMap();
                    x.CreateMap<IllustrativeFunding, DomainModels.Rates.IllustrativeFunding>().ReverseMap();
                });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        /// <inheritdoc />
        public override async Task CreateAsync(
            int year,
            DomainModels.SchoolInformation schoolInformation,
            CancellationToken cancellationToken)
        {
            if (schoolInformation == null)
            {
                throw new ArgumentNullException(nameof(schoolInformation));
            }

            if (!schoolInformation.Urn.HasValue)
            {
                throw new DataException(
                    $"A {nameof(schoolInformation.Urn)} is required in " +
                    $"order to insert.");
            }

            long urn = schoolInformation.Urn.Value;

            List<ModelsBase> modelsBases = new List<ModelsBase>
            {
                this.Map<DomainModels.SchoolInformation, SchoolInformation>(
                    year,
                    urn,
                    schoolInformation),

                this.Map<DomainModels.Rates.BaselineFunding, BaselineFunding>(
                    year,
                    urn,
                    schoolInformation.BaselineFunding),
                this.Map<DomainModels.Rates.NotionalFunding, NotionalFunding>(
                    year,
                    urn,
                    schoolInformation.NotionalFunding),
            };

            if (schoolInformation.IllustrativeFunding != null)
            {
                IllustrativeFunding illustrativeFunding =
                    this.Map<DomainModels.Rates.IllustrativeFunding, IllustrativeFunding>(
                        year,
                        urn,
                        schoolInformation.IllustrativeFunding);

                modelsBases.Add(illustrativeFunding);
            }

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
                    $"for {nameof(year)} {year}, {nameof(urn)} {urn}.",
                    storageException);
            }
        }

        /// <inheritdoc />
        public async Task<Domain.Models.SchoolInformation> GetSchoolInformationAsync(
            int year,
            long urn,
            CancellationToken cancellationToken)
        {
            Domain.Models.SchoolInformation toReturn = null;

            string identifier =
                $"{year.ToString(CultureInfo.InvariantCulture)}-{urn.ToString(CultureInfo.InvariantCulture)}";

            TableQuery tableQuery = new TableQuery();

            string filter = TableQuery.GenerateFilterCondition(
                "PartitionKey",
                QueryComparisons.Equal,
                identifier);

            tableQuery.Where(filter);

            IList<SchoolRatesGroupsBase> schoolRatesGroupsBase =
                await this.CloudTable.ExecuteQueryAsync(
                    tableQuery,
                    this.EntityResolver,
                    cancellationToken)
                    .ConfigureAwait(false);

            if (schoolRatesGroupsBase.Count == 0)
            {
                throw new RatesNotFoundException(nameof(urn), urn);
            }

            toReturn = this.Map(schoolRatesGroupsBase);

            return toReturn;
        }

        private TSchoolRatesGroupsBase Map<TModelsBase, TSchoolRatesGroupsBase>(
            int year,
            long urn,
            TModelsBase modelsBase)
            where TSchoolRatesGroupsBase : SchoolRatesGroupsBase
            where TModelsBase : DomainModels.ModelsBase
        {
            TSchoolRatesGroupsBase toReturn =
                this.mapper.Map<TSchoolRatesGroupsBase>(modelsBase);

            toReturn.PartitionKey =
                $"{year.ToString(CultureInfo.InvariantCulture)}-{urn.ToString(CultureInfo.InvariantCulture)}";

            Type type = typeof(TSchoolRatesGroupsBase);
            toReturn.RowKey = type.Name;

            return toReturn;
        }

        private Domain.Models.SchoolInformation Map(
            IList<SchoolRatesGroupsBase> schoolRatesGroupsBases)
        {
            DomainModels.SchoolInformation toReturn = null;

            toReturn =
                this.ExtractAndMap<DomainModels.SchoolInformation, SchoolInformation>(
                    schoolRatesGroupsBases);

            toReturn.BaselineFunding =
                this.ExtractAndMap<DomainModels.Rates.BaselineFunding, BaselineFunding>(
                    schoolRatesGroupsBases);

            toReturn.IllustrativeFunding =
                this.ExtractAndMap<DomainModels.Rates.IllustrativeFunding, IllustrativeFunding>(
                    schoolRatesGroupsBases);

            toReturn.NotionalFunding =
                this.ExtractAndMap<DomainModels.Rates.NotionalFunding, NotionalFunding>(
                    schoolRatesGroupsBases);

            return toReturn;
        }

        private TModelsBase ExtractAndMap<TModelsBase, TSchoolRatesGroupsBase>(
            IList<SchoolRatesGroupsBase> schoolRatesGroupsBases)
            where TSchoolRatesGroupsBase : SchoolRatesGroupsBase
            where TModelsBase : DomainModels.ModelsBase
        {
            TModelsBase toReturn = null;

            SchoolRatesGroupsBase schoolRatesGroupsBase =
                schoolRatesGroupsBases.SingleOrDefault(x => x is TSchoolRatesGroupsBase);

            if (schoolRatesGroupsBase != null)
            {
                TSchoolRatesGroupsBase schoolRatesGroupsBaseTyped =
                    schoolRatesGroupsBase as TSchoolRatesGroupsBase;

                toReturn = this.mapper.Map<TSchoolRatesGroupsBase, TModelsBase>(
                    schoolRatesGroupsBaseTyped);
            }

            return toReturn;
        }

        private SchoolRatesGroupsBase EntityResolver(
            string partitionKey,
            string rowKey,
            DateTimeOffset timestamp,
            IDictionary<string, EntityProperty> properties,
            string etag)
        {
            SchoolRatesGroupsBase toReturn = null;

            Type baseType = typeof(SchoolRatesGroupsBase);

            string baseTypeNamespace = baseType.Namespace;

            // rowKey is the entity type.
            string typeName = $"{baseTypeNamespace}.{rowKey}";

            Type type = Type.GetType(typeName);

            toReturn = (SchoolRatesGroupsBase)Activator.CreateInstance(type);

            toReturn.PartitionKey = partitionKey;
            toReturn.RowKey = rowKey;
            toReturn.Timestamp = timestamp;
            toReturn.ETag = etag;

            toReturn.ReadEntity(properties, null);

            return toReturn;
        }
    }
}
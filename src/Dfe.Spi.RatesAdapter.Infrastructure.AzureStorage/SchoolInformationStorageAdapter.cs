namespace Dfe.Spi.RatesAdapter.Infrastructure.AzureStorage
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using AutoMapper;
    using Dfe.Spi.Common.AzureStorage;
    using Dfe.Spi.Common.Logging.Definitions;
    using Dfe.Spi.RatesAdapter.Domain.Definition;
    using Dfe.Spi.RatesAdapter.Domain.Definition.SettingsProviders;
    using Dfe.Spi.RatesAdapter.Domain.Models;
    using Dfe.Spi.RatesAdapter.Infrastructure.AzureStorage.Models.SchoolRatesGroups;
    using Microsoft.WindowsAzure.Storage.Table;

    /// <summary>
    /// Implements <see cref="ISchoolInformationStorageAdapter" />.
    /// </summary>
    public class SchoolInformationStorageAdapter
        : StorageAdapterBase, ISchoolInformationStorageAdapter
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
            : base(schoolInformationStorageAdapterSettingsProvider)
        {
            this.loggerWrapper = loggerWrapper;

            MapperConfiguration mapperConfiguration = new MapperConfiguration(
                x =>
                {
                    x.CreateMap<Models.SchoolRatesGroups.SchoolInformation, Domain.Models.SchoolInformation>();

                    x.CreateMap<BaselineFunding, Domain.Models.Rates.BaselineFunding>();
                    x.CreateMap<NotionalFunding, Domain.Models.Rates.NotionalFunding>();
                    x.CreateMap<IllustrativeFunding, Domain.Models.Rates.IllustrativeFunding>();
                });

            this.mapper = mapperConfiguration.CreateMapper();
        }

        /// <inheritdoc />
        public async Task<Domain.Models.SchoolInformation> GetSchoolInformationAsync(
            long urn,
            CancellationToken cancellationToken)
        {
            Domain.Models.SchoolInformation toReturn = null;

            string urnStr = urn.ToString(CultureInfo.InvariantCulture);

            TableQuery tableQuery = new TableQuery();

            string filter = TableQuery.GenerateFilterCondition(
                "PartitionKey",
                QueryComparisons.Equal,
                urnStr);

            tableQuery.Where(filter);

            IList<SchoolRatesGroupsBase> schoolRatesGroupsBase =
                await this.CloudTable.ExecuteQueryAsync(
                    tableQuery,
                    this.EntityResolver,
                    cancellationToken)
                    .ConfigureAwait(false);

            toReturn = this.Map(schoolRatesGroupsBase);

            return toReturn;
        }

        private Domain.Models.SchoolInformation Map(
            IList<SchoolRatesGroupsBase> schoolRatesGroupsBases)
        {
            Domain.Models.SchoolInformation toReturn = null;

            toReturn =
                this.ExtractAndMap<Domain.Models.SchoolInformation, Models.SchoolRatesGroups.SchoolInformation>(
                    schoolRatesGroupsBases);

            toReturn.BaselineFunding =
                this.ExtractAndMap<Domain.Models.Rates.BaselineFunding, BaselineFunding>(
                    schoolRatesGroupsBases);

            toReturn.IllustrativeFunding =
                this.ExtractAndMap<Domain.Models.Rates.IllustrativeFunding, IllustrativeFunding>(
                    schoolRatesGroupsBases);

            toReturn.NotionalFunding =
                this.ExtractAndMap<Domain.Models.Rates.NotionalFunding, NotionalFunding>(
                    schoolRatesGroupsBases);

            return toReturn;
        }

        private TModelsBase ExtractAndMap<TModelsBase, TSchoolRatesGroupsBase>(
            IList<SchoolRatesGroupsBase> schoolRatesGroupsBases)
            where TSchoolRatesGroupsBase : SchoolRatesGroupsBase
            where TModelsBase : ModelsBase
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
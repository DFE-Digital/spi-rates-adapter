namespace Dfe.Spi.RatesAdapter.FunctionApp.SettingsProviders
{
    using Dfe.Spi.RatesAdapter.Domain.Definition.SettingsProviders;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Implements <see cref="ISchoolInformationStorageAdapterSettingsProvider" />.
    /// </summary>
    public class SchoolInformationStorageAdapterSettingsProvider
        : StorageAdapterSettingsProvider, ISchoolInformationStorageAdapterSettingsProvider
    {
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initialises a new instance of the
        /// <see cref="SchoolInformationStorageAdapterSettingsProvider" />
        /// class.
        /// </summary>
        /// <param name="configuration">
        /// An instance of type <see cref="IConfiguration" />.
        /// </param>
        public SchoolInformationStorageAdapterSettingsProvider(
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
                    this.configuration["SchoolInformationTableName"];

                return toReturn;
            }
        }
    }
}
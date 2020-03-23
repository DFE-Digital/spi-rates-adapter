namespace Dfe.Spi.RatesAdapter.StoragePopulator.ConsoleApp.SettingsProvider
{
    using Dfe.Spi.RatesAdapter.Domain.Definitions.SettingsProviders;

    /// <summary>
    /// Implement
    /// <see cref="ISchoolInformationStorageAdapterSettingsProvider" />.
    /// </summary>
    public class SchoolInformationStorageAdapterSettingsProvider
        : StorageAdapterSettingsProvider, ISchoolInformationStorageAdapterSettingsProvider
    {
        /// <summary>
        /// Initialises a new instance of the
        /// <see cref="SchoolInformationStorageAdapterSettingsProvider" />
        /// class.
        /// </summary>
        /// <param name="storageConnectionString">
        /// The storage connection string.
        /// </param>
        /// <param name="tableName">
        /// The table name in which to populate.
        /// </param>
        public SchoolInformationStorageAdapterSettingsProvider(
            string storageConnectionString,
            string tableName)
            : base(storageConnectionString, tableName)
        {
            // Nothing.
        }
    }
}
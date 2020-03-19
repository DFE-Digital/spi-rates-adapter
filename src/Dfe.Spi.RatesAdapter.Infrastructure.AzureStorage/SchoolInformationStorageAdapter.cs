namespace Dfe.Spi.RatesAdapter.Infrastructure.AzureStorage
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Dfe.Spi.RatesAdapter.Domain.Definition;
    using Dfe.Spi.RatesAdapter.Domain.Definition.SettingsProviders;
    using Dfe.Spi.RatesAdapter.Domain.Models;

    /// <summary>
    /// Implements <see cref="ISchoolInformationStorageAdapter" />.
    /// </summary>
    public class SchoolInformationStorageAdapter
        : StorageAdapterBase, ISchoolInformationStorageAdapter
    {
        /// <summary>
        /// Initialises a new instance of the
        /// <see cref="SchoolInformationStorageAdapter" /> class.
        /// </summary>
        /// <param name="schoolInformationStorageAdapterSettingsProvider">
        /// An instance of type
        /// <see cref="ISchoolInformationStorageAdapterSettingsProvider" />.
        /// </param>
        public SchoolInformationStorageAdapter(
            ISchoolInformationStorageAdapterSettingsProvider schoolInformationStorageAdapterSettingsProvider)
            : base(schoolInformationStorageAdapterSettingsProvider)
        {
            // Nothing.
        }

        /// <inheritdoc />
        public Task<SchoolInformation> GetSchoolInformationAsync(
            long urn,
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException(
                "Look up the school by URN, out of the configured storage.");
        }
    }
}
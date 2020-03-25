namespace Dfe.Spi.RatesAdapter.Infrastructure.AzureStorage
{
    using System.Threading;
    using System.Threading.Tasks;
    using Dfe.Spi.Common.Logging.Definitions;
    using Dfe.Spi.RatesAdapter.Domain.Definitions;
    using Dfe.Spi.RatesAdapter.Domain.Definitions.SettingsProviders;
    using Dfe.Spi.RatesAdapter.Domain.Models;

    /// <summary>
    /// Implements <see cref="ILocalAuthorityInformationStorageAdapter" />.
    /// </summary>
    public class LocalAuthorityInformationStorageAdapter
        : StorageAdapterBase<LocalAuthorityInformation>, ILocalAuthorityInformationStorageAdapter
    {
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
            // Nothing, for now.
        }

        /// <inheritdoc />
        public override Task CreateAsync(
            int year,
            LocalAuthorityInformation localAuthorityInformation,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
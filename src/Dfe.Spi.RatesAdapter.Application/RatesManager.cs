namespace Dfe.Spi.RatesAdapter.Application
{
    using System.Globalization;
    using System.Threading;
    using System.Threading.Tasks;
    using Dfe.Spi.Models.Entities;
    using Dfe.Spi.RatesAdapter.Application.Definitions;
    using Dfe.Spi.RatesAdapter.Domain.Definition;
    using Dfe.Spi.RatesAdapter.Domain.Models;

    /// <summary>
    /// Implements <see cref="IRatesManager" />.
    /// </summary>
    public class RatesManager : IRatesManager
    {
        private readonly ISchoolInformationStorageAdapter schoolInformationStorageAdapter;

        /// <summary>
        /// Initialises a new instance of the <see cref="RatesManager" />
        /// class.
        /// </summary>
        /// <param name="schoolInformationStorageAdapter">
        /// An instance of type
        /// <see cref="ISchoolInformationStorageAdapter" />.
        /// </param>
        public RatesManager(
            ISchoolInformationStorageAdapter schoolInformationStorageAdapter)
        {
            this.schoolInformationStorageAdapter = schoolInformationStorageAdapter;
        }

        /// <inheritdoc />
        public async Task<Rates> GetRatesAsync(
            string id,
            CancellationToken cancellationToken)
        {
            // TODO: Simply in place to allow the calling of this adapter.
            //       The upper half will no doubt need refactoring when we
            //       know more about the super-model.
            SchoolInformation schoolInformation =
                await this.schoolInformationStorageAdapter.GetSchoolInformationAsync(
                    long.Parse(id, CultureInfo.InvariantCulture),
                    cancellationToken)
                    .ConfigureAwait(false);

            throw new System.NotImplementedException("Finish me!");
        }
    }
}
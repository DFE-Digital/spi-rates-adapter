namespace Dfe.Spi.RatesAdapter.Infrastructure.AzureStorage.Models.SchoolRatesGroups
{
    using Dfe.Spi.RatesAdapter.Domain.Definitions.Models;
    using Microsoft.WindowsAzure.Storage.Table;

    /// <summary>
    /// Implements <see cref="ISchoolInformation" />.
    /// </summary>
    public class SchoolInformation : SchoolRatesGroupsBase, ISchoolInformation
    {
        /// <inheritdoc />
        public string Region
        {
            get;
            set;
        }

        /// <inheritdoc />
        [IgnoreProperty]
        public long? Urn
        {
            get;
            set;
        }
    }
}
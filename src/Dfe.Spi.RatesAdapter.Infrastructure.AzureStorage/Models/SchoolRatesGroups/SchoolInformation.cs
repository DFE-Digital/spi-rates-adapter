namespace Dfe.Spi.RatesAdapter.Infrastructure.AzureStorage.Models.SchoolRatesGroups
{
    using Dfe.Spi.RatesAdapter.Domain.Definition.Models;

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
    }
}
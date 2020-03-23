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
        [IgnoreProperty]
        public long? Urn
        {
            get;
            set;
        }

        /// <inheritdoc />
        public string Region
        {
            get;
            set;
        }

        /// <inheritdoc />
        public long? LaEstab
        {
            get;
            set;
        }

        /// <inheritdoc />
        public string LaName
        {
            get;
            set;
        }

        /// <inheritdoc />
        public short? LaNumber
        {
            get;
            set;
        }

        /// <inheritdoc />
        public bool? NewAndGrowing
        {
            get;
            set;
        }

        /// <inheritdoc />
        public bool? PartiallyOpen
        {
            get;
            set;
        }

        /// <inheritdoc />
        public string Phase
        {
            get;
            set;
        }

        /// <inheritdoc />
        public string SchoolName
        {
            get;
            set;
        }

        /// <inheritdoc />
        public bool? TheoreticalBaselineExists
        {
            get;
            set;
        }
    }
}
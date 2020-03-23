namespace Dfe.Spi.RatesAdapter.Infrastructure.AzureStorage.Models.SchoolRatesGroups
{
    using Dfe.Spi.RatesAdapter.Domain.Definitions.Models.Rates;

    /// <summary>
    /// Implements <see cref="IBaselineFunding" />.
    /// </summary>
    public class BaselineFunding : SchoolRatesGroupsBase, IBaselineFunding
    {
        /// <inheritdoc />
        public long? Value
        {
            get;
            set;
        }

        /// <inheritdoc />
        public long? BaselineFundingFullSchool
        {
            get;
            set;
        }

        /// <inheritdoc />
        public int? PupilCount
        {
            get;
            set;
        }

        /// <inheritdoc />
        public int? NewAndGrowingSchoolsPupilCountIfFull
        {
            get;
            set;
        }

        /// <inheritdoc />
        public long? NewAndGrowingSchoolsValueIfFull
        {
            get;
            set;
        }

        /// <inheritdoc />
        public long? ValuePerPupil
        {
            get;
            set;
        }

        /// <inheritdoc />
        public long? NewAndGrowingSchoolsValuePerPupilIfFull
        {
            get;
            set;
        }
    }
}
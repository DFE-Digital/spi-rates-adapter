namespace Dfe.Spi.RatesAdapter.Infrastructure.AzureStorage.Models.SchoolRatesGroups
{
    using Dfe.Spi.RatesAdapter.Domain.Definitions.Models.Rates;

    /// <summary>
    /// Implements <see cref="IIllustrativeFunding" />.
    /// </summary>
    public class IllustrativeFunding
        : SchoolRatesGroupsBase, IIllustrativeFunding
    {
        /// <inheritdoc />
        public long? TotalNffFunding
        {
            get;
            set;
        }

        /// <inheritdoc />
        public double? PercentageChangeComparedToBaseline
        {
            get;
            set;
        }

        /// <inheritdoc />
        public double? PercentageChangeInPupilLedFunding
        {
            get;
            set;
        }

        /// <inheritdoc />
        public long? TotalNffFundingIfFullyImplemented
        {
            get;
            set;
        }

        /// <inheritdoc />
        public long? NewAndGrowingSchoolsTotalNffFundingIfFullyImplemented
        {
            get;
            set;
        }

        /// <inheritdoc />
        public double? PercentageChangeComparedToBaselineIfFullyImplemented
        {
            get;
            set;
        }

        /// <inheritdoc />
        public double? PercentageChangeInPupilLedFundingIfFullyImplemented
        {
            get;
            set;
        }
    }
}
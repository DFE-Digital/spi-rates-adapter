namespace Dfe.Spi.RatesAdapter.Infrastructure.AzureStorage.Models.SchoolRatesGroups
{
    using Dfe.Spi.RatesAdapter.Domain.Definitions.Models.Rates;

    /// <summary>
    /// Implements <see cref="INotionalFunding" />.
    /// </summary>
    public class NotionalFunding : SchoolRatesGroupsBase, INotionalFunding
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
        public int? PupilCount
        {
            get;
            set;
        }

        /// <inheritdoc />
        public long? TotalNffFundingPerPupil
        {
            get;
            set;
        }

        /// <inheritdoc />
        public double? PercentageChangeInPupilLedFundingPerPupil
        {
            get;
            set;
        }
    }
}

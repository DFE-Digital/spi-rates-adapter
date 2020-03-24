namespace Dfe.Spi.RatesAdapter.Infrastructure.AzureStorage.Models.SchoolRatesGroups
{
    using Dfe.Spi.RatesAdapter.Domain.Definitions.Models.Rates;

    /// <summary>
    /// Implements <see cref="INotionalFunding" />.
    /// </summary>
    public class NotionalFunding : SchoolRatesGroupsBase, INotionalFunding
    {
        /// <inheritdoc />
        public double? TotalNffFunding
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
        public double? PupilCount
        {
            get;
            set;
        }

        /// <inheritdoc />
        public double? TotalNffFundingPerPupil
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

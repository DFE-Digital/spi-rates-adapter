namespace Dfe.Spi.RatesAdapter.Domain.Definitions.Models.Rates
{
    /// <summary>
    /// Describes the properties of a illustrative funding model.
    /// </summary>
    public interface IIllustrativeFunding
    {
        /// <summary>
        /// Gets or sets the <c>TotalNffFunding</c> value.
        /// </summary>
        long? TotalNffFunding
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>PercentageChangeComparedToBaseline</c> value.
        /// </summary>
        double? PercentageChangeComparedToBaseline
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>PercentageChangeInPupilLedFunding</c> value.
        /// </summary>
        double? PercentageChangeInPupilLedFunding
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>TotalNffFundingIfFullyImplemented</c> value.
        /// </summary>
        long? TotalNffFundingIfFullyImplemented
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the
        /// <c>NewAndGrowingSchoolsTotalNffFundingIfFullyImplemented</c> value.
        /// </summary>
        long? NewAndGrowingSchoolsTotalNffFundingIfFullyImplemented
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the
        /// <c>PercentageChangeComparedToBaselineIfFullyImplemented</c> value.
        /// </summary>
        double? PercentageChangeComparedToBaselineIfFullyImplemented
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the
        /// <c>PercentageChangeInPupilLedFundingIfFullyImplemented</c> value.
        /// </summary>
        double? PercentageChangeInPupilLedFundingIfFullyImplemented
        {
            get;
            set;
        }
    }
}
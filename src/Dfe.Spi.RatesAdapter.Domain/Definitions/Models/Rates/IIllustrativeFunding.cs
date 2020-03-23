namespace Dfe.Spi.RatesAdapter.Domain.Definitions.Models.Rates
{
    /// <summary>
    /// Describes the properties of a illustrative funding model.
    /// </summary>
    public interface IIllustrativeFunding
    {
        /// <summary>
        /// Gets or sets the <c>TotalNffFunding</c> value.
        /// Spreadsheets: 2018, 2019.
        /// </summary>
        long? TotalNffFunding
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>PercentageChangeComparedToBaseline</c> value.
        /// Spreadsheets: 2018, 2019.
        /// </summary>
        double? PercentageChangeComparedToBaseline
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the
        /// <c>NewAndGrowingSchoolsTotalNffFundingIfFullyImplemented</c> value.
        /// Spreadsheets: 2018, 2019.
        /// </summary>
        long? NewAndGrowingSchoolsTotalNffFundingIfFullyImplemented
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>TotalNffFundingIfFullyImplemented</c> value.
        /// Spreadsheets: 2018.
        /// </summary>
        long? TotalNffFundingIfFullyImplemented
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>PercentageChangeInPupilLedFunding</c> value.
        /// Spreadsheets: 2018.
        /// </summary>
        double? PercentageChangeInPupilLedFunding
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the
        /// <c>PercentageChangeComparedToBaselineIfFullyImplemented</c> value.
        /// Spreadsheets: 2018.
        /// </summary>
        double? PercentageChangeComparedToBaselineIfFullyImplemented
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the
        /// <c>PercentageChangeInPupilLedFundingIfFullyImplemented</c> value.
        /// Spreadsheets: 2018.
        /// </summary>
        double? PercentageChangeInPupilLedFundingIfFullyImplemented
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>TotalNffFundingIfFullyImplementedPerPupil</c> value.
        /// Spreadsheets: 2019.
        /// </summary>
        long? TotalNffFundingIfFullyImplementedPerPupil
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>PercentageChangeInPupilLedFundingPerPupil</c> value.
        /// Spreadsheets: 2019.
        /// </summary>
        double? PercentageChangeInPupilLedFundingPerPupil
        {
            get;
            set;
        }
    }
}
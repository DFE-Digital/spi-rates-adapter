namespace Dfe.Spi.RatesAdapter.Domain.Definitions.Models.Rates
{
    /// <summary>
    /// Describes the properties of a notional funding model.
    /// </summary>
    public interface INotionalFunding
    {
        /// <summary>
        /// Gets or sets the <c>TotalNffFunding</c> value.
        /// Spreadsheets: 2018, 2019, 2020.
        /// </summary>
        double? TotalNffFunding
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>PercentageChangeComparedToBaseline</c> value.
        /// Spreadsheets: 2018, 2019, 2020.
        /// </summary>
        double? PercentageChangeComparedToBaseline
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>PupilCount</c> value.
        /// Note: A double, because helpfully, on the 2020 spreadsheet, it is
        ///       a double (quite how that works is anyone's best guess).
        /// Spreadsheets: 2019, 2020.
        /// </summary>
        double? PupilCount
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>TotalNffFundingPerPupil</c> value.
        /// Spreadsheets: 2019, 2020.
        /// </summary>
        double? TotalNffFundingPerPupil
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>PercentageChangeInPupilLedFundingPerPupil</c>
        /// value.
        /// Spreadsheets: 2019, 2020.
        /// </summary>
        double? PercentageChangeInPupilLedFundingPerPupil
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
    }
}
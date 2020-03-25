namespace Dfe.Spi.RatesAdapter.Domain.Definitions.Models.Rates
{
    /// <summary>
    /// Describes the properties of the provisional funding model.
    /// </summary>
    public interface IProvisionalFunding
    {
        /// <summary>
        /// Gets or sets the <c>ActualPrimaryUnitOfFunding</c> value.
        /// Spreadsheets: 2018, 2019, 2020.
        /// </summary>
        double? ActualPrimaryUnitOfFunding
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>ActualSecondaryUnitOfFunding</c> value.
        /// Spreadsheets: 2018, 2019, 2020.
        /// </summary>
        double? ActualSecondaryUnitOfFunding
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>PrimaryPupilNumbers</c> value.
        /// Spreadsheets: 2018, 2019, 2020.
        /// NOTE: Spreadsheet 2018 contains a double for a value from this
        ///       column.
        /// </summary>
        double? PrimaryPupilNumbers
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>SecondaryPupilNumbers</c> value.
        /// Spreadsheets: 2018, 2019, 2020.
        /// NOTE: Spreadsheet 2018 contains a double for a value from this
        ///       column.
        /// </summary>
        double? SecondaryPupilNumbers
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the
        /// <c>ActualFundingThroughGrowthPremesisMobilityFactors</c> value.
        /// Spreadsheets: 2018.
        /// </summary>
        double? ActualFundingThroughGrowthPremesisMobilityFactors
        {
            get;
            set;
        }
    }
}
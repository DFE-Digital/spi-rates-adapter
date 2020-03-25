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
        /// <c>ActualFundingThroughPremesisMobilityGrowthFactors</c> value.
        /// Spreadsheets: 2018.
        /// </summary>
        double? ActualFundingThroughPremesisMobilityGrowthFactors
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>ActualFundingThroughPremesisMobilityFactors</c>
        /// value.
        /// Spreadsheets: 2019.
        /// </summary>
        double? ActualFundingThroughPremesisMobilityFactors
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>ActualFundingThroughPremesisFactors</c> value.
        /// Spreadsheets: 2020.
        /// </summary>
        double? ActualFundingThroughPremesisFactors
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>ProvisionalNffSchoolsBlockFunding</c> value.
        /// Spreadsheets: 2018, 2019.
        /// </summary>
        double? ProvisionalNffSchoolsBlockFunding
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>IllustrativeGrowthFunding</c> value.
        /// Spreadsheets: 2019.
        /// </summary>
        double? IllustrativeGrowthFunding
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>LocalAuthorityProtection</c> value.
        /// Spreadsheets: 2020.
        /// </summary>
        double? LocalAuthorityProtection
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the
        /// <c>ProvisionalNffSchoolsBlockFundingExcludingFundingThroughGrowthFactor</c>
        /// value.
        /// Spreadsheets: 2020.
        /// </summary>
        double? ProvisionalNffSchoolsBlockFundingExcludingFundingThroughGrowthFactor
        {
            get;
            set;
        }
    }
}
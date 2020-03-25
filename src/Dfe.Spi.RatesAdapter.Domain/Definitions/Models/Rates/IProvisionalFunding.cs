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
        /// Gets or sets the <c>NffSchoolsBlockFunding</c> value.
        /// Spreadsheets: 2018, 2019.
        /// </summary>
        double? NffSchoolsBlockFunding
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
        /// <c>NffSchoolsBlockFundingExcludingFundingThroughGrowthFactor</c>
        /// value.
        /// Spreadsheets: 2020.
        /// </summary>
        double? NffSchoolsBlockFundingExcludingFundingThroughGrowthFactor
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>ActualHighNeedsNffAllocations</c> value.
        /// Spreadsheets: 2018, 2019, 2020.
        /// </summary>
        double? ActualHighNeedsNffAllocations
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the
        /// <c>ActualAcaWeightedBasicEntitlementFactorUnitRate</c> value.
        /// Spreadsheets: 2018, 2019.
        /// </summary>
        double? ActualAcaWeightedBasicEntitlementFactorUnitRate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>NumberOfPupilsInSpecialSchoolsAcadamies</c>
        /// value.
        /// Spreadsheets: 2018, 2020.
        /// Note: This is a double on the 2020 spreadsheet (for some reason).
        /// </summary>
        double? NumberOfPupilsInSpecialSchoolsAcadamies
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>AcaWeightedBasicEntitlementUnitRate</c>
        /// value.
        /// Spreadsheets: 2020.
        /// </summary>
        double? AcaWeightedBasicEntitlementUnitRate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>BasicEntitlementFactor</c> value.
        /// Spreadsheets: 2020.
        /// </summary>
        double? BasicEntitlementFactor
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the
        /// <c>NumberOfPupilsInSpecialSchoolsAcadamiesIndependentSettings</c>
        /// value.
        /// Spreadsheets: 2019.
        /// Note: This value is a double in the 2019 spreadsheet.
        /// </summary>
        double? NumberOfPupilsInSpecialSchoolsAcadamiesIndependentSettings
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the
        /// <c>ImportExportAdjustmentsIncludingAdjustmentsToNewAndGrowingSpecialFreeSchools</c>
        /// value.
        /// Spreadsheets: 2020.
        /// </summary>
        double? ImportExportAdjustmentsIncludingAdjustmentsToNewAndGrowingSpecialFreeSchools
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the
        /// <c>HospitalEducationFundingWithEightPercentUplift</c> value.
        /// Spreadsheets: 2020.
        /// </summary>
        double? HospitalEducationFundingWithEightPercentUplift
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>ActualImportExportAdjustmentUnitRate</c> value.
        /// Spreadsheets: 2018, 2019.
        /// </summary>
        double? ActualImportExportAdjustmentUnitRate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>NetNumberOfImportedPupils</c> value.
        /// Spreadsheets: 2018, 2019, 2020.
        /// Note: This is a double on the 2018 spreadsheet (for some reason).
        /// </summary>
        double? NetNumberOfImportedPupils
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the
        /// <c>AdditionalFundingForNewAndGrowingSpecialFreeSchools</c> value.
        /// Spreadsheets: 2020.
        /// </summary>
        double? AdditionalFundingForNewAndGrowingSpecialFreeSchools
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>HospitalEducationSpending</c> value.
        /// Spreadsheets: 2019.
        /// </summary>
        double? HospitalEducationSpending
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>NffHighNeedsBlockFunding</c> value.
        /// Spreadsheets: 2018, 2019, 2020.
        /// </summary>
        double? NffHighNeedsBlockFunding
        {
            get;
            set;
        }
    }
}
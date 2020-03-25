namespace Dfe.Spi.RatesAdapter.Domain.Models.Rates
{
    using Dfe.Spi.RatesAdapter.Domain.Definitions.Models.Rates;

    /// <summary>
    /// Implements <see cref="IProvisionalFunding" />.
    /// </summary>
    public class ProvisionalFunding : RatesBase, IProvisionalFunding
    {
        /// <inheritdoc />
        public double? ActualPrimaryUnitOfFunding
        {
            get;
            set;
        }

        /// <inheritdoc />
        public double? ActualSecondaryUnitOfFunding
        {
            get;
            set;
        }

        /// <inheritdoc />
        public double? PrimaryPupilNumbers
        {
            get;
            set;
        }

        /// <inheritdoc />
        public double? SecondaryPupilNumbers
        {
            get;
            set;
        }

        /// <inheritdoc />
        public double? ActualFundingThroughPremesisMobilityGrowthFactors
        {
            get;
            set;
        }

        /// <inheritdoc />
        public double? ActualFundingThroughPremesisMobilityFactors
        {
            get;
            set;
        }

        /// <inheritdoc />
        public double? ActualFundingThroughPremesisFactors
        {
            get;
            set;
        }

        /// <inheritdoc />
        public double? NffSchoolsBlockFunding
        {
            get;
            set;
        }

        /// <inheritdoc />
        public double? IllustrativeGrowthFunding
        {
            get;
            set;
        }

        /// <inheritdoc />
        public double? LocalAuthorityProtection
        {
            get;
            set;
        }

        /// <inheritdoc />
        public double? NffSchoolsBlockFundingExcludingFundingThroughGrowthFactor
        {
            get;
            set;
        }

        /// <inheritdoc />
        public double? ActualHighNeedsNffAllocations
        {
            get;
            set;
        }

        /// <inheritdoc />
        public double? ActualAcaWeightedBasicEntitlementFactorUnitRate
        {
            get;
            set;
        }

        /// <inheritdoc />
        public double? NumberOfPupilsInSpecialSchoolsAcadamies
        {
            get;
            set;
        }

        /// <inheritdoc />
        public double? AcaWeightedBasicEntitlementUnitRate
        {
            get;
            set;
        }

        /// <inheritdoc />
        public double? BasicEntitlementFactor
        {
            get;
            set;
        }

        /// <inheritdoc />
        public double? NumberOfPupilsInSpecialSchoolsAcadamiesIndependentSettings
        {
            get;
            set;
        }

        /// <inheritdoc />
        public double? ImportExportAdjustmentsIncludingAdjustmentsToNewAndGrowingSpecialFreeSchools
        {
            get;
            set;
        }

        /// <inheritdoc />
        public double? HospitalEducationFundingWithEightPercentUplift
        {
            get;
            set;
        }

        /// <inheritdoc />
        public double? ActualImportExportAdjustmentUnitRate
        {
            get;
            set;
        }

        /// <inheritdoc />
        public double? NetNumberOfImportedPupils
        {
            get;
            set;
        }

        /// <inheritdoc />
        public double? AdditionalFundingForNewAndGrowingSpecialFreeSchools
        {
            get;
            set;
        }

        /// <inheritdoc />
        public double? HospitalEducationSpending
        {
            get;
            set;
        }

        /// <inheritdoc />
        public double? NffHighNeedsBlockFunding
        {
            get;
            set;
        }
    }
}
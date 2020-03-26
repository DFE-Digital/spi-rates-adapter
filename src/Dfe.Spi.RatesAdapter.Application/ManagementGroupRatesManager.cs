namespace Dfe.Spi.RatesAdapter.Application
{
    using System.Threading;
    using System.Threading.Tasks;
    using Dfe.Spi.Models.Entities;
    using Dfe.Spi.Models.RatesModels.ManagementGroupModels;
    using Dfe.Spi.RatesAdapter.Application.Definitions;
    using Dfe.Spi.RatesAdapter.Domain.Definitions;
    using LocalDomainModels = Dfe.Spi.RatesAdapter.Domain.Models;

    /// <summary>
    /// Implements <see cref="IManagementGroupRatesManager" />.
    /// </summary>
    public class ManagementGroupRatesManager : IManagementGroupRatesManager
    {
        private readonly ILocalAuthorityInformationStorageAdapter localAuthorityInformationStorageAdapter;

        /// <summary>
        /// Initialises a new instance of the
        /// <see cref="ManagementGroupRatesManager" /> class.
        /// </summary>
        /// <param name="localAuthorityInformationStorageAdapter">
        /// An instance of type
        /// <see cref="ILocalAuthorityInformationStorageAdapter" />.
        /// </param>
        public ManagementGroupRatesManager(
            ILocalAuthorityInformationStorageAdapter localAuthorityInformationStorageAdapter)
        {
            this.localAuthorityInformationStorageAdapter = localAuthorityInformationStorageAdapter;
        }

        /// <inheritdoc />
        public async Task<ManagementGroupRates> GetManagementGroupRatesAsync(
            int year,
            short laNumber,
            CancellationToken cancellationToken)
        {
            ManagementGroupRates toReturn = null;

            LocalDomainModels.LocalAuthorityInformation localAuthorityInformation =
                await this.localAuthorityInformationStorageAdapter.GetLocalAuthorityInformationAsync(
                    year,
                    laNumber,
                    cancellationToken)
                    .ConfigureAwait(false);

            toReturn = Map(localAuthorityInformation);

            return toReturn;
        }

        private static ManagementGroupRates Map(
            LocalDomainModels.LocalAuthorityInformation localAuthorityInformation)
        {
            ManagementGroupRates toReturn = null;

            ProvisionalFunding provisionalFunding =
                Map(localAuthorityInformation.ProvisionalFunding);

            toReturn = new ManagementGroupRates()
            {
                ProvisionalFunding = provisionalFunding,
            };

            return toReturn;
        }

        private static ProvisionalFunding Map(
            LocalDomainModels.Rates.ProvisionalFunding provisionalFunding)
        {
            ProvisionalFunding toReturn = new ProvisionalFunding()
            {
                AcaWeightedBasicEntitlementUnitRate = provisionalFunding.AcaWeightedBasicEntitlementUnitRate,
                ActualAcaWeightedBasicEntitlementFactorUnitRate = provisionalFunding.ActualAcaWeightedBasicEntitlementFactorUnitRate,
                ActualCssbUnitOfFunding = provisionalFunding.ActualCssbUnitOfFunding,
                ActualCssbUnitOfFundingForOngoingFunctions = provisionalFunding.ActualCssbUnitOfFundingForOngoingFunctions,
                ActualFundingForHistoricCommitments = provisionalFunding.ActualFundingForHistoricCommitments,
                ActualFundingThroughPremesisFactors = provisionalFunding.ActualFundingThroughPremesisFactors,
                ActualFundingThroughPremesisMobilityFactors = provisionalFunding.ActualFundingThroughPremesisMobilityFactors,
                ActualFundingThroughPremesisMobilityGrowthFactors = provisionalFunding.ActualFundingThroughPremesisMobilityGrowthFactors,
                ActualHighNeedsNffAllocations = provisionalFunding.ActualHighNeedsNffAllocations,
                ActualImportExportAdjustmentUnitRate = provisionalFunding.ActualImportExportAdjustmentUnitRate,
                ActualPrimaryUnitOfFunding = provisionalFunding.ActualPrimaryUnitOfFunding,
                ActualSecondaryUnitOfFunding = provisionalFunding.ActualSecondaryUnitOfFunding,
                AdditionalFundingForNewAndGrowingSpecialFreeSchools = provisionalFunding.AdditionalFundingForNewAndGrowingSpecialFreeSchools,
                BasicEntitlementFactor = provisionalFunding.BasicEntitlementFactor,
                HospitalEducationFundingWithEightPercentUplift = provisionalFunding.HospitalEducationFundingWithEightPercentUplift,
                HospitalEducationSpending = provisionalFunding.HospitalEducationSpending,
                IllustrativeGrowthFunding = provisionalFunding.IllustrativeGrowthFunding,
                ImportExportAdjustmentsIncludingAdjustmentsToNewAndGrowingSpecialFreeSchools = provisionalFunding.ImportExportAdjustmentsIncludingAdjustmentsToNewAndGrowingSpecialFreeSchools,
                LocalAuthorityProtection = provisionalFunding.LocalAuthorityProtection,
                NetNumberOfImportedPupils = provisionalFunding.NetNumberOfImportedPupils,
                NffAllocationsForSchoolsHighNeedsAndCentralSchoolServicesBlocks = provisionalFunding.NffAllocationsForSchoolsHighNeedsAndCentralSchoolServicesBlocks,
                NffCssbFunding = provisionalFunding.NffCssbFunding,
                NffHighNeedsBlockFunding = provisionalFunding.NffHighNeedsBlockFunding,
                NffSchoolsBlockFunding = provisionalFunding.NffSchoolsBlockFunding,
                NffSchoolsBlockFundingExcludingFundingThroughGrowthFactor = provisionalFunding.NffSchoolsBlockFundingExcludingFundingThroughGrowthFactor,
                NumberOfPupilsInSpecialSchoolsAcadamies = provisionalFunding.NumberOfPupilsInSpecialSchoolsAcadamies,
                NumberOfPupilsInSpecialSchoolsAcadamiesIndependentSettings = provisionalFunding.NumberOfPupilsInSpecialSchoolsAcadamiesIndependentSettings,
                PrimaryPupilNumbers = provisionalFunding.PrimaryPupilNumbers,
                PupilNumbers = provisionalFunding.PupilNumbers,
                PupilNumbersSchoolsBlockDsgDuplicatesApportioned = provisionalFunding.PupilNumbersSchoolsBlockDsgDuplicatesApportioned,
                SecondaryPupilNumbers = provisionalFunding.SecondaryPupilNumbers,
            };

            return toReturn;
        }
    }
}
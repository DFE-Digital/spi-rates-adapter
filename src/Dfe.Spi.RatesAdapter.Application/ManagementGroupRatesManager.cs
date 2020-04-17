using System.Linq;
using Dfe.Spi.Models.Extensions;
using Dfe.Spi.RatesAdapter.Application.Models;
using Dfe.Spi.RatesAdapter.Domain.Exceptions;

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
            string fields,
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

            if (!string.IsNullOrEmpty(fields))
            {
                toReturn = toReturn.Pick(fields);
            }

            return toReturn;
        }

        /// <inheritdoc />
        public async Task<ManagementGroupRates[]> GetManagementGroupsRatesAsync(
            ManagementGroupYearPointer[] managementGroupYearPointers, 
            string[] fields, 
            CancellationToken cancellationToken)
        {
            var fieldsString = fields == null || fields.Length == 0
                ? null
                : fields.Aggregate((x, y) => $"{x},{y}");
            var learningProvidersRates = await Task.WhenAll(managementGroupYearPointers.Select(async (pointer) =>
            {
                try
                {
                    return await GetManagementGroupRatesAsync(pointer.Year, pointer.LaNumber, fieldsString, CancellationToken.None);
                }
                catch (RatesNotFoundException)
                {
                    return null;
                }
            }));

            return learningProvidersRates;
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
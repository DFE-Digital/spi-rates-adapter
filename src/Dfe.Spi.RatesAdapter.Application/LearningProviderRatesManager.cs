using System.Linq;
using System.Security.Cryptography;
using Dfe.Spi.Models.Extensions;
using Dfe.Spi.RatesAdapter.Application.Models;

namespace Dfe.Spi.RatesAdapter.Application
{
    using System.Threading;
    using System.Threading.Tasks;
    using Dfe.Spi.Models.Entities;
    using Dfe.Spi.Models.RatesModels.LearningProviderModels;
    using Dfe.Spi.RatesAdapter.Application.Definitions;
    using Dfe.Spi.RatesAdapter.Domain.Definitions;
    using LocalDomainModels = Dfe.Spi.RatesAdapter.Domain.Models;

    /// <summary>
    /// Implements <see cref="ILearningProviderRatesManager" />.
    /// </summary>
    public class LearningProviderRatesManager : ILearningProviderRatesManager
    {
        private readonly ISchoolInformationStorageAdapter schoolInformationStorageAdapter;

        /// <summary>
        /// Initialises a new instance of the <see cref="LearningProviderRatesManager" />
        /// class.
        /// </summary>
        /// <param name="schoolInformationStorageAdapter">
        /// An instance of type
        /// <see cref="ISchoolInformationStorageAdapter" />.
        /// </param>
        public LearningProviderRatesManager(
            ISchoolInformationStorageAdapter schoolInformationStorageAdapter)
        {
            this.schoolInformationStorageAdapter = schoolInformationStorageAdapter;
        }

        /// <inheritdoc />
        public async Task<LearningProviderRates> GetLearningProviderRatesAsync(
            int year,
            long urn,
            string fields,
            CancellationToken cancellationToken)
        {
            LearningProviderRates toReturn = null;

            LocalDomainModels.SchoolInformation schoolInformation =
                await this.schoolInformationStorageAdapter.GetSchoolInformationAsync(
                    year,
                    urn,
                    cancellationToken)
                    .ConfigureAwait(false);

            toReturn = Map(schoolInformation);
            
            if (!string.IsNullOrEmpty(fields))
            {
                toReturn = toReturn.Pick(fields);
            }

            return toReturn;
        }

        public async Task<LearningProviderRates[]> GetLearningProvidersRatesAsync(
            LearningProviderYearPointer[] learningProviderYearPointers, 
            string[] fields,
            CancellationToken cancellationToken)
        {
            var fieldsString = fields == null || fields.Length == 0
                ? null
                : fields.Aggregate((x, y) => $"{x},{y}");
            var learningProvidersRates = await Task.WhenAll(learningProviderYearPointers.Select(pointer =>
                GetLearningProviderRatesAsync(pointer.Year, pointer.Urn, fieldsString, CancellationToken.None)));

            return learningProvidersRates;
        }


        private static LearningProviderRates Map(
            LocalDomainModels.SchoolInformation schoolInformation)
        {
            LearningProviderRates toReturn = null;

            BaselineFunding baselineFunding =
                Map(schoolInformation.BaselineFunding);
            NotionalFunding notionalFunding =
                Map(schoolInformation.NotionalFunding);

            IllustrativeFunding illustrativeFunding = null;
            if (schoolInformation.IllustrativeFunding != null)
            {
                illustrativeFunding =
                    Map(schoolInformation.IllustrativeFunding);
            }

            toReturn = new LearningProviderRates()
            {
                BaselineFunding = baselineFunding,
                IllustrativeFunding = illustrativeFunding,
                NotionalFunding = notionalFunding,
            };

            return toReturn;
        }

        private static NotionalFunding Map(
            LocalDomainModels.Rates.NotionalFunding notionalFunding)
        {
            NotionalFunding toReturn = new NotionalFunding()
            {
                PercentageChangeComparedToBaseline = notionalFunding.PercentageChangeComparedToBaseline,
                PercentageChangeInPupilLedFunding = notionalFunding.PercentageChangeInPupilLedFunding,
                PercentageChangeInPupilLedFundingPerPupil = notionalFunding.PercentageChangeInPupilLedFundingPerPupil,
                PupilCount = notionalFunding.PupilCount,
                TotalNffFunding = notionalFunding.TotalNffFunding,
                TotalNffFundingPerPupil = notionalFunding.TotalNffFundingPerPupil,
            };

            return toReturn;
        }

        private static IllustrativeFunding Map(
            LocalDomainModels.Rates.IllustrativeFunding illustrativeFunding)
        {
            IllustrativeFunding toReturn = new IllustrativeFunding()
            {
                NewAndGrowingSchoolsTotalNffFundingIfFullyImplemented = illustrativeFunding.NewAndGrowingSchoolsTotalNffFundingIfFullyImplemented,
                PercentageChangeComparedToBaseline = illustrativeFunding.PercentageChangeComparedToBaseline,
                PercentageChangeComparedToBaselineIfFullyImplemented = illustrativeFunding.PercentageChangeComparedToBaseline,
                PercentageChangeInPupilLedFunding = illustrativeFunding.PercentageChangeInPupilLedFunding,
                PercentageChangeInPupilLedFundingIfFullyImplemented = illustrativeFunding.PercentageChangeInPupilLedFundingIfFullyImplemented,
                PercentageChangeInPupilLedFundingPerPupil = illustrativeFunding.PercentageChangeInPupilLedFundingPerPupil,
                TotalNffFunding = illustrativeFunding.TotalNffFunding,
                TotalNffFundingIfFullyImplemented = illustrativeFunding.TotalNffFundingIfFullyImplemented,
                TotalNffFundingIfFullyImplementedPerPupil = illustrativeFunding.TotalNffFundingIfFullyImplementedPerPupil,
            };

            return toReturn;
        }

        private static BaselineFunding Map(
            LocalDomainModels.Rates.BaselineFunding baselineFunding)
        {
            BaselineFunding toReturn = new BaselineFunding()
            {
                BaselineFundingFullSchool = baselineFunding.BaselineFundingFullSchool,
                NewAndGrowingSchoolsPupilCountIfFull = baselineFunding.NewAndGrowingSchoolsPupilCountIfFull,
                NewAndGrowingSchoolsValueIfFull = baselineFunding.NewAndGrowingSchoolsValueIfFull,
                NewAndGrowingSchoolsValuePerPupilIfFull = baselineFunding.NewAndGrowingSchoolsValuePerPupilIfFull,
                PupilCount = baselineFunding.PupilCount,
                Value = baselineFunding.Value,
                ValuePerPupil = baselineFunding.ValuePerPupil,
            };

            return toReturn;
        }
    }
}
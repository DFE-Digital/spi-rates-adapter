namespace Dfe.Spi.RatesAdapter.Application
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Dfe.Spi.Models.Entities;
    using Dfe.Spi.Models.RatesModels;
    using Dfe.Spi.RatesAdapter.Application.Definitions;
    using Dfe.Spi.RatesAdapter.Domain.Definitions;
    using LocalDomainModels = Dfe.Spi.RatesAdapter.Domain.Models;

    /// <summary>
    /// Implements <see cref="IRatesManager" />.
    /// </summary>
    public class RatesManager : IRatesManager
    {
        private readonly ISchoolInformationStorageAdapter schoolInformationStorageAdapter;

        /// <summary>
        /// Initialises a new instance of the <see cref="RatesManager" />
        /// class.
        /// </summary>
        /// <param name="schoolInformationStorageAdapter">
        /// An instance of type
        /// <see cref="ISchoolInformationStorageAdapter" />.
        /// </param>
        public RatesManager(
            ISchoolInformationStorageAdapter schoolInformationStorageAdapter)
        {
            this.schoolInformationStorageAdapter = schoolInformationStorageAdapter;
        }

        /// <inheritdoc />
        public async Task<Rates> GetRatesAsync(
            int year,
            string entityName,
            string identifier,
            CancellationToken cancellationToken)
        {
            Rates toReturn = null;

            if (entityName == nameof(LearningProvider))
            {
                long urn;
                if (!long.TryParse(identifier, out urn))
                {
                    // TODO: Throw some sort of exception.
                }

                LocalDomainModels.SchoolInformation schoolInformation =
                    await this.schoolInformationStorageAdapter.GetSchoolInformationAsync(
                        year,
                        urn,
                        cancellationToken)
                        .ConfigureAwait(false);

                toReturn = Map(schoolInformation);
            }
            else
            {
                throw new NotImplementedException(
                    "Not yet done! Implement me!");
            }

            return toReturn;
        }

        private static Rates Map(
            LocalDomainModels.SchoolInformation schoolInformation)
        {
            Rates toReturn = null;

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

            toReturn = new Rates()
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
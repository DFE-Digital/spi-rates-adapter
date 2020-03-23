namespace Dfe.Spi.RatesAdapter.Domain.Definitions.Models.Rates
{
    /// <summary>
    /// Describes the properties of a baseline funding model.
    /// </summary>
    public interface IBaselineFunding
    {
        /// <summary>
        /// Gets or sets the <c>BaselineFunding</c> value.
        /// Spreadsheets: 2018, 2019.
        /// </summary>
        long? Value
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>BaselineFundingFullSchool</c> value.
        /// Spreadsheets: 2018.
        /// </summary>
        long? BaselineFundingFullSchool
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>PupilCount</c> value.
        /// Spreadsheets: 2019.
        /// </summary>
        int? PupilCount
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>NewAndGrowingSchoolsPupilCountIfFull</c> value.
        /// Spreadsheets: 2019.
        /// </summary>
        int? NewAndGrowingSchoolsPupilCountIfFull
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>NewAndGrowingSchoolsValueIfFull</c> value.
        /// Spreadsheets: 2019.
        /// </summary>
        long? NewAndGrowingSchoolsValueIfFull
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>ValuePerPupil</c> value.
        /// Spreadsheets: 2019.
        /// </summary>
        long? ValuePerPupil
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the <c>NewAndGrowingSchoolsValuePerPupilIfFull</c>
        /// value.
        /// Spreadsheets: 2019.
        /// </summary>
        long? NewAndGrowingSchoolsValuePerPupilIfFull
        {
            get;
            set;
        }
    }
}
﻿namespace Dfe.Spi.RatesAdapter.Domain.Models.Rates
{
    using Dfe.Spi.RatesAdapter.Domain.Definitions.Models.Rates;

    /// <summary>
    /// Implements <see cref="IBaselineFunding" />.
    /// </summary>
    public class BaselineFunding : RatesBase, IBaselineFunding
    {
        /// <inheritdoc />
        public double? Value
        {
            get;
            set;
        }

        /// <inheritdoc />
        public long? BaselineFundingFullSchool
        {
            get;
            set;
        }

        /// <inheritdoc />
        public double? PupilCount
        {
            get;
            set;
        }

        /// <inheritdoc />
        public int? NewAndGrowingSchoolsPupilCountIfFull
        {
            get;
            set;
        }

        /// <inheritdoc />
        public long? NewAndGrowingSchoolsValueIfFull
        {
            get;
            set;
        }

        /// <inheritdoc />
        public long? ValuePerPupil
        {
            get;
            set;
        }

        /// <inheritdoc />
        public long? NewAndGrowingSchoolsValuePerPupilIfFull
        {
            get;
            set;
        }
    }
}
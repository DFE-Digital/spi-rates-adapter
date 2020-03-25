﻿namespace Dfe.Spi.RatesAdapter.Domain.Models.Rates
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
        public double? ProvisionalNffSchoolsBlockFunding
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
        public double? ProvisionalNffSchoolsBlockFundingExcludingFundingThroughGrowthFactor
        {
            get;
            set;
        }
    }
}
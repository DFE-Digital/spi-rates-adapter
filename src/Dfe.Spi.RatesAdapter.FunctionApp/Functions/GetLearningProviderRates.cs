namespace Dfe.Spi.RatesAdapter.FunctionApp.Functions
{
    using System;
    using System.Globalization;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using Dfe.Spi.Common.Http.Server.Definitions;
    using Dfe.Spi.Common.Logging.Definitions;
    using Dfe.Spi.Common.Models;
    using Dfe.Spi.Models.Entities;
    using Dfe.Spi.RatesAdapter.Application.Definitions;
    using Dfe.Spi.RatesAdapter.Domain.Exceptions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Newtonsoft.Json;

    /// <summary>
    /// Entry class for the <c>get-learning-provider-rates</c> function.
    /// </summary>
    public class GetLearningProviderRates : FunctionsBase
    {
        private readonly ILoggerWrapper loggerWrapper;
        private readonly IHttpErrorBodyResultProvider httpErrorBodyResultProvider;
        private readonly IHttpSpiExecutionContextManager httpSpiExecutionContextManager;
        private readonly ILearningProviderRatesManager learningProviderRatesManager;

        /// <summary>
        /// Initialises a new instance of the
        /// <see cref="GetLearningProviderRates" /> class.
        /// </summary>
        /// <param name="loggerWrapper">
        /// An instance of type <see cref="ILoggerWrapper" />.
        /// </param>
        /// <param name="httpErrorBodyResultProvider">
        /// An instance of type <see cref="IHttpErrorBodyResultProvider" />.
        /// </param>
        /// <param name="httpSpiExecutionContextManager">
        /// An instance of type <see cref="IHttpSpiExecutionContextManager" />.
        /// </param>
        /// <param name="learningProviderRatesManager">
        /// An instance of type <see cref="ILearningProviderRatesManager" />.
        /// </param>
        public GetLearningProviderRates(
            ILoggerWrapper loggerWrapper,
            IHttpErrorBodyResultProvider httpErrorBodyResultProvider,
            IHttpSpiExecutionContextManager httpSpiExecutionContextManager,
            ILearningProviderRatesManager learningProviderRatesManager)
            : base(loggerWrapper)
        {
            this.loggerWrapper = loggerWrapper;
            this.httpErrorBodyResultProvider = httpErrorBodyResultProvider;
            this.httpSpiExecutionContextManager = httpSpiExecutionContextManager;
            this.learningProviderRatesManager = learningProviderRatesManager;
        }

        /// <summary>
        /// Entry method for the <c>get-learning-provider-rates</c> function.
        /// </summary>
        /// <param name="httpRequest">
        /// An instance of <see cref="HttpContext" />.
        /// </param>
        /// <param name="id">
        /// The id of the <see cref="LearningProviderRates" /> instance.
        /// </param>
        /// <param name="cancellationToken">
        /// An instance of <see cref="CancellationToken" />.
        /// </param>
        /// <returns>
        /// An instance of type <see cref="IActionResult" />.
        /// </returns>
        [FunctionName("get-learning-provider-rates")]
        public async Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "learning-provider-rates/{id}")]
            HttpRequest httpRequest,
            string id,
            CancellationToken cancellationToken)
        {
            IActionResult toReturn = null;

            if (httpRequest == null)
            {
                throw new ArgumentNullException(nameof(httpRequest));
            }

            if (string.IsNullOrEmpty(id))
            {
                // id can't be null, as asp.net core's routing will reutrn a
                // 404. so we can guarentee there'll be something there
                // (in any normal/non unit test case).
                throw new ArgumentNullException(nameof(id));
            }

            IHeaderDictionary headerDictionary = httpRequest.Headers;

            this.httpSpiExecutionContextManager.SetContext(headerDictionary);

            string[] idParts = id.Split(
                new char[] { '-' },
                StringSplitOptions.RemoveEmptyEntries);

            if (idParts.Length != 2)
            {
                toReturn = this.httpErrorBodyResultProvider.GetHttpErrorBodyResult(
                    HttpStatusCode.BadRequest,
                    1);
            }

            if (toReturn == null)
            {
                string yearStr = idParts[0];
                string urnStr = idParts[1];

                int year = default(int);
                if (!int.TryParse(yearStr, out year))
                {
                    toReturn =
                        this.httpErrorBodyResultProvider.GetHttpErrorBodyResult(
                            HttpStatusCode.BadRequest,
                            2,
                            yearStr);
                }

                long urn = default(long);
                if (!long.TryParse(urnStr, out urn))
                {
                    toReturn =
                        this.httpErrorBodyResultProvider.GetHttpErrorBodyResult(
                            HttpStatusCode.BadRequest,
                            4,
                            urnStr);
                }

                if (toReturn == null)
                {
                    toReturn = await this.ExecuteValidatedRequestAsync(
                        year,
                        urn,
                        cancellationToken)
                        .ConfigureAwait(false);
                }
            }

            this.LogOutgoingErrors(toReturn);

            return toReturn;
        }

        private async Task<IActionResult> ExecuteValidatedRequestAsync(
            int year,
            long urn,
            CancellationToken cancellationToken)
        {
            IActionResult toReturn = null;

            try
            {
                LearningProviderRates learningProviderRates =
                    await this.learningProviderRatesManager.GetLearningProviderRatesAsync(
                        year,
                        urn,
                        cancellationToken)
                        .ConfigureAwait(false);

                learningProviderRates.Name =
                    $"Learning Provider Rates for year {year} " +
                    $"({nameof(urn)}: {urn})";

                JsonSerializerSettings jsonSerializerSettings =
                    JsonConvert.DefaultSettings();

                if (jsonSerializerSettings == null)
                {
                    toReturn = new JsonResult(learningProviderRates);
                }
                else
                {
                    toReturn = new JsonResult(
                        learningProviderRates,
                        jsonSerializerSettings);
                }
            }
            catch (RatesNotFoundException)
            {
                toReturn =
                    this.httpErrorBodyResultProvider.GetHttpErrorBodyResult(
                        HttpStatusCode.NotFound,
                        3,
                        year.ToString(CultureInfo.InvariantCulture),
                        urn.ToString(CultureInfo.InvariantCulture));
            }

            return toReturn;
        }
    }
}
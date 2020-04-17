namespace Dfe.Spi.RatesAdapter.FunctionApp.Functions
{
    using System;
    using System.Globalization;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;
    using Dfe.Spi.Common.Http.Server.Definitions;
    using Dfe.Spi.Common.Logging.Definitions;
    using Dfe.Spi.Models.Entities;
    using Dfe.Spi.Models.Extensions;
    using Dfe.Spi.RatesAdapter.Application.Definitions;
    using Dfe.Spi.RatesAdapter.Domain.Exceptions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Newtonsoft.Json;

    /// <summary>
    /// Entry class for the <c>get-management-group-rates</c> function.
    /// </summary>
    public class GetManagementGroupRates : FunctionsBase
    {
        private readonly ILoggerWrapper loggerWrapper;
        private readonly IHttpErrorBodyResultProvider httpErrorBodyResultProvider;
        private readonly IHttpSpiExecutionContextManager httpSpiExecutionContextManager;
        private readonly IManagementGroupRatesManager managementGroupRatesManager;

        /// <summary>
        /// Initialises a new instance of the
        /// <see cref="GetManagementGroupRates" /> class.
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
        /// <param name="managementGroupRatesManager">
        /// An instance of type <see cref="IManagementGroupRatesManager" />.
        /// </param>
        public GetManagementGroupRates(
            ILoggerWrapper loggerWrapper,
            IHttpErrorBodyResultProvider httpErrorBodyResultProvider,
            IHttpSpiExecutionContextManager httpSpiExecutionContextManager,
            IManagementGroupRatesManager managementGroupRatesManager)
            : base(loggerWrapper)
        {
            this.loggerWrapper = loggerWrapper;
            this.httpErrorBodyResultProvider = httpErrorBodyResultProvider;
            this.httpSpiExecutionContextManager = httpSpiExecutionContextManager;
            this.managementGroupRatesManager = managementGroupRatesManager;
        }

        /// <summary>
        /// Entry method for the <c>get-management-group-rates</c> function.
        /// </summary>
        /// <param name="httpRequest">
        /// An instance of <see cref="HttpContext" />.
        /// </param>
        /// <param name="id">
        /// The id of the <see cref="ManagementGroupRates" /> instance.
        /// </param>
        /// <param name="cancellationToken">
        /// An instance of <see cref="CancellationToken" />.
        /// </param>
        /// <returns>
        /// An instance of type <see cref="IActionResult" />.
        /// </returns>
        [FunctionName("get-management-group-rates")]
        public async Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "management-group-rates/{id}")]
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
                    5);
            }

            if (toReturn == null)
            {
                string yearStr = idParts[0];
                string laNumberStr = idParts[1];

                int year = default(int);
                if (!int.TryParse(yearStr, out year))
                {
                    toReturn =
                        this.httpErrorBodyResultProvider.GetHttpErrorBodyResult(
                            HttpStatusCode.BadRequest,
                            2,
                            yearStr);
                }

                short laNumber = default(short);
                if (!short.TryParse(laNumberStr, out laNumber))
                {
                    toReturn =
                        this.httpErrorBodyResultProvider.GetHttpErrorBodyResult(
                            HttpStatusCode.BadRequest,
                            7,
                            laNumberStr);
                }

                if (toReturn == null)
                {
                    string fields = httpRequest.Query["fields"];

                    toReturn = await this.ExecuteValidatedRequestAsync(
                        year,
                        laNumber,
                        fields,
                        cancellationToken)
                        .ConfigureAwait(false);
                }
            }

            this.LogOutgoingErrors(toReturn);

            return toReturn;
        }

        private async Task<IActionResult> ExecuteValidatedRequestAsync(
            int year,
            short laNumber,
            string fields,
            CancellationToken cancellationToken)
        {
            IActionResult toReturn = null;

            try
            {
                ManagementGroupRates managementGroupRates =
                    await this.managementGroupRatesManager.GetManagementGroupRatesAsync(
                        year,
                        laNumber,
                        fields,
                        cancellationToken)
                        .ConfigureAwait(false);

                managementGroupRates.Name =
                    $"Management Group Rates for year {year} " +
                    $"({nameof(laNumber)}: {laNumber})";

                JsonSerializerSettings jsonSerializerSettings =
                    JsonConvert.DefaultSettings();

                if (jsonSerializerSettings == null)
                {
                    toReturn = new JsonResult(managementGroupRates);
                }
                else
                {
                    toReturn = new JsonResult(
                        managementGroupRates,
                        jsonSerializerSettings);
                }
            }
            catch (RatesNotFoundException)
            {
                toReturn =
                    this.httpErrorBodyResultProvider.GetHttpErrorBodyResult(
                        HttpStatusCode.NotFound,
                        6,
                        year.ToString(CultureInfo.InvariantCulture),
                        laNumber.ToString(CultureInfo.InvariantCulture));
            }

            return toReturn;
        }
    }
}
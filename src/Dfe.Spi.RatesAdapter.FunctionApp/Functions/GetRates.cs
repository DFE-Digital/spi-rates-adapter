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
    /// Entry class for the <c>get-rates</c> function.
    /// </summary>
    public class GetRates
    {
        private readonly ILoggerWrapper loggerWrapper;
        private readonly IHttpErrorBodyResultProvider httpErrorBodyResultProvider;
        private readonly IHttpSpiExecutionContextManager httpSpiExecutionContextManager;
        private readonly IRatesManager ratesManager;

        /// <summary>
        /// Initialises a new instance of the
        /// <see cref="GetRates" /> class.
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
        /// <param name="ratesManager">
        /// An instance of type <see cref="IRatesManager" />.
        /// </param>
        public GetRates(
            ILoggerWrapper loggerWrapper,
            IHttpErrorBodyResultProvider httpErrorBodyResultProvider,
            IHttpSpiExecutionContextManager httpSpiExecutionContextManager,
            IRatesManager ratesManager)
        {
            this.loggerWrapper = loggerWrapper;
            this.httpErrorBodyResultProvider = httpErrorBodyResultProvider;
            this.httpSpiExecutionContextManager = httpSpiExecutionContextManager;
            this.ratesManager = ratesManager;
        }

        /// <summary>
        /// Entry method for the <c>get-rates</c> function.
        /// </summary>
        /// <param name="httpRequest">
        /// An instance of <see cref="HttpContext" />.
        /// </param>
        /// <param name="id">
        /// The id of the <see cref="Rates" /> instance.
        /// </param>
        /// <param name="cancellationToken">
        /// An instance of <see cref="CancellationToken" />.
        /// </param>
        /// <returns>
        /// An instance of type <see cref="IActionResult" />.
        /// </returns>
        [FunctionName("get-rates")]
        public async Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Function, "GET", Route = "rates/{id}")]
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

            if (idParts.Length != 3)
            {
                toReturn = this.httpErrorBodyResultProvider.GetHttpErrorBodyResult(
                    HttpStatusCode.BadRequest,
                    1);
            }

            if (toReturn == null)
            {
                string yearStr = idParts[0];
                string entityName = idParts[1];
                string identifier = idParts[2];

                int year = default(int);
                if (!int.TryParse(yearStr, out year))
                {
                    toReturn =
                        this.httpErrorBodyResultProvider.GetHttpErrorBodyResult(
                            HttpStatusCode.BadRequest,
                            2,
                            yearStr);
                }

                if (toReturn == null)
                {
                    toReturn = await this.ExecuteValidatedRequestAsync(
                        year,
                        entityName,
                        identifier,
                        cancellationToken)
                        .ConfigureAwait(false);
                }
            }

            this.LogOutgoingErrors(toReturn);

            return toReturn;
        }

        private void LogOutgoingErrors(IActionResult toReturn)
        {
            // Is it an error?
            JsonResult jsonResult = toReturn as JsonResult;
            if (jsonResult != null && jsonResult.Value is HttpErrorBody)
            {
                HttpErrorBody httpErrorBody =
                    jsonResult.Value as HttpErrorBody;

                this.loggerWrapper.Warning(
                    $"This request ended with a (handled) error being " +
                    $"returned: {httpErrorBody}.");
            }
        }

        private async Task<IActionResult> ExecuteValidatedRequestAsync(
            int year,
            string entityName,
            string identifier,
            CancellationToken cancellationToken)
        {
            IActionResult toReturn = null;

            try
            {
                Rates rates = await this.ratesManager.GetRatesAsync(
                    year,
                    entityName,
                    identifier,
                    cancellationToken)
                    .ConfigureAwait(false);

                rates.Name =
                    $"Rates for year {year}, for {entityName} ({identifier})";

                JsonSerializerSettings jsonSerializerSettings =
                    JsonConvert.DefaultSettings();

                if (jsonSerializerSettings == null)
                {
                    toReturn = new JsonResult(rates);
                }
                else
                {
                    toReturn = new JsonResult(rates, jsonSerializerSettings);
                }
            }
            catch (EntityNotImplementedException entityNotImplementedException)
            {
                toReturn =
                    this.httpErrorBodyResultProvider.GetHttpErrorBodyResult(
                        HttpStatusCode.NotImplemented,
                        3,
                        entityNotImplementedException.EntityName);
            }
            catch (InvalidIdentifierException invalidIdentifierException)
            {
                toReturn =
                    this.httpErrorBodyResultProvider.GetHttpErrorBodyResult(
                        HttpStatusCode.BadRequest,
                        4,
                        invalidIdentifierException.Value,
                        invalidIdentifierException.Name,
                        invalidIdentifierException.ExpectedTypeName);
            }
            catch (RatesNotFoundException ratesNotFoundException)
            {
                toReturn =
                    this.httpErrorBodyResultProvider.GetHttpErrorBodyResult(
                        HttpStatusCode.NotFound,
                        5,
                        ratesNotFoundException.IdentifierName,
                        ratesNotFoundException.IdentifierValue.ToString(),
                        year.ToString(CultureInfo.InvariantCulture));
            }

            return toReturn;
        }

        private IActionResult GetErrorBody(
            HttpStatusCode httpStatusCode,
            int errorId,
            Exception exception)
        {
            IActionResult toReturn = null;

            string message = exception.Message;

            Type exceptionType = exception.GetType();

            this.loggerWrapper.Error(
                $"An exception of type {exceptionType.Name} was thrown: " +
                $"{message}",
                exception);

            toReturn =
                this.httpErrorBodyResultProvider.GetHttpErrorBodyResult(
                    httpStatusCode,
                    errorId,
                    message);

            return toReturn;
        }
    }
}
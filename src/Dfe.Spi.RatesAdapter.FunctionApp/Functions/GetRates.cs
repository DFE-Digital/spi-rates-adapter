namespace Dfe.Spi.RatesAdapter.FunctionApp.Functions
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Dfe.Spi.Common.Http.Server.Definitions;
    using Dfe.Spi.Models.Entities;
    using Dfe.Spi.RatesAdapter.Application.Definitions;
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
        private readonly IHttpSpiExecutionContextManager httpSpiExecutionContextManager;
        private readonly IRatesManager ratesManager;

        /// <summary>
        /// Initialises a new instance of the
        /// <see cref="GetRates" /> class.
        /// </summary>
        /// <param name="httpSpiExecutionContextManager">
        /// An instance of type <see cref="IHttpSpiExecutionContextManager" />.
        /// </param>
        /// <param name="ratesManager">
        /// An instance of type <see cref="IRatesManager" />.
        /// </param>
        public GetRates(
            IHttpSpiExecutionContextManager httpSpiExecutionContextManager,
            IRatesManager ratesManager)
        {
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
                // TODO: Throw some sort of exception.
            }

            IHeaderDictionary headerDictionary = httpRequest.Headers;

            this.httpSpiExecutionContextManager.SetContext(headerDictionary);

            string[] idParts = id.Split(
                new char[] { '-' },
                StringSplitOptions.RemoveEmptyEntries);

            if (idParts.Length != 3)
            {
                // TODO: Throw some sort of exception.
            }

            string yearStr = idParts[0];

            int year;
            if (!int.TryParse(yearStr, out year))
            {
                // TODO: Throw an exception.
            }

            string entityName = idParts[1];
            string identifier = idParts[2];

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

            return toReturn;
        }
    }
}
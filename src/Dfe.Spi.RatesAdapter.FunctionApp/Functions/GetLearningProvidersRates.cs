using System;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Dfe.Spi.Common.Http.Server;
using Dfe.Spi.Common.Http.Server.Definitions;
using Dfe.Spi.Common.Logging.Definitions;
using Dfe.Spi.Common.Models;
using Dfe.Spi.RatesAdapter.Application.Definitions;
using Dfe.Spi.RatesAdapter.Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Dfe.Spi.RatesAdapter.FunctionApp.Functions
{
    public class GetLearningProvidersRates : FunctionsBase<GetLearningProvidersRatesRequest>
    {
        private readonly ILearningProviderRatesManager learningProviderRatesManager;
        private readonly IHttpErrorBodyResultProvider httpErrorBodyResultProvider;
        private readonly ILoggerWrapper loggerWrapper;

        public GetLearningProvidersRates(
            ILearningProviderRatesManager learningProviderRatesManager,
            IHttpErrorBodyResultProvider httpErrorBodyResultProvider,
            IHttpSpiExecutionContextManager httpSpiExecutionContextManager,
            ILoggerWrapper loggerWrapper)
            : base(
                httpSpiExecutionContextManager,
                loggerWrapper)
        {
            this.learningProviderRatesManager = learningProviderRatesManager;
            this.httpErrorBodyResultProvider = httpErrorBodyResultProvider;
            this.loggerWrapper = loggerWrapper;
        }
        
        [FunctionName("GetLearningProvidersRates")]
        public async Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "learning-provider-rates")]
            HttpRequest req,
            CancellationToken cancellationToken)
        {
            return await ValidateAndRunAsync(req, cancellationToken);
        }

        protected override HttpErrorBodyResult GetMalformedErrorResponse()
        {
            return this.httpErrorBodyResultProvider.GetHttpErrorBodyResult(
                HttpStatusCode.BadRequest,
                1);
        }

        protected override HttpErrorBodyResult GetSchemaValidationResponse(string message)
        {
            return this.httpErrorBodyResultProvider.GetHttpErrorBodyResult(
                HttpStatusCode.BadRequest,
                2,
                message);
        }

        protected override async Task<IActionResult> ProcessWellFormedRequestAsync(GetLearningProvidersRatesRequest request, CancellationToken cancellationToken)
        {
            var learningProviderYearPointers = new LearningProviderYearPointer[request.Identifiers.Length];
            for (var i = 0; i < learningProviderYearPointers.Length; i++)
            {
                var idParts = request.Identifiers[i].Split(
                    new char[] { '-' },
                    StringSplitOptions.RemoveEmptyEntries);

                if (idParts.Length != 2)
                {
                    return this.httpErrorBodyResultProvider.GetHttpErrorBodyResult(
                        HttpStatusCode.BadRequest,
                        1);
                }

                if (!int.TryParse(idParts[0], out var year))
                {
                    return this.httpErrorBodyResultProvider.GetHttpErrorBodyResult(
                            HttpStatusCode.BadRequest,
                            2,
                            idParts[0]);
                }

                if (!long.TryParse(idParts[1], out var urn))
                {
                    return this.httpErrorBodyResultProvider.GetHttpErrorBodyResult(
                            HttpStatusCode.BadRequest,
                            4,
                            idParts[1]);
                }

                learningProviderYearPointers[i] = new LearningProviderYearPointer
                {
                    Year = year,
                    Urn = urn,
                };
            }

            var learningProviderRates = await this.learningProviderRatesManager.GetLearningProvidersRatesAsync(
                learningProviderYearPointers, request.Fields, cancellationToken);
            

            JsonSerializerSettings jsonSerializerSettings =
                JsonConvert.DefaultSettings();

            if (jsonSerializerSettings == null)
            {
                return new JsonResult(learningProviderRates);
            }
            else
            {
                return new JsonResult(
                    learningProviderRates,
                    jsonSerializerSettings);
            }
        }
    }

    public class GetLearningProvidersRatesRequest : RequestResponseBase
    {
        public string[] Identifiers { get; set; }
        public string[] Fields { get; set; }
    }
}
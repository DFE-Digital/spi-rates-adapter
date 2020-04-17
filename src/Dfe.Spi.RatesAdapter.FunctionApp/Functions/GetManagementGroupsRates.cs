using System;
using System.Net;
using System.Threading;
using Dfe.Spi.Common.Http.Server;
using Dfe.Spi.RatesAdapter.Application.Models;

namespace Dfe.Spi.RatesAdapter.FunctionApp.Functions
{
    using System.IO;
    using System.Threading.Tasks;
    using Dfe.Spi.Common.Http.Server.Definitions;
    using Dfe.Spi.Common.Logging.Definitions;
    using Dfe.Spi.Common.Models;
    using Dfe.Spi.RatesAdapter.Application.Definitions;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Azure.WebJobs.Extensions.Http;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    
    public class GetManagementGroupsRates : FunctionsBase<GetManagementGroupsRatesRequest>
    {
        private readonly IManagementGroupRatesManager managementGroupRatesManager;
        private readonly IHttpErrorBodyResultProvider httpErrorBodyResultProvider;
        private readonly ILoggerWrapper loggerWrapper;

        public GetManagementGroupsRates(
            IManagementGroupRatesManager managementGroupRatesManager,
            IHttpErrorBodyResultProvider httpErrorBodyResultProvider,
            IHttpSpiExecutionContextManager httpSpiExecutionContextManager,
            ILoggerWrapper loggerWrapper)
            : base(
                httpSpiExecutionContextManager,
                loggerWrapper)
        {
            this.managementGroupRatesManager = managementGroupRatesManager;
            this.httpErrorBodyResultProvider = httpErrorBodyResultProvider;
            this.loggerWrapper = loggerWrapper;
        }
        
        [FunctionName("GetManagementGroupsRates")]
        public async Task<IActionResult> RunAsync(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "management-group-rates")]
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

        protected override async Task<IActionResult> ProcessWellFormedRequestAsync(GetManagementGroupsRatesRequest request, CancellationToken cancellationToken)
        {
            var managementGroupYearPointers = new ManagementGroupYearPointer[request.Identifiers.Length];
            for (var i = 0; i < managementGroupYearPointers.Length; i++)
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

                if (!short.TryParse(idParts[1], out var laNumber))
                {
                    return this.httpErrorBodyResultProvider.GetHttpErrorBodyResult(
                            HttpStatusCode.BadRequest,
                            4,
                            idParts[1]);
                }

                managementGroupYearPointers[i] = new ManagementGroupYearPointer
                {
                    Year = year,
                    LaNumber = laNumber,
                };
            }

            var learningProviderRates = await this.managementGroupRatesManager.GetManagementGroupsRatesAsync(
                managementGroupYearPointers, request.Fields, cancellationToken);
            

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

    public class GetManagementGroupsRatesRequest : RequestResponseBase
    {
        public string[] Identifiers { get; set; }
        public string[] Fields { get; set; }
    }
}
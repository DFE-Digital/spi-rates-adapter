namespace Dfe.Spi.RatesAdapter.FunctionApp
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Dfe.Spi.Common.Context.Definitions;
    using Dfe.Spi.Common.Http.Server;
    using Dfe.Spi.Common.Http.Server.Definitions;
    using Dfe.Spi.Common.Logging;
    using Dfe.Spi.Common.Logging.Definitions;
    using Dfe.Spi.RatesAdapter.Application;
    using Dfe.Spi.RatesAdapter.Application.Definitions;
    using Microsoft.Azure.Functions.Extensions.DependencyInjection;
    using Microsoft.Azure.WebJobs.Logging;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    /// <summary>
    /// Functions startup class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Startup : FunctionsStartup
    {
        private const string SystemErrorIdentifier = "RA";

        /// <inheritdoc />
        public override void Configure(
            IFunctionsHostBuilder functionsHostBuilder)
        {
            if (functionsHostBuilder == null)
            {
                throw new ArgumentNullException(nameof(functionsHostBuilder));
            }

            // camelCase, if you please.
            JsonConvert.DefaultSettings =
                () => new JsonSerializerSettings()
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    NullValueHandling = NullValueHandling.Ignore,
                };

            IServiceCollection serviceCollection =
                functionsHostBuilder.Services;

            HttpErrorBodyResultProvider httpErrorBodyResultProvider =
                new HttpErrorBodyResultProvider(
                    SystemErrorIdentifier,
                    HttpErrorMessages.ResourceManager);

            AddLogging(serviceCollection);
            AddManagers(serviceCollection);

            serviceCollection
                .AddSingleton<IHttpErrorBodyResultProvider>(httpErrorBodyResultProvider)
                .AddScoped<IHttpSpiExecutionContextManager, HttpSpiExecutionContextManager>()
                .AddScoped<ISpiExecutionContextManager>(x => x.GetService<IHttpSpiExecutionContextManager>());
        }

        private static void AddLogging(IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddScoped<ILogger>(CreateILogger)
                .AddScoped<ILoggerWrapper, LoggerWrapper>();
        }

        private static void AddManagers(IServiceCollection serviceCollection)
        {
            serviceCollection
                .AddScoped<IRatesManager, RatesManager>();
        }

        private static ILogger CreateILogger(IServiceProvider serviceProvider)
        {
            ILogger toReturn = null;

            ILoggerFactory loggerFactory =
                serviceProvider.GetService<ILoggerFactory>();

            string categoryName = LogCategories.CreateFunctionUserCategory(
                nameof(Dfe.Spi.RatesAdapter));

            toReturn = loggerFactory.CreateLogger(categoryName);

            return toReturn;
        }
    }
}
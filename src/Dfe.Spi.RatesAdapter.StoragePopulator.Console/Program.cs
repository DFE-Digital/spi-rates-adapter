namespace Dfe.Spi.RatesAdapter.StoragePopulator.ConsoleApp
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.Threading;
    using System.Threading.Tasks;
    using CommandLine;
    using Dfe.Spi.Common.Logging.Definitions;
    using Dfe.Spi.RatesAdapter.Domain.Definitions;
    using Dfe.Spi.RatesAdapter.Domain.Definitions.SettingsProviders;
    using Dfe.Spi.RatesAdapter.Infrastructure.AzureStorage;
    using Dfe.Spi.RatesAdapter.StoragePopulator.Application;
    using Dfe.Spi.RatesAdapter.StoragePopulator.Application.Definitions;
    using Dfe.Spi.RatesAdapter.StoragePopulator.Application.Definitions.Processors;
    using Dfe.Spi.RatesAdapter.StoragePopulator.Application.Models.SpreadsheetProcessorModels;
    using Dfe.Spi.RatesAdapter.StoragePopulator.ConsoleApp.Definitions;
    using Dfe.Spi.RatesAdapter.StoragePopulator.ConsoleApp.Models;
    using Dfe.Spi.RatesAdapter.StoragePopulator.ConsoleApp.SettingsProvider;
    using Dfe.Spi.RatesAdapter.StoragePopulator.Domain.Definitions;
    using Dfe.Spi.RatesAdapter.StoragePopulator.Infrastructure.Excel;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Main entry class for the console app.
    /// </summary>
    public class Program : IProgram
    {
        private readonly ISpreadsheetProcessor spreadsheetProcessor;

        /// <summary>
        /// Initialises a new instance of the <see cref="Program" /> class.
        /// </summary>
        /// <param name="spreadsheetProcessor">
        /// An instance of type <see cref="ISpreadsheetProcessor" />.
        /// </param>
        public Program(ISpreadsheetProcessor spreadsheetProcessor)
        {
            if (spreadsheetProcessor == null)
            {
                throw new ArgumentNullException(nameof(spreadsheetProcessor));
            }

            this.spreadsheetProcessor = spreadsheetProcessor;
            this.spreadsheetProcessor.InsertionProgressReported +=
                this.OnInsertionProgressReported;
        }

        /// <summary>
        /// Main entry method for the console app.
        /// </summary>
        /// <param name="args">
        /// The command line arguments.
        /// </param>
        /// <returns>
        /// An exit code for the application process.
        /// </returns>
        [ExcludeFromCodeCoverage]
        public static int Main(string[] args)
        {
            int toReturn = -1;

            Parser.Default
                .ParseArguments<Options>(args)
                .WithParsed(x =>
                {
                    toReturn = InvokeRun(x);
                });

            return toReturn;
        }

        /// <inheritdoc />
        public async Task<int> RunAsync(
            Options options,
            CancellationToken cancellationToken)
        {
            int toReturn = 0;

            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            ProcessRequest processRequest = Map(options);

            ProcessResponse processResponse =
                await this.spreadsheetProcessor.ProcessAsync(
                    processRequest,
                    cancellationToken)
                    .ConfigureAwait(false);

            return toReturn;
        }

        [ExcludeFromCodeCoverage]
        private static int InvokeRun(Options options)
        {
            int toReturn = -1;

            SchoolInformationStorageAdapterSettingsProvider schoolInformationStorageAdapterSettingsProvider =
                new SchoolInformationStorageAdapterSettingsProvider(
                    options.StorageConnectionString,
                    options.TableName);

            LocalAuthorityInformationStorageAdapterSettingsProvider localAuthorityInformationStorageAdapterSettingsProvider =
                new LocalAuthorityInformationStorageAdapterSettingsProvider(
                    options.StorageConnectionString,
                    options.TableName);

            using (ServiceProvider serviceProvider = CreateServiceProvider(schoolInformationStorageAdapterSettingsProvider, localAuthorityInformationStorageAdapterSettingsProvider))
            {
                IProgram program = serviceProvider.GetService<IProgram>();

                CancellationToken cancellationToken = CancellationToken.None;
                Task<int> runAsyncTask = program.RunAsync(
                    options,
                    cancellationToken);

                toReturn = runAsyncTask.Result;
            }

            return toReturn;
        }

        [ExcludeFromCodeCoverage]
        private static ServiceProvider CreateServiceProvider(
            SchoolInformationStorageAdapterSettingsProvider schoolInformationStorageAdapterSettingsProvider,
            LocalAuthorityInformationStorageAdapterSettingsProvider localAuthorityInformationStorageAdapter)
        {
            ServiceProvider toReturn = new ServiceCollection()
                .AddScoped<ILoggerWrapper, LoggerWrapper>()
                .AddSingleton<ISchoolInformationStorageAdapterSettingsProvider>(schoolInformationStorageAdapterSettingsProvider)
                .AddSingleton<ILocalAuthorityInformationStorageAdapterSettingsProvider>(localAuthorityInformationStorageAdapter)
                .AddScoped<ISchoolInformationStorageAdapter, SchoolInformationStorageAdapter>()
                .AddScoped<ILocalAuthorityInformationStorageAdapter, LocalAuthorityInformationStorageAdapter>()
                .AddScoped<IConfigurationFileReader, ConfigurationFileReader>()
                .AddScoped<ISpreadsheetReader, SpreadsheetReader>()
                .AddScoped<ISpreadsheetProcessor, SpreadsheetProcessor>()
                .AddScoped<IProgram, Program>()
                .BuildServiceProvider();

            return toReturn;
        }

        private static ProcessRequest Map(Options options)
        {
            ProcessRequest toReturn = new ProcessRequest()
            {
                StorageConnectionString = options.StorageConnectionString,
                TableName = options.TableName,
                SpreadsheetFile = options.SpreadsheetFile,
                ConfigFile = options.ConfigFile,
            };

            return toReturn;
        }

        private void OnInsertionProgressReported(decimal percentage)
        {
            Console.Title =
                $"Record Insertion Progress: " +
                $"{percentage.ToString("0.00", CultureInfo.InvariantCulture)}%...";
        }
    }
}
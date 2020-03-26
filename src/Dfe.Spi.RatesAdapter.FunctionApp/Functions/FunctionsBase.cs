namespace Dfe.Spi.RatesAdapter.FunctionApp.Functions
{
    using Dfe.Spi.Common.Logging.Definitions;
    using Dfe.Spi.Common.Models;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Base class for functions in the
    /// <see cref="Dfe.Spi.RatesAdapter.FunctionApp.Functions" /> namespace.
    /// </summary>
    public abstract class FunctionsBase
    {
        private readonly ILoggerWrapper loggerWrapper;

        /// <summary>
        /// Initialises a new instance of the <see cref="FunctionsBase" />
        /// class.
        /// </summary>
        /// <param name="loggerWrapper">
        /// An instance of type <see cref="ILoggerWrapper" />.
        /// </param>
        public FunctionsBase(ILoggerWrapper loggerWrapper)
        {
            this.loggerWrapper = loggerWrapper;
        }

        /// <summary>
        /// Inspects a particular <see cref="IActionResult" /> instance,
        /// and if it's an error, logs it out.
        /// </summary>
        /// <param name="actionResult">
        /// An instance of type <see cref="IActionResult" />.
        /// </param>
        protected void LogOutgoingErrors(IActionResult actionResult)
        {
            // Is it an error?
            JsonResult jsonResult = actionResult as JsonResult;
            if (jsonResult != null && jsonResult.Value is HttpErrorBody)
            {
                HttpErrorBody httpErrorBody =
                    jsonResult.Value as HttpErrorBody;

                this.loggerWrapper.Warning(
                    $"This request ended with a (handled) error being " +
                    $"returned: {httpErrorBody}.");
            }
        }
    }
}
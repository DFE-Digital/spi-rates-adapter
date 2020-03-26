namespace Dfe.Spi.RatesAdapter.StoragePopulator.Application.Definitions.Processors
{
    /// <summary>
    /// Describes the <c>InsertionProgressReportedHandler</c> event handler.
    /// </summary>
    /// <param name="percentage">
    /// The percentage of completeness, as a <see cref="decimal" />.
    /// </param>
    public delegate void InsertionProgressReportedHandler(decimal percentage);
}
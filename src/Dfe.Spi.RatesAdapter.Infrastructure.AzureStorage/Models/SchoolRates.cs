namespace Dfe.Spi.RatesAdapter.Infrastructure.AzureStorage.Models
{
    /// <summary>
    /// Represents a <c>SchoolRates</c> table entry.
    /// </summary>
    public abstract class SchoolRates : ModelsBase
    {
        // PK is the URN.
        // RK is the SchoolRatesGroups dervied type.
    }
}
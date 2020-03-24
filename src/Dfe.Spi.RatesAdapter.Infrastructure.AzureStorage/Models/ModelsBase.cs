namespace Dfe.Spi.RatesAdapter.Infrastructure.AzureStorage.Models
{
    using Meridian.MeaningfulToString;
    using Microsoft.WindowsAzure.Storage.Table;

    /// <summary>
    /// Base class for all models in the
    /// <see cref="Dfe.Spi.RatesAdapter.Infrastructure.AzureStorage.Models" />
    /// namespace.
    /// </summary>
    public class ModelsBase : TableEntity
    {
        /// <inheritdoc />
        public override string ToString()
        {
            return this.MeaningfulToString();
        }
    }
}
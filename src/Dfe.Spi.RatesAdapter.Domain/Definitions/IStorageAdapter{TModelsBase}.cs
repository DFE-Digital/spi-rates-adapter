namespace Dfe.Spi.RatesAdapter.Domain.Definitions
{
    using System.Threading;
    using System.Threading.Tasks;
    using Dfe.Spi.RatesAdapter.Domain.Models;

    /// <summary>
    /// Describes the operations of a storage adapter.
    /// </summary>
    /// <typeparam name="TModelsBase">
    /// A type deriving from <see cref="ModelsBase" />.
    /// </typeparam>
    public interface IStorageAdapter<TModelsBase>
        where TModelsBase : ModelsBase
    {
        /// <summary>
        /// Clears the underlying storage of all entries.
        /// </summary>
        /// <param name="cancellationToken">
        /// An instance of <see cref="CancellationToken" />.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        Task ClearStorageAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Creates an instance of type <typeparamref name="TModelsBase" />.
        /// </summary>
        /// <param name="modelsBase">
        /// An instance of type <typeparamref name="TModelsBase" />.
        /// </param>
        /// <param name="cancellationToken">
        /// An instance of <see cref="CancellationToken" />.
        /// </param>
        /// <returns>
        /// A <see cref="Task"/> representing the asynchronous operation.
        /// </returns>
        Task CreateAsync(
            TModelsBase modelsBase,
            CancellationToken cancellationToken);
    }
}

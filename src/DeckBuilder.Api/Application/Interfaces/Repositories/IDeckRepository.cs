namespace DeckBuilder.Application.Interfaces.Repositories
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using DeckBuilder.Domain.Decks;

    /// <summary>
    /// Provides persistence operations for decks.
    /// </summary>
    public interface IDeckRepository
    {
        /// <summary>
        /// Adds a deck to the repository.
        /// </summary>
        /// <param name="deck">The deck to add.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        Task AddAsync(Deck deck, CancellationToken cancellationToken);

        /// <summary>
        /// Gets a deck by its identifier.
        /// </summary>
        /// <param name="idDeck">The deck identifier.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>The deck if found; otherwise, <c>null</c>.</returns>
        Task<Deck?> GetByIdAsync(Guid idDeck, CancellationToken cancellationToken);

        /// <summary>
        /// Updates a deck in the repository.
        /// </summary>
        /// <param name="deck">The deck to update.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        Task UpdateAsync(Deck deck, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes a deck by its identifier.
        /// </summary>
        /// <param name="idDeck">The deck identifier.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns><c>true</c> if deleted; otherwise, <c>false</c>.</returns>
        Task<bool> DeleteAsync(Guid idDeck, CancellationToken cancellationToken);
    }
}
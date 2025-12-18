namespace DeckBuilder.Application.Interfaces.Services
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using DeckBuilder.Application.DTOs.Deck;
    using DeckBuilder.Domain.Enums;

    /// <summary>
    /// Exposes deck write operations.
    /// </summary>
    public interface IDeckWriteService
    {
        /// <summary>
        /// Creates a new deck.
        /// </summary>
        /// <param name="request">The create request.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        Task<DeckDto> CreateAsync(CreateDeckRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Changes a deck visibility.
        /// </summary>
        /// <param name="idDeck">The deck identifier.</param>
        /// <param name="request">The update visibility request.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns><c>true</c> if updated; otherwise, <c>false</c>.</returns>
        Task<bool> ChangeVisibilityAsync(Guid idDeck, UpdateDeckVisibilityRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Adds or updates an entry within a deck.
        /// </summary>
        /// <param name="idDeck">The deck identifier.</param>
        /// <param name="request">The upsert entry request.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns><c>true</c> if updated; otherwise, <c>false</c>.</returns>
        Task<bool> UpsertEntryAsync(Guid idDeck, UpsertDeckEntryRequest request, CancellationToken cancellationToken);

        /// <summary>
        /// Removes an entry from a deck.
        /// </summary>
        /// <param name="idDeck">The deck identifier.</param>
        /// <param name="cardScryfallId">The card Scryfall identifier.</param>
        /// <param name="section">The deck section.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns><c>true</c> if removed; otherwise, <c>false</c>.</returns>
        Task<bool> RemoveEntryAsync(Guid idDeck, string cardScryfallId, DeckSection section, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes a deck by its identifier.
        /// </summary>
        /// <param name="idDeck">The deck identifier.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns><c>true</c> if deleted; otherwise, <c>false</c>.</returns>
        Task<bool> DeleteAsync(Guid idDeck, CancellationToken cancellationToken);
    }
}

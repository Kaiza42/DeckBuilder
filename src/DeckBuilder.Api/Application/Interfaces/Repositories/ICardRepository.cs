namespace DeckBuilder.Application.Interfaces.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using DeckBuilder.Application.DTOs.Card;
    using DeckBuilder.Domain.Entities;

    /// <summary>
    /// Defines read-only data access operations for card entities.
    /// </summary>
    public interface ICardRepository
    {
        /// <summary>
        /// Retrieves a card by its internal identifier.
        /// </summary>
        /// <param name="id">The identifier of the card.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The matching card, or null if no card is found.</returns>
        Task<Card?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves a card by its Scryfall identifier.
        /// </summary>
        /// <param name="scryfallId">The Scryfall identifier of the card.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The matching card, or null if no card is found.</returns>
        Task<Card?> GetByScryfallIdAsync(string scryfallId, CancellationToken cancellationToken);

        /// <summary>
        /// Searches for cards matching the specified filters and pagination settings.
        /// </summary>
        /// <param name="parameters">The search and pagination parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A read-only collection of cards that match the given parameters.</returns>
        Task<IReadOnlyCollection<Card>> SearchAsync(CardSearchParameters parameters, CancellationToken cancellationToken);
    }
}
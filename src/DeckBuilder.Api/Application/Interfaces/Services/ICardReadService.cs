namespace DeckBuilder.Application.Interfaces.Services
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using DeckBuilder.Application.DTOs.Cards;

    /// <summary>
    /// Defines read-only operations used by the API to retrieve card information.
    /// </summary>
    public interface ICardReadService
    {
        /// <summary>
        /// Retrieves a card by its Scryfall identifier.
        /// </summary>
        /// <param name="scryfallId">The Scryfall identifier of the card.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The matching card DTO, or null if no card is found.</returns>
        Task<CardDto?> GetByScryfallIdAsync(string scryfallId, CancellationToken cancellationToken);

        /// <summary>
        /// Searches for cards using a free-text query understood by Scryfall.
        /// </summary>
        /// <param name="query">The search query (for example, "lightning bolt").</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A read-only collection of matching card DTOs.</returns>
        Task<IReadOnlyCollection<CardDto>> SearchAsync(string query, CancellationToken cancellationToken);
    }
}
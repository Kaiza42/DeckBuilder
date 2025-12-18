namespace DeckBuilder.Application.Interfaces.Services
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using DeckBuilder.Application.DTOs.Card;

    /// <summary>
    /// Provides read-only operations to retrieve card information.
    /// </summary>
    public interface ICardReadService
    {
        /// <summary>
        /// Retrieves a card from Scryfall by its Scryfall identifier.
        /// </summary>
        Task<CardDto?> GetByScryfallIdAsync(
            string scryfallId,
            CancellationToken cancellationToken);

        /// <summary>
        /// Searches cards using a raw Scryfall query string.
        /// </summary>
        Task<IReadOnlyCollection<CardDto>> SearchAsync(
            string query,
            CancellationToken cancellationToken);

        /// <summary>
        /// Searches cards using structured criteria.
        /// </summary>
        Task<IReadOnlyCollection<CardDto>> SearchAsync(
            CardSearchCriteriaDto criteria,
            CancellationToken cancellationToken);
    }
}
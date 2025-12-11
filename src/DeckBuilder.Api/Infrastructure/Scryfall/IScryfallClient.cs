namespace DeckBuilder.Infrastructure.Scryfall
{
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using DeckBuilder.Infrastructure.Scryfall.Models;

    /// <summary>
    /// Defines operations used to communicate with the Scryfall API.
    /// </summary>
    public interface IScryfallClient
    {
        /// <summary>
        /// Retrieves a single card from Scryfall by its Scryfall identifier.
        /// </summary>
        /// <param name="scryfallId">The Scryfall identifier of the card.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The corresponding Scryfall card, or null if not found.</returns>
        Task<ScryfallCard?> GetCardByIdAsync(string scryfallId, CancellationToken cancellationToken);

        /// <summary>
        /// Searches for cards using a Scryfall search query string.
        /// </summary>
        /// <param name="query">The Scryfall search query (for example, "lightning bolt").</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A read-only collection of matching Scryfall cards.</returns>
        Task<IReadOnlyCollection<ScryfallCard>> SearchCardsAsync(string query, CancellationToken cancellationToken);
    }
}
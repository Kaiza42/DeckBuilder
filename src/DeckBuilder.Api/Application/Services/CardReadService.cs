namespace DeckBuilder.Application.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using DeckBuilder.Application.DTOs.Card;
    using DeckBuilder.Application.Interfaces.Services;
    using DeckBuilder.Application.Mappers;
    using DeckBuilder.Infrastructure.Scryfall;
    using DeckBuilder.Infrastructure.Scryfall.Querying;

    /// <summary>
    /// Provides read-only operations used by the API to retrieve card information from Scryfall.
    /// </summary>
    /// <remarks>
    /// This service is responsible for orchestrating Scryfall queries and mapping external Scryfall models
    /// into application DTOs. It does not persist data and does not apply additional filtering beyond what
    /// is expressed in the Scryfall query language.
    /// </remarks>
    public class CardReadService : ICardReadService
    {
        private readonly IScryfallClient scryfallClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="CardReadService"/> class.
        /// </summary>
        /// <param name="scryfallClient">The client used to communicate with the Scryfall API.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="scryfallClient"/> is null.
        /// </exception>
        public CardReadService(IScryfallClient scryfallClient)
        {
            this.scryfallClient = scryfallClient ?? throw new ArgumentNullException(nameof(scryfallClient));
        }

        /// <summary>
        /// Retrieves a single card from Scryfall using its unique Scryfall identifier.
        /// </summary>
        /// <param name="scryfallId">The Scryfall identifier of the card.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A mapped <see cref="CardDto"/> when the card is found; otherwise, null.
        /// </returns>
        public async Task<CardDto?> GetByScryfallIdAsync(string scryfallId, CancellationToken cancellationToken)
        {
            var card = await this.scryfallClient
                .GetCardByIdAsync(scryfallId, cancellationToken)
                .ConfigureAwait(false);

            return card is null ? null : CardMapper.ToDto(card);
        }

        /// <summary>
        /// Searches cards on Scryfall using a raw Scryfall query string.
        /// </summary>
        /// <param name="query">
        /// The Scryfall query string (e.g., "f:standard c:ur cmc&lt;=2 r:rare").
        /// </param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A read-only collection of mapped <see cref="CardDto"/> results. The collection is empty when no card matches.
        /// </returns>
        public async Task<IReadOnlyCollection<CardDto>> SearchAsync(string query, CancellationToken cancellationToken)
        {
            var cards = await this.scryfallClient
                .SearchCardsAsync(query, cancellationToken)
                .ConfigureAwait(false);

            return cards.Select(CardMapper.ToDto).ToArray();
        }

        /// <summary>
        /// Searches cards on Scryfall using structured criteria.
        /// </summary>
        /// <param name="criteria">
        /// The structured criteria used to build a Scryfall query (format, colors, mana value, rarity, etc.).
        /// </param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>
        /// A read-only collection of mapped <see cref="CardDto"/> results. The collection is empty when no card matches.
        /// </returns>
        /// <remarks>
        /// This method converts <paramref name="criteria"/> into a Scryfall query string using
        /// <see cref="ScryfallQueryBuilder"/> and then executes the search through <see cref="IScryfallClient"/>.
        /// </remarks>
        public async Task<IReadOnlyCollection<CardDto>> SearchAsync(CardSearchCriteriaDto criteria, CancellationToken cancellationToken)
        {
            var query = ScryfallQueryBuilder.Build(criteria);

            return await this.SearchAsync(query, cancellationToken).ConfigureAwait(false);
        }
    }
}

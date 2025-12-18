namespace DeckBuilder.Controllers
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Threading;
    using System.Threading.Tasks;
    using DeckBuilder.Application.DTOs.Card;
    using DeckBuilder.Application.Interfaces.Services;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Exposes endpoints to search and retrieve Magic: The Gathering cards from Scryfall.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class CardsController : ControllerBase
    {
        private readonly ICardReadService cardReadService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CardsController"/> class.
        /// </summary>
        /// <param name="cardReadService">The card read service.</param>
        public CardsController(ICardReadService cardReadService)
        {
            this.cardReadService = cardReadService;
        }

        /// <summary>
        /// Retrieves a card by its Scryfall identifier.
        /// </summary>
        /// <param name="scryfallId">The Scryfall identifier of the card.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The matching card, or 404 when not found.</returns>
        [HttpGet("{scryfallId}")]
        [ProducesResponseType(typeof(CardDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CardDto>> GetByScryfallIdAsync(
            [FromRoute] string scryfallId,
            CancellationToken cancellationToken)
        {
            var card = await this.cardReadService
                .GetByScryfallIdAsync(scryfallId, cancellationToken)
                .ConfigureAwait(false);

            if (card is null)
            {
                return this.NotFound();
            }

            return this.Ok(card);
        }

        /// <summary>
        /// Searches cards using a raw Scryfall query string.
        /// </summary>
        /// <param name="q">The Scryfall query (e.g., "f:standard c:ur cmc&lt;=2 r:rare").</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of matching cards.</returns>
        [HttpGet("search")]
        [ProducesResponseType(typeof(IReadOnlyCollection<CardDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IReadOnlyCollection<CardDto>>> SearchByQueryAsync(
            [FromQuery(Name = "q"), Required] string q,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(q))
            {
                return this.BadRequest("Query parameter 'q' is required.");
            }

            var cards = await this.cardReadService
                .SearchAsync(q, cancellationToken)
                .ConfigureAwait(false);

            return this.Ok(cards);
        }

        /// <summary>
        /// Searches cards using structured criteria.
        /// </summary>
        /// <param name="criteria">The structured search criteria.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of matching cards.</returns>
        [HttpPost("search")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(IReadOnlyCollection<CardDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IReadOnlyCollection<CardDto>>> SearchByCriteriaAsync(
            [FromBody, Required] CardSearchCriteriaDto? criteria,
            CancellationToken cancellationToken)
        {
            if (criteria is null)
            {
                return this.BadRequest("Request body is required.");
            }

            var hasAnyFilter =
                !string.IsNullOrWhiteSpace(criteria.Name) ||
                !string.IsNullOrWhiteSpace(criteria.Format) ||
                criteria.Colors is not null ||
                criteria.MinCmc is not null ||
                criteria.MaxCmc is not null ||
                criteria.Rarity is not null;

            if (!hasAnyFilter)
            {
                return this.BadRequest("At least one search criterion must be provided.");
            }

            var cards = await this.cardReadService
                .SearchAsync(criteria, cancellationToken)
                .ConfigureAwait(false);

            return this.Ok(cards);
        }
    }
}

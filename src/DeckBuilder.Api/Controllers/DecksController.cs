namespace DeckBuilder.Controllers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using DeckBuilder.Application.DTOs.Deck;
    using DeckBuilder.Application.Interfaces.Services;
    using DeckBuilder.Domain.Enums;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Exposes endpoints to create and manage decks.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public sealed class DecksController : ControllerBase
    {
        /// <summary>
        /// The route name used to retrieve a deck by its identifier.
        /// </summary>
        private const string GetByIdRouteName = "GetDeckById";

        private readonly IDeckReadService deckReadService;
        private readonly IDeckWriteService deckWriteService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DecksController"/> class.
        /// </summary>
        /// <param name="deckReadService">The deck read service.</param>
        /// <param name="deckWriteService">The deck write service.</param>
        public DecksController(IDeckReadService deckReadService, IDeckWriteService deckWriteService)
        {
            this.deckReadService = deckReadService;
            this.deckWriteService = deckWriteService;
        }

        /// <summary>
        /// Creates a new deck.
        /// </summary>
        /// <param name="request">The create deck request.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>The created deck.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(DeckDto), StatusCodes.Status201Created)]
        public async Task<ActionResult<DeckDto>> CreateAsync(
            [FromBody] CreateDeckRequest request,
            CancellationToken cancellationToken)
        {
            var created = await this.deckWriteService.CreateAsync(request, cancellationToken);
            return this.CreatedAtRoute(GetByIdRouteName, new { idDeck = created.IdDeck }, created);
        }

        /// <summary>
        /// Gets a deck by its identifier.
        /// </summary>
        /// <param name="idDeck">The deck identifier.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>The deck if found; otherwise, <see cref="StatusCodes.Status404NotFound"/>.</returns>
        [HttpGet("{idDeck:guid}", Name = GetByIdRouteName)]
        [ProducesResponseType(typeof(DeckDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DeckDto>> GetByIdAsync(
            [FromRoute] Guid idDeck,
            CancellationToken cancellationToken)
        {
            var deck = await this.deckReadService.GetByIdAsync(idDeck, cancellationToken);
            return deck is null ? this.NotFound() : this.Ok(deck);
        }

        /// <summary>
        /// Changes the visibility of a deck.
        /// </summary>
        /// <param name="idDeck">The deck identifier.</param>
        /// <param name="request">The visibility request.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// <see cref="StatusCodes.Status204NoContent"/> if updated; otherwise, <see cref="StatusCodes.Status404NotFound"/>.
        /// </returns>
        [HttpPatch("{idDeck:guid}/visibility")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ChangeVisibilityAsync(
            [FromRoute] Guid idDeck,
            [FromBody] UpdateDeckVisibilityRequest request,
            CancellationToken cancellationToken)
        {
            var updated = await this.deckWriteService.ChangeVisibilityAsync(idDeck, request, cancellationToken);
            return updated ? this.NoContent() : this.NotFound();
        }

        /// <summary>
        /// Adds or updates an entry within a deck.
        /// </summary>
        /// <param name="idDeck">The deck identifier.</param>
        /// <param name="request">The upsert request.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// <see cref="StatusCodes.Status204NoContent"/> if updated; otherwise, <see cref="StatusCodes.Status404NotFound"/>.
        /// </returns>
        [HttpPost("{idDeck:guid}/entries")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpsertEntryAsync(
            [FromRoute] Guid idDeck,
            [FromBody] UpsertDeckEntryRequest request,
            CancellationToken cancellationToken)
        {
            var updated = await this.deckWriteService.UpsertEntryAsync(idDeck, request, cancellationToken);
            return updated ? this.NoContent() : this.NotFound();
        }

        /// <summary>
        /// Removes an entry from a deck.
        /// </summary>
        /// <param name="idDeck">The deck identifier.</param>
        /// <param name="cardScryfallId">The card Scryfall identifier.</param>
        /// <param name="section">The deck section.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// <see cref="StatusCodes.Status204NoContent"/> if removed; otherwise, <see cref="StatusCodes.Status404NotFound"/>.
        /// </returns>
        [HttpDelete("{idDeck:guid}/entries/{cardScryfallId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveEntryAsync(
            [FromRoute] Guid idDeck,
            [FromRoute] string cardScryfallId,
            [FromQuery] DeckSection section,
            CancellationToken cancellationToken)
        {
            var removed = await this.deckWriteService.RemoveEntryAsync(
                idDeck,
                cardScryfallId,
                section,
                cancellationToken);

            return removed ? this.NoContent() : this.NotFound();
        }

        /// <summary>
        /// Deletes a deck by its identifier.
        /// </summary>
        /// <param name="idDeck">The deck identifier.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>
        /// <see cref="StatusCodes.Status204NoContent"/> if deleted; otherwise, <see cref="StatusCodes.Status404NotFound"/>.
        /// </returns>
        [HttpDelete("{idDeck:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(
            [FromRoute] Guid idDeck,
            CancellationToken cancellationToken)
        {
            var deleted = await this.deckWriteService.DeleteAsync(idDeck, cancellationToken);
            return deleted ? this.NoContent() : this.NotFound();
        }
    }
}

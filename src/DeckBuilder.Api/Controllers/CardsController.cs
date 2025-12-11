namespace DeckBuilder.Api.Controllers;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DeckBuilder.Application.DTOs.Cards;
using DeckBuilder.Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Provides read-only endpoints to retrieve card information from Scryfall.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class CardsController : ControllerBase
{
    private readonly ICardReadService cardReadService;

    /// <summary>
    /// Initializes a new instance of the <see cref="CardsController"/> class.
    /// </summary>
    /// <param name="cardReadService">The service used to retrieve card information.</param>
    public CardsController(ICardReadService cardReadService)
    {
        this.cardReadService = cardReadService;
    }

    /// <summary>
    /// Retrieves a card by its Scryfall identifier.
    /// </summary>
    /// <param name="scryfallId">The Scryfall identifier of the card.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// Returns <see cref="StatusCodes.Status200OK"/> with the card,
    /// or <see cref="StatusCodes.Status404NotFound"/> if it does not exist.
    /// </returns>
    [HttpGet("scryfall/{scryfallId}")]
    [ProducesResponseType(typeof(CardDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByScryfallIdAsync(string scryfallId, CancellationToken cancellationToken)
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
    /// Searches for cards using a Scryfall-compatible query string.
    /// </summary>
    /// <param name="q">The search query (for example, "lightning bolt").</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    /// Returns <see cref="StatusCodes.Status200OK"/> with a collection of matching cards.
    /// </returns>
    [HttpGet("search")]
    [ProducesResponseType(typeof(IReadOnlyCollection<CardDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> SearchAsync([FromQuery] string q, CancellationToken cancellationToken)
    {
        var cards = await this.cardReadService
            .SearchAsync(q, cancellationToken)
            .ConfigureAwait(false);

        return this.Ok(cards);
    }
}

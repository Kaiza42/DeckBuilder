namespace DeckBuilder.Application.Interfaces.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using DeckBuilder.Application.DTOs.Card;

    /// <summary>
    /// Defines read-only operations used by the API to retrieve card information.
    /// </summary>
    public interface ICardReadService
    {
        /// <summary>
        /// Retrieves a card by its internal identifier and returns it as a <see cref="CardDto"/>.
        /// </summary>
        /// <param name="id">The identifier of the card.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The matching card DTO, or null if no card is found.</returns>
        Task<CardDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        /// <summary>
        /// Retrieves a card by its Scryfall identifier and returns it as a <see cref="CardDto"/>.
        /// </summary>
        /// <param name="scryfallId">The Scryfall identifier of the card.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The matching card DTO, or null if no card is found.</returns>
        Task<CardDto?> GetByScryfallIdAsync(string scryfallId, CancellationToken cancellationToken);

        /// <summary>
        /// Searches for cards matching the specified filters and pagination settings
        /// and returns them as a collection of <see cref="CardDto"/> objects.
        /// </summary>
        /// <param name="parameters">The search and pagination parameters.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A read-only collection of card DTOs that match the given parameters.</returns>
        Task<IReadOnlyCollection<CardDto>> SearchAsync(CardSearchParameters parameters, CancellationToken cancellationToken);
    }
}
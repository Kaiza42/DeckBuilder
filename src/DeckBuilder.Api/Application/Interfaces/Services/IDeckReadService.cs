namespace DeckBuilder.Application.Interfaces.Services
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using DeckBuilder.Application.DTOs.Deck;

    /// <summary>
    /// Exposes deck read operations.
    /// </summary>
    public interface IDeckReadService
    {
        /// <summary>
        /// Gets a deck by its identifier.
        /// </summary>
        /// <param name="idDeck">The deck identifier.</param>
        /// <param name="cancellationToken">A token to cancel the operation.</param>
        /// <returns>The deck if found; otherwise, <c>null</c>.</returns>
        Task<DeckDto?> GetByIdAsync(Guid idDeck, CancellationToken cancellationToken);
    }
}
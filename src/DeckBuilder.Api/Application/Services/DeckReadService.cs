namespace DeckBuilder.Application.Services
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using DeckBuilder.Application.DTOs.Deck;
    using DeckBuilder.Application.Interfaces.Repositories;
    using DeckBuilder.Application.Interfaces.Services;
    using DeckBuilder.Application.Mappers;

    /// <summary>
    /// Provides deck read operations.
    /// </summary>
    public sealed class DeckReadService : IDeckReadService
    {
        private readonly IDeckRepository deckRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeckReadService"/> class.
        /// </summary>
        /// <param name="deckRepository">The deck repository.</param>
        public DeckReadService(IDeckRepository deckRepository)
        {
            this.deckRepository = deckRepository;
        }

        /// <inheritdoc />
        public async Task<DeckDto?> GetByIdAsync(Guid idDeck, CancellationToken cancellationToken)
        {
            var deck = await this.deckRepository.GetByIdAsync(idDeck, cancellationToken);
            return deck is null ? null : DeckMapper.ToDto(deck);
        }
    }
}
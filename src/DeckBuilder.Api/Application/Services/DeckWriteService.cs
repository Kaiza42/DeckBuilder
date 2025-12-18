namespace DeckBuilder.Application.Services
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using DeckBuilder.Application.DTOs.Deck;
    using DeckBuilder.Application.Interfaces.Repositories;
    using DeckBuilder.Application.Interfaces.Services;
    using DeckBuilder.Application.Mappers;
    using DeckBuilder.Domain.Decks;
    using DeckBuilder.Domain.Enums;

    /// <summary>
    /// Provides deck write operations.
    /// </summary>
    public sealed class DeckWriteService : IDeckWriteService
    {
        private readonly IDeckRepository deckRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeckWriteService"/> class.
        /// </summary>
        /// <param name="deckRepository">The deck repository.</param>
        public DeckWriteService(IDeckRepository deckRepository)
        {
            this.deckRepository = deckRepository;
        }

        /// <inheritdoc />
        public async Task<DeckDto> CreateAsync(CreateDeckRequest request, CancellationToken cancellationToken)
        {
            var deck = new Deck(
                idDeck: Guid.NewGuid(),
                name: request.Name,
                format: request.Format,
                visibility: request.Visibility,
                description: request.Description);

            await this.deckRepository.AddAsync(deck, cancellationToken);
            return DeckMapper.ToDto(deck);
        }

        /// <inheritdoc />
        public async Task<bool> ChangeVisibilityAsync(Guid idDeck, UpdateDeckVisibilityRequest request, CancellationToken cancellationToken)
        {
            var deck = await this.deckRepository.GetByIdAsync(idDeck, cancellationToken);
            if (deck is null)
            {
                return false;
            }

            deck.ChangeVisibility(request.Visibility);
            await this.deckRepository.UpdateAsync(deck, cancellationToken);
            return true;
        }

        /// <inheritdoc />
        public async Task<bool> UpsertEntryAsync(Guid idDeck, UpsertDeckEntryRequest request, CancellationToken cancellationToken)
        {
            var deck = await this.deckRepository.GetByIdAsync(idDeck, cancellationToken);
            if (deck is null)
            {
                return false;
            }

            deck.UpsertEntry(request.CardScryfallId, request.Quantity, request.Section);
            await this.deckRepository.UpdateAsync(deck, cancellationToken);
            return true;
        }

        /// <inheritdoc />
        public async Task<bool> RemoveEntryAsync(Guid idDeck, string cardScryfallId, DeckSection section, CancellationToken cancellationToken)
        {
            var deck = await this.deckRepository.GetByIdAsync(idDeck, cancellationToken);
            if (deck is null)
            {
                return false;
            }

            var removed = deck.RemoveEntry(cardScryfallId, section);
            if (!removed)
            {
                return false;
            }

            await this.deckRepository.UpdateAsync(deck, cancellationToken);
            return true;
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(Guid idDeck, CancellationToken cancellationToken)
        {
            return await this.deckRepository.DeleteAsync(idDeck, cancellationToken);
        }
    }
}

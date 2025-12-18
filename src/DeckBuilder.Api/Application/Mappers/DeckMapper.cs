namespace DeckBuilder.Application.Mappers
{
    using System;
    using System.Linq;
    using DeckBuilder.Application.DTOs.Deck;
    using DeckBuilder.Domain.Decks;

    /// <summary>
    /// Provides mapping helpers between deck domain entities and DTOs.
    /// </summary>
    public static class DeckMapper
    {
        /// <summary>
        /// Maps a <see cref="Deck"/> domain entity to a <see cref="DeckDto"/>.
        /// </summary>
        /// <param name="deck">The deck domain entity.</param>
        /// <returns>The mapped deck DTO.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="deck"/> is <c>null</c>.</exception>
        public static DeckDto ToDto(Deck deck)
        {
            if (deck is null)
            {
                throw new ArgumentNullException(nameof(deck));
            }

            return new DeckDto
            {
                IdDeck = deck.IdDeck,
                Name = deck.Name,
                Format = deck.Format,
                Description = deck.Description,
                Visibility = deck.Visibility,
                CreatedAtUtc = deck.CreatedAtUtc,
                UpdatedAtUtc = deck.UpdatedAtUtc,
                Entries = deck.Entries
                    .Select(e => new DeckEntryDto
                    {
                        CardScryfallId = e.CardScryfallId,
                        Quantity = e.Quantity,
                        Section = e.Section,
                    })
                    .ToArray(),
            };
        }
    }
}
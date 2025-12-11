namespace DeckBuilder.Application.Mappings
{
    using System.Collections.Generic;
    using System.Linq;
    using DeckBuilder.Application.DTOs.Card;
    using DeckBuilder.Domain.Entities;

    /// <summary>
    /// Provides mapping helpers between domain card entities and card data transfer objects.
    /// </summary>
    public static class CardMappings
    {
        /// <summary>
        /// Maps a <see cref="Card"/> domain entity to a <see cref="CardDto"/>.
        /// </summary>
        /// <param name="card">The domain card entity to map.</param>
        /// <returns>A <see cref="CardDto"/> representing the given card.</returns>
        public static CardDto ToDto(this Card card)
        {
            return new CardDto
            {
                IdCard = card.IdCard,
                ScryfallId = card.ScryfallId,
                ArenaId = card.ArenaId,
                Name = card.Name,
                SetCode = card.SetCode,
                CollectorNumber = card.CollectorNumber,
                ManaCost = card.ManaCost,
                Cmc = card.Cmc,
                Colors = card.Colors,
                ColorIdentity = card.ColorIdentity,
                TypeLine = card.TypeLine,
                OracleText = card.OracleText,
                Power = card.Power,
                Toughness = card.Toughness,
                Rarity = card.Rarity,
                ImageUrl = card.ImageUrl,
                IsToken = card.IsToken,
                IsDoubleFaced = card.IsDoubleFaced,
            };
        }

        /// <summary>
        /// Maps a collection of <see cref="Card"/> domain entities to a collection of <see cref="CardDto"/> objects.
        /// </summary>
        /// <param name="cards">The collection of domain card entities to map.</param>
        /// <returns>A read-only collection of <see cref="CardDto"/> objects.</returns>
        public static IReadOnlyCollection<CardDto> ToDtoList(this IEnumerable<Card> cards)
        {
            return cards
                .Select(c => c.ToDto())
                .ToArray();
        }
    }
}

namespace DeckBuilder.Application.Mappers
{
    using System;
    using System.Collections.Generic;
    using DeckBuilder.Application.DTOs.Card;
    using DeckBuilder.Domain.Enums;
    using DeckBuilder.Infrastructure.Scryfall.Models;

    /// <summary>
    /// Provides mapping utilities to convert Scryfall models into application DTOs.
    /// </summary>
    public static class CardMapper
    {
        /// <summary>
        /// Maps a <see cref="ScryfallCard"/> to a <see cref="CardDto"/>.
        /// </summary>
        /// <param name="card">The Scryfall card to map.</param>
        /// <returns>A mapped <see cref="CardDto"/> instance.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="card"/> is null.</exception>
        public static CardDto ToDto(ScryfallCard card)
        {
            if (card is null)
            {
                throw new ArgumentNullException(nameof(card));
            }

            return new CardDto
            {
                ScryfallId = card.Id,
                ArenaId = card.ArenaId?.ToString(),
                Name = card.Name,
                SetCode = card.SetCode.ToUpperInvariant(),
                CollectorNumber = card.CollectorNumber,
                ManaCost = card.ManaCost,
                Cmc = card.Cmc,
                Colors = MapColors(card.Colors),
                ColorIdentity = MapColors(card.ColorIdentity),
                TypeLine = card.TypeLine,
                OracleText = card.OracleText,
                Power = card.Power,
                Toughness = card.Toughness,
                Rarity = MapRarity(card.Rarity),
                ImageUrl = card.ImageUris?.Normal,
                IsToken = false,
                IsDoubleFaced = false,
            };
        }

        /// <summary>
        /// Converts Scryfall color symbols (W, U, B, R, G) into a flagged <see cref="CardColor"/> value.
        /// </summary>
        /// <param name="colors">The list of color symbols returned by Scryfall.</param>
        /// <returns>A flagged <see cref="CardColor"/> representing the mapped colors, or <see cref="CardColor.Colorless"/> when empty.</returns>
        private static CardColor MapColors(IReadOnlyList<string>? colors)
        {
            if (colors is null || colors.Count == 0)
            {
                return CardColor.Colorless;
            }

            var result = CardColor.Colorless;

            foreach (var c in colors)
            {
                result |= c switch
                {
                    "W" => CardColor.White,
                    "U" => CardColor.Blue,
                    "B" => CardColor.Black,
                    "R" => CardColor.Red,
                    "G" => CardColor.Green,
                    _ => CardColor.Colorless,
                };
            }

            return result;
        }

        /// <summary>
        /// Converts Scryfall rarity values into the application <see cref="CardRarity"/> enum.
        /// </summary>
        /// <param name="rarity">The rarity string returned by Scryfall.</param>
        /// <returns>The mapped <see cref="CardRarity"/> value, or null when the rarity is unknown or missing.</returns>
        private static CardRarity? MapRarity(string? rarity)
        {
            return rarity?.ToLowerInvariant() switch
            {
                "common" => CardRarity.Common,
                "uncommon" => CardRarity.Uncommon,
                "rare" => CardRarity.Rare,
                "mythic" => CardRarity.Mythic,
                _ => null,
            };
        }
    }
}

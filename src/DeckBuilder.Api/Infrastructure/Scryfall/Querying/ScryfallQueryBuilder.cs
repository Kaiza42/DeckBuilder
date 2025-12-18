namespace DeckBuilder.Infrastructure.Scryfall.Querying
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using DeckBuilder.Application.DTOs.Card;
    using DeckBuilder.Domain.Enums;

    /// <summary>
    /// Builds Scryfall query strings from application-level search criteria.
    /// </summary>
    public static class ScryfallQueryBuilder
    {
        /// <summary>
        /// Builds a Scryfall query string from the provided criteria.
        /// </summary>
        /// <param name="criteria">The criteria to convert into a Scryfall query.</param>
        /// <returns>A Scryfall query string suitable for Scryfall search endpoints.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="criteria"/> is null.</exception>
        public static string Build(CardSearchCriteriaDto criteria)
        {
            if (criteria is null)
            {
                throw new ArgumentNullException(nameof(criteria));
            }

            var parts = new List<string>();

            if (!string.IsNullOrWhiteSpace(criteria.Name))
            {
                parts.Add(criteria.Name.Trim());
            }

            if (!string.IsNullOrWhiteSpace(criteria.Format))
            {
                parts.Add($"f:{criteria.Format.Trim().ToLowerInvariant()}");
            }

            if (criteria.Colors is not null)
            {
                var colorPart = BuildColorQuery(criteria.Colors.Value);
                if (!string.IsNullOrWhiteSpace(colorPart))
                {
                    parts.Add(colorPart);
                }
            }

            if (criteria.MinCmc is not null)
            {
                parts.Add($"cmc>={criteria.MinCmc.Value.ToString(CultureInfo.InvariantCulture)}");
            }

            if (criteria.MaxCmc is not null)
            {
                parts.Add($"cmc<={criteria.MaxCmc.Value.ToString(CultureInfo.InvariantCulture)}");
            }

            if (criteria.Rarity is not null)
            {
                parts.Add($"r:{criteria.Rarity.Value.ToString().ToLowerInvariant()}");
            }

            return string.Join(' ', parts);
        }

        /// <summary>
        /// Builds the Scryfall "c:" clause from a flagged <see cref="CardColor"/>.
        /// </summary>
        /// <param name="colors">The requested colors.</param>
        /// <returns>A Scryfall color clause such as "c:ur". Returns an empty string when no colors are set.</returns>
        private static string BuildColorQuery(CardColor colors)
        {
            if (colors == CardColor.Colorless)
            {
                return "c:c";
            }

            var symbols = new List<string>(5);

            var mappings = new (CardColor Color, string Symbol)[]
            {
                (CardColor.White, "w"),
                (CardColor.Blue, "u"),
                (CardColor.Black, "b"),
                (CardColor.Red, "r"),
                (CardColor.Green, "g"),
            };

            foreach (var (color, symbol) in mappings)
            {
                if (colors.HasFlag(color))
                {
                    symbols.Add(symbol);
                }
            }

            if (symbols.Count == 0)
            {
                return string.Empty;
            }

            return $"c:{string.Join(string.Empty, symbols)}";
        }
    }
}

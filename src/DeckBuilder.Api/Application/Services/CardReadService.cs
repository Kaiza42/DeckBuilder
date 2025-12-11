namespace DeckBuilder.Application.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using DeckBuilder.Application.DTOs.Cards;
    using DeckBuilder.Application.Interfaces.Services;
    using DeckBuilder.Domain.Enums;
    using DeckBuilder.Infrastructure.Scryfall;
    using DeckBuilder.Infrastructure.Scryfall.Models;

    /// <summary>
    /// Provides read-only operations used by the API to retrieve card information from Scryfall.
    /// </summary>
    public class CardReadService : ICardReadService
    {
        private readonly IScryfallClient scryfallClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="CardReadService"/> class.
        /// </summary>
        /// <param name="scryfallClient">The client used to communicate with the Scryfall API.</param>
        public CardReadService(IScryfallClient scryfallClient)
        {
            this.scryfallClient = scryfallClient;
        }

        /// <inheritdoc/>
        public async Task<CardDto?> GetByScryfallIdAsync(string scryfallId, CancellationToken cancellationToken)
        {
            var card = await this.scryfallClient.GetCardByIdAsync(scryfallId, cancellationToken).ConfigureAwait(false);

            if (card is null)
            {
                return null;
            }

            return MapToDto(card);
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyCollection<CardDto>> SearchAsync(string query, CancellationToken cancellationToken)
        {
            var cards = await this.scryfallClient.SearchCardsAsync(query, cancellationToken).ConfigureAwait(false);

            return cards.Select(MapToDto).ToArray();
        }

        private static CardDto MapToDto(ScryfallCard card)
        {
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

        private static CardColor MapColors(IReadOnlyList<string>? colors)
        {
            if (colors is null || colors.Count == 0)
            {
                return CardColor.Colorless;
            }

            var result = CardColor.Colorless;

            foreach (var symbol in colors)
            {
                switch (symbol)
                {
                    case "W":
                        result |= CardColor.White;
                        break;
                    case "U":
                        result |= CardColor.Blue;
                        break;
                    case "B":
                        result |= CardColor.Black;
                        break;
                    case "R":
                        result |= CardColor.Red;
                        break;
                    case "G":
                        result |= CardColor.Green;
                        break;
                    default:
                        break;
                }
            }

            return result;
        }

        private static CardRarity? MapRarity(string? rarity)
        {
            if (rarity is null)
            {
                return null;
            }

            switch (rarity.ToLowerInvariant())
            {
                case "common":
                    return CardRarity.Common;
                case "uncommon":
                    return CardRarity.Uncommon;
                case "rare":
                    return CardRarity.Rare;
                case "mythic":
                    return CardRarity.Mythic;
                default:
                    return null;
            }
        }
    }
}

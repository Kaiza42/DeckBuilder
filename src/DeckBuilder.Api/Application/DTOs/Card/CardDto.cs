namespace DeckBuilder.Application.DTOs.Card
{
    using System;
    using DeckBuilder.Domain.Enums;

    /// <summary>
    /// Represents a data transfer object used to expose card information through the API.
    /// This DTO is intended for read-only scenarios (API responses).
    /// </summary>
    public class CardDto
    {
        /// <summary>
        /// Gets or sets the internal identifier of the card.
        /// </summary>
        public Guid IdCard { get; set; }

        /// <summary>
        /// Gets or sets the unique Scryfall identifier of the card.
        /// This can be used by clients to query additional data from Scryfall.
        /// </summary>
        public string ScryfallId { get; set; } = null!;

        /// <summary>
        /// Gets or sets the MTG Arena identifier of the card, if available.
        /// </summary>
        public string? ArenaId { get; set; }

        /// <summary>
        /// Gets or sets the name of the card.
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Gets or sets the set code the card belongs to (for example, "WOE").
        /// </summary>
        public string SetCode { get; set; } = null!;

        /// <summary>
        /// Gets or sets the collector number of the card within its set.
        /// </summary>
        public string CollectorNumber { get; set; } = null!;

        /// <summary>
        /// Gets or sets the mana cost of the card, using Magic syntax (for example, "{1}{U}{U}").
        /// </summary>
        public string? ManaCost { get; set; }

        /// <summary>
        /// Gets or sets the converted mana cost (CMC) of the card.
        /// </summary>
        public decimal Cmc { get; set; }

        /// <summary>
        /// Gets or sets the colors of the card.
        /// </summary>
        public CardColor Colors { get; set; }

        /// <summary>
        /// Gets or sets the color identity of the card, typically used for deck legality rules.
        /// </summary>
        public CardColor ColorIdentity { get; set; }

        /// <summary>
        /// Gets or sets the type line of the card (for example, "Creature — Human Wizard").
        /// </summary>
        public string TypeLine { get; set; } = null!;

        /// <summary>
        /// Gets or sets the Oracle rules text of the card.
        /// </summary>
        public string? OracleText { get; set; }

        /// <summary>
        /// Gets or sets the power of the card.
        /// </summary>
        public string? Power { get; set; }

        /// <summary>
        /// Gets or sets the toughness of the card.
        /// </summary>
        public string? Toughness { get; set; }

        /// <summary>
        /// Gets or sets the rarity of the card.
        /// </summary>
        public CardRarity? Rarity { get; set; }

        /// <summary>
        /// Gets or sets the primary image URL of the card.
        /// </summary>
        public string? ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this card represents a token.
        /// </summary>
        public bool IsToken { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this card has multiple faces.
        /// </summary>
        public bool IsDoubleFaced { get; set; }
    }
}

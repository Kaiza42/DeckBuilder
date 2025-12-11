namespace DeckBuilder.Domain.Entities
{
    using System;
    using DeckBuilder.Domain.Enums;

    /// <summary>
    /// Represents a Magic: The Gathering card as stored in the DeckBuilder domain.
    /// This model is designed to be compatible with data imported from Scryfall.
    /// </summary>
    public class Card
    {
        /// <summary>
        /// Gets or sets the internal identifier of the card.
        /// </summary>
        public Guid IdCard { get; set; }

        /// <summary>
        /// Gets or sets the unique Scryfall identifier of the card.
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
        /// Gets or sets the three or four letter set code the card belongs to (for example, "WOE").
        /// </summary>
        public string SetCode { get; set; } = null!;

        /// <summary>
        /// Gets or sets the collector number of the card within its set.
        /// This can include suffixes such as "123a".
        /// </summary>
        public string CollectorNumber { get; set; } = null!;

        /// <summary>
        /// Gets or sets the mana cost of the card, using Magic syntax (for example, "{1}{U}{U}").
        /// May be null for cards without a mana cost.
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
        /// Stored as a string to support values such as "*", "1+*", and similar expressions.
        /// </summary>
        public string? Power { get; set; }

        /// <summary>
        /// Gets or sets the toughness of the card.
        /// Stored as a string to support values such as "*", "1+*", and similar expressions.
        /// </summary>
        public string? Toughness { get; set; }

        /// <summary>
        /// Gets or sets the rarity of the card.
        /// May be null for tokens or other non-standard card types.
        /// </summary>
        public CardRarity? Rarity { get; set; }

        /// <summary>
        /// Gets or sets the primary image URL of the card.
        /// This typically points to a Scryfall image.
        /// </summary>
        public string? ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this card represents a token.
        /// </summary>
        public bool IsToken { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this card has multiple faces.
        /// This is used for double-faced or modal double-faced cards.
        /// </summary>
        public bool IsDoubleFaced { get; set; }

        /// <summary>
        /// Gets or sets the UTC date and time when the card was first created in the system.
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Gets or sets the UTC date and time when the card was last updated in the system.
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}

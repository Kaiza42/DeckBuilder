namespace DeckBuilder.Domain.Entities
{
    using System;
    using DeckBuilder.Domain.Enums;

    /// <summary>
    /// Represents a Magic: The Gathering card as stored in the DeckBuilder domain.
    /// This model is designed to be compatible with data imported from Scryfall and MTG Arena.
    /// </summary>
    public class Card
    {
        /// <summary>
        /// Gets or sets the internal identifier of the card.
        /// </summary>
        public Guid Id { get; set; }

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
        //

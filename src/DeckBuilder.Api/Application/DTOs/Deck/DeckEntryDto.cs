namespace DeckBuilder.Application.DTOs.Deck
{
    using DeckBuilder.Domain.Enums;

    /// <summary>
    /// Represents a deck entry data transfer object.
    /// </summary>
    public sealed class DeckEntryDto
    {
        /// <summary>
        /// Gets or sets the Scryfall identifier of the card.
        /// </summary>
        public string CardScryfallId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the quantity of the card in the specified deck section.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the section of the deck the entry belongs to.
        /// </summary>
        public DeckSection Section { get; set; }
    }
}
namespace DeckBuilder.Domain.Decks
{
    using DeckBuilder.Domain.Enums;

    /// <summary>
    /// Represents an entry (card + quantity + section) inside a deck.
    /// </summary>
    public class DeckEntry
    {
        public DeckEntry(string cardScryfallId, int quantity, DeckSection section)
        {
            if (string.IsNullOrWhiteSpace(cardScryfallId))
            {
                throw new ArgumentException("Card Scryfall id is required.", nameof(cardScryfallId));
            }

            if (quantity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than 0.");
            }

            this.CardScryfallId = cardScryfallId.Trim();
            this.Quantity = quantity;
            this.Section = section;
        }

        /// <summary>
        /// Gets the Scryfall identifier of the card.
        /// </summary>
        public string CardScryfallId { get; }

        /// <summary>
        /// Gets the card quantity in the specified section.
        /// </summary>
        public int Quantity { get; private set; }

        /// <summary>
        /// Gets the section the entry belongs to.
        /// </summary>
        public DeckSection Section { get; }

        /// <summary>
        /// Updates the quantity of the entry.
        /// </summary>
        /// <param name="quantity">The new quantity.</param>
        public void SetQuantity(int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than 0.");
            }

            this.Quantity = quantity;
        }
    }
}
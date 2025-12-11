namespace DeckBuilder.Domain.Enums
{
    /// <summary>
    /// Represents the colors of a Magic: The Gathering card.
    /// This enum is flagged to support multicolored cards.
    /// </summary>
    [Flags]
    public enum CardColor
    {
        /// <summary>
        /// The card has no color.
        /// </summary>
        Colorless = 0,

        /// <summary>
        /// The card is white.
        /// </summary>
        White = 1 << 0,

        /// <summary>
        /// The card is blue.
        /// </summary>
        Blue = 1 << 1,

        /// <summary>
        /// The card is black.
        /// </summary>
        Black = 1 << 2,

        /// <summary>
        /// The card is red.
        /// </summary>
        Red = 1 << 3,

        /// <summary>
        /// The card is green.
        /// </summary>
        Green = 1 << 4,
    }
}
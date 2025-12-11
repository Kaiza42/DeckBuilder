namespace DeckBuilder.Domain.Enums
{
    /// <summary>
    /// Represents the rarity of a Magic: The Gathering card.
    /// </summary>
    public enum CardRarity
    {
        /// <summary>
        /// The card is a common rarity.
        /// </summary>
        Common = 0,

        /// <summary>
        /// The card is an uncommon rarity.
        /// </summary>
        Uncommon = 1,

        /// <summary>
        /// The card is a rare rarity.
        /// </summary>
        Rare = 2,

        /// <summary>
        /// The card is a mythic rare rarity.
        /// </summary>
        Mythic = 3,
    }
}
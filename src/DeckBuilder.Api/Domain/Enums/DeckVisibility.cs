namespace DeckBuilder.Domain.Enums
{
    /// <summary>
    /// Represents the visibility level of a deck.
    /// </summary>
    public enum DeckVisibility
    {
        /// <summary>
        /// The deck is only visible to its owner (later) and the system.
        /// </summary>
        Private = 0,

        /// <summary>
        /// The deck is visible to everyone and can be listed publicly.
        /// </summary>
        Public = 1,

        /// <summary>
        /// The deck is accessible by link/id but not listed publicly.
        /// </summary>
        Unlisted = 2,
    }
}
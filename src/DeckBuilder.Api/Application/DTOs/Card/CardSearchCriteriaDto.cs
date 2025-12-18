namespace DeckBuilder.Application.DTOs.Card
{
    using DeckBuilder.Domain.Enums;

    /// <summary>
    /// Represents structured search criteria used to find cards.
    /// This DTO is converted into a Scryfall query string by the application layer.
    /// </summary>
    public sealed class CardSearchCriteriaDto
    {
        /// <summary>
        /// Gets the free-text name query (e.g., "Lightning Bolt").
        /// When provided, it is included as-is in the generated Scryfall query.
        /// </summary>
        public string? Name { get; init; }

        /// <summary>
        /// Gets the target format (e.g., "standard", "pioneer", "modern", "historic", "commander").
        /// This value is mapped to Scryfall format filtering (f:&lt;format&gt;).
        /// </summary>
        public string? Format { get; init; }

        /// <summary>
        /// Gets the desired card colors (Scryfall 'c:' filter).
        /// Example: Blue|Red.
        /// </summary>
        public CardColor? Colors { get; init; }

        /// <summary>
        /// Gets the minimum converted mana cost to match (inclusive).
        /// This maps to Scryfall "cmc&gt;=".
        /// </summary>
        public int? MinCmc { get; init; }

        /// <summary>
        /// Gets the maximum converted mana cost to match (inclusive).
        /// This maps to Scryfall "cmc&lt;=".
        /// </summary>
        public int? MaxCmc { get; init; }

        /// <summary>
        /// Gets the rarity filter.
        /// This maps to Scryfall "r:&lt;rarity&gt;".
        /// </summary>
        public CardRarity? Rarity { get; init; }
    }
}
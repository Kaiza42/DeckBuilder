namespace DeckBuilder.Application.DTOs.Card
{
    using DeckBuilder.Domain.Enums;

    /// <summary>
    /// Represents filter and pagination parameters used to search for cards.
    /// </summary>
    public class CardSearchParameters
    {
        /// <summary>
        /// Gets or sets an optional name or partial name used to filter cards.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Gets or sets an optional set code (for example, "WOE") used to filter cards.
        /// </summary>
        public string? SetCode { get; set; }

        /// <summary>
        /// Gets or sets optional colors that must be present on the card.
        /// </summary>
        public CardColor? Colors { get; set; }

        /// <summary>
        /// Gets or sets an optional rarity used to filter cards.
        /// </summary>
        public CardRarity? Rarity { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether only tokens, only non-tokens,
        /// or both should be returned. When null, both are allowed.
        /// </summary>
        public bool? IsToken { get; set; }

        /// <summary>
        /// Gets or sets the 1-based page index used for pagination.
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// Gets or sets the number of items per page.
        /// </summary>
        public int PageSize { get; set; } = 50;
    }
}
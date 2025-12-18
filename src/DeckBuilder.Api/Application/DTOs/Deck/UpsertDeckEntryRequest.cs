namespace DeckBuilder.Application.DTOs.Deck
{
    using System.ComponentModel.DataAnnotations;
    using DeckBuilder.Domain.Enums;

    /// <summary>
    /// Request used to add or update a deck entry.
    /// </summary>
    public sealed class UpsertDeckEntryRequest
    {
        [Required]
        [MinLength(1)]
        public string CardScryfallId { get; set; } = string.Empty;

        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        public DeckSection Section { get; set; } = DeckSection.Mainboard;
    }
}
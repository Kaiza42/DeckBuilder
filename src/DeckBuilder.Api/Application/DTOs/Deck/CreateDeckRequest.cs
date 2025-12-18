namespace DeckBuilder.Application.DTOs.Deck
{
    using System.ComponentModel.DataAnnotations;
    using DeckBuilder.Domain.Enums;

    /// <summary>
    /// Request used to create a deck.
    /// </summary>
    public sealed class CreateDeckRequest
    {
        [Required]
        [MinLength(1)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MinLength(1)]
        public string Format { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DeckVisibility Visibility { get; set; } = DeckVisibility.Private;
    }
}
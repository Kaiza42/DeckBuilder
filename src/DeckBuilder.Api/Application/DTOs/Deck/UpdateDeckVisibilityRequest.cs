namespace DeckBuilder.Application.DTOs.Deck
{
    using System.ComponentModel.DataAnnotations;
    using DeckBuilder.Domain.Enums;

    /// <summary>
    /// Request used to update deck visibility.
    /// </summary>
    public sealed class UpdateDeckVisibilityRequest
    {
        [Required]
        public DeckVisibility Visibility { get; set; }
    }
}
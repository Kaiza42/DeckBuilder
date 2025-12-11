namespace DeckBuilder.Infrastructure.Scryfall.Models
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Represents the image URIs section from a Scryfall card.
    /// </summary>
    public sealed class ScryfallImageUris
    {
        [JsonPropertyName("normal")]
        public string? Normal { get; set; }
    }
}
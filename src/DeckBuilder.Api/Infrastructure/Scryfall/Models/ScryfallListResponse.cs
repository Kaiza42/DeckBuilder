namespace DeckBuilder.Infrastructure.Scryfall.Models
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Represents a generic Scryfall list response.
    /// </summary>
    /// <typeparam name="T">The type contained in the list.</typeparam>
    public sealed class ScryfallListResponse<T>
    {
        [JsonPropertyName("data")]
        public List<T> Data { get; set; } = new ();
    }
}
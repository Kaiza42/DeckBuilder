namespace DeckBuilder.Infrastructure.Scryfall.Models
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    /// <summary>
    /// Represents the subset of the Scryfall card JSON used by the DeckBuilder API.
    /// </summary>
    public sealed class ScryfallCard
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = null!;

        [JsonPropertyName("arena_id")]
        public int? ArenaId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("set")]
        public string SetCode { get; set; } = null!;

        [JsonPropertyName("collector_number")]
        public string CollectorNumber { get; set; } = null!;

        [JsonPropertyName("mana_cost")]
        public string? ManaCost { get; set; }

        [JsonPropertyName("cmc")]
        public decimal Cmc { get; set; }

        [JsonPropertyName("colors")]
        public IReadOnlyList<string>? Colors { get; set; }

        [JsonPropertyName("color_identity")]
        public IReadOnlyList<string>? ColorIdentity { get; set; }

        [JsonPropertyName("type_line")]
        public string TypeLine { get; set; } = null!;

        [JsonPropertyName("oracle_text")]
        public string? OracleText { get; set; }

        [JsonPropertyName("power")]
        public string? Power { get; set; }

        [JsonPropertyName("toughness")]
        public string? Toughness { get; set; }

        [JsonPropertyName("rarity")]
        public string? Rarity { get; set; }

        [JsonPropertyName("image_uris")]
        public ScryfallImageUris? ImageUris { get; set; }
    }
}
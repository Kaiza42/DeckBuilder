namespace DeckBuilder.Application.DTOs.Deck
{
    using System;
    using System.Collections.Generic;
    using DeckBuilder.Domain.Enums;

    /// <summary>
    /// Represents a deck data transfer object.
    /// </summary>
    public sealed class DeckDto
    {
        /// <summary>
        /// Gets or sets the deck identifier.
        /// </summary>
        public Guid IdDeck { get; set; }

        /// <summary>
        /// Gets or sets the deck name.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the deck format (e.g. Standard, Modern).
        /// </summary>
        public string Format { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the deck description.
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Gets or sets the deck visibility.
        /// </summary>
        public DeckVisibility Visibility { get; set; }

        /// <summary>
        /// Gets or sets the deck creation timestamp (UTC).
        /// </summary>
        public DateTimeOffset CreatedAtUtc { get; set; }

        /// <summary>
        /// Gets or sets the deck last update timestamp (UTC).
        /// </summary>
        public DateTimeOffset UpdatedAtUtc { get; set; }

        /// <summary>
        /// Gets or sets the deck entries.
        /// </summary>
        public IReadOnlyCollection<DeckEntryDto> Entries { get; set; } = Array.Empty<DeckEntryDto>();
    }
}
namespace DeckBuilder.Domain.Decks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DeckBuilder.Domain.Enums;

    /// <summary>
    /// Represents a deck aggregate root.
    /// </summary>
    public sealed class Deck
    {
        private readonly List<DeckEntry> entries = new List<DeckEntry>();

        public Deck(Guid idDeck, string name, string format, DeckVisibility visibility, string? description = null)
        {
            if (idDeck == Guid.Empty)
            {
                throw new ArgumentException("Deck id cannot be empty.", nameof(idDeck));
            }

            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Deck name is required.", nameof(name));
            }

            if (string.IsNullOrWhiteSpace(format))
            {
                throw new ArgumentException("Deck format is required.", nameof(format));
            }

            this.IdDeck = idDeck;
            this.Name = name.Trim();
            this.Format = format.Trim();
            this.Visibility = visibility;
            this.Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim();

            this.CreatedAtUtc = DateTimeOffset.UtcNow;
            this.UpdatedAtUtc = this.CreatedAtUtc;
        }

        public Guid IdDeck { get; }

        public string Name { get; private set; }

        public string Format { get; private set; }

        public string? Description { get; private set; }

        public DeckVisibility Visibility { get; private set; }

        public DateTimeOffset CreatedAtUtc { get; }

        public DateTimeOffset UpdatedAtUtc { get; private set; }

        public IReadOnlyCollection<DeckEntry> Entries => this.entries.AsReadOnly();

        public void ChangeVisibility(DeckVisibility visibility)
        {
            this.Visibility = visibility;
            this.Touch();
        }

        public void SetDescription(string? description)
        {
            this.Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim();
            this.Touch();
        }

        /// <summary>
        /// Adds a new entry or updates an existing one (by card + section).
        /// </summary>
        public void UpsertEntry(string cardScryfallId, int quantity, DeckSection section)
        {
            var normalizedId = (cardScryfallId ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(normalizedId))
            {
                throw new ArgumentException("Card Scryfall id is required.", nameof(cardScryfallId));
            }

            var existing = this.entries.FirstOrDefault(e =>
                e.CardScryfallId == normalizedId && e.Section == section);

            if (existing is null)
            {
                this.entries.Add(new DeckEntry(normalizedId, quantity, section));
            }
            else
            {
                existing.SetQuantity(quantity);
            }

            this.Touch();
        }

        /// <summary>
        /// Removes an entry by card + section.
        /// </summary>
        public bool RemoveEntry(string cardScryfallId, DeckSection section)
        {
            var normalizedId = (cardScryfallId ?? string.Empty).Trim();
            if (string.IsNullOrWhiteSpace(normalizedId))
            {
                throw new ArgumentException("Card Scryfall id is required.", nameof(cardScryfallId));
            }

            var existing = this.entries.FirstOrDefault(e =>
                e.CardScryfallId == normalizedId && e.Section == section);

            if (existing is null)
            {
                return false;
            }

            this.entries.Remove(existing);
            this.Touch();
            return true;
        }

        private void Touch()
        {
            this.UpdatedAtUtc = DateTimeOffset.UtcNow;
        }
    }
}

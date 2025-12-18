namespace DeckBuilder.Infrastructure.Persistence.Configurations
{
    using DeckBuilder.Domain.Decks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Configures the <see cref="Deck"/> entity mapping.
    /// </summary>
    public sealed class DeckConfiguration : IEntityTypeConfiguration<Deck>
    {
        /// <inheritdoc />
        public void Configure(EntityTypeBuilder<Deck> builder)
        {
            builder.ToTable("Decks");

            builder.HasKey(d => d.IdDeck);

            builder.Property(d => d.IdDeck)
                .ValueGeneratedNever();

            builder.Property(d => d.Name)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(d => d.Format)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(d => d.Description)
                .HasMaxLength(2000);

            builder.Property(d => d.Visibility)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(d => d.CreatedAtUtc)
                .IsRequired();

            builder.Property(d => d.UpdatedAtUtc)
                .IsRequired();

            // DeckEntry is part of the Deck aggregate (owned collection).
            builder.OwnsMany(d => d.Entries, eb =>
            {
                eb.ToTable("DeckEntries");

                // Shadow FK to Deck
                eb.WithOwner().HasForeignKey("IdDeck");

                eb.Property(e => e.CardScryfallId)
                    .HasMaxLength(50)
                    .IsRequired();

                eb.Property(e => e.Quantity)
                    .IsRequired();

                eb.Property(e => e.Section)
                    .HasConversion<int>()
                    .IsRequired();

                // One row per (Deck, Card, Section)
                eb.HasKey("IdDeck", nameof(DeckEntry.CardScryfallId), nameof(DeckEntry.Section));

                eb.HasIndex(nameof(DeckEntry.CardScryfallId));
            });
        }
    }
}

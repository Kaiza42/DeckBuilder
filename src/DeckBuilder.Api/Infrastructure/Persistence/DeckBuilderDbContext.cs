namespace DeckBuilder.Infrastructure.Persistence
{
    using DeckBuilder.Domain.Decks;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Represents the application's database context.
    /// </summary>
    public sealed class DeckBuilderDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeckBuilderDbContext"/> class.
        /// </summary>
        /// <param name="options">The database context options.</param>
        public DeckBuilderDbContext(DbContextOptions<DeckBuilderDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Gets the decks set.
        /// </summary>
        public DbSet<Deck> Decks => this.Set<Deck>();

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DeckBuilderDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
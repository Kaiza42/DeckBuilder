namespace DeckBuilder.Infrastructure.Persistence.Repositories
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using DeckBuilder.Application.Interfaces.Repositories;
    using DeckBuilder.Domain.Decks;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Provides an EF Core repository for decks.
    /// </summary>
    public sealed class EfDeckRepository : IDeckRepository
    {
        private readonly DeckBuilderDbContext dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="EfDeckRepository"/> class.
        /// </summary>
        /// <param name="dbContext">The database context.</param>
        public EfDeckRepository(DeckBuilderDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc />
        public async Task AddAsync(Deck deck, CancellationToken cancellationToken)
        {
            await this.dbContext.Decks.AddAsync(deck, cancellationToken);
            await this.dbContext.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<Deck?> GetByIdAsync(Guid idDeck, CancellationToken cancellationToken)
        {
            return await this.dbContext.Decks
                .Include(d => d.Entries)
                .FirstOrDefaultAsync(d => d.IdDeck == idDeck, cancellationToken);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(Deck deck, CancellationToken cancellationToken)
        {
            this.dbContext.Decks.Update(deck);
            await this.dbContext.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task<bool> DeleteAsync(Guid idDeck, CancellationToken cancellationToken)
        {
            var deck = await this.dbContext.Decks.FirstOrDefaultAsync(d => d.IdDeck == idDeck, cancellationToken);
            if (deck is null)
            {
                return false;
            }

            this.dbContext.Decks.Remove(deck);
            await this.dbContext.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}

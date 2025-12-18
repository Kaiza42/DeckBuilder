namespace DeckBuilder.Persistence
{
    using System;
    using DeckBuilder.Infrastructure.Persistence;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Provides a design-time factory for <see cref="DeckBuilderDbContext"/> to enable EF Core migrations.
    /// </summary>
    public sealed class DesignTimeDeckBuilderDbContextFactory : IDesignTimeDbContextFactory<DeckBuilderDbContext>
    {
        /// <inheritdoc />
        public DeckBuilderDbContext CreateDbContext(string[] args)
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString("DeckBuilderDb");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    "Missing connection string 'ConnectionStrings:DeckBuilderDb'. " +
                    "Set it in appsettings or via environment variable 'ConnectionStrings__DeckBuilderDb'.");
            }

            var optionsBuilder = new DbContextOptionsBuilder<DeckBuilderDbContext>();
            optionsBuilder.UseNpgsql(connectionString);

            return new DeckBuilderDbContext(optionsBuilder.Options);
        }
    }
}
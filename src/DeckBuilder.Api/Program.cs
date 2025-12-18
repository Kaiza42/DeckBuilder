// <copyright file="Program.cs" company="DeckBuilder">
// Copyright (c) 2025 DeckBuilder. All rights reserved.
// </copyright>

namespace DeckBuilder.Api;

using System;
using System.Threading.Tasks;
using DeckBuilder.Application.Interfaces.Repositories;
using DeckBuilder.Application.Interfaces.Services;
using DeckBuilder.Application.Services;
using DeckBuilder.Infrastructure.Persistence;
using DeckBuilder.Infrastructure.Persistence.Repositories;
using DeckBuilder.Infrastructure.Scryfall;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

/// <summary>
/// Entry point for the DeckBuilder HTTP API.
/// </summary>
public static class Program
{
    /// <summary>
    /// Application entry point.
    /// </summary>
    /// <param name="args">Command-line arguments.</param>
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // MVC Controllers
        builder.Services.AddControllers();

        // Health checks (for Docker healthcheck)
        builder.Services.AddHealthChecks();

        // OpenAPI (.NET 9) + override du "server" pour éviter [::1]
        builder.Services.AddOpenApi(options =>
        {
            options.AddDocumentTransformer((document, context, cancellationToken) =>
            {
                document.Servers.Clear();
                document.Servers.Add(new OpenApiServer
                {
                    Url = "http://localhost:8080",
                });

                return Task.CompletedTask;
            });
        });

        // Database (PostgreSQL)
        // Do not throw here: EF design-time tooling may build the host without providing configuration.
        var connectionString = builder.Configuration.GetConnectionString("DeckBuilderDb");

        builder.Services.AddDbContext<DeckBuilderDbContext>(options =>
        {
            // Use whatever is available at runtime; validated after app build.
            if (!string.IsNullOrWhiteSpace(connectionString))
            {
                options.UseNpgsql(connectionString);
            }
        });

        // Repositories
        builder.Services.AddScoped<IDeckRepository, EfDeckRepository>();

        // Application services
        builder.Services.AddScoped<ICardReadService, CardReadService>();
        builder.Services.AddScoped<IDeckReadService, DeckReadService>();
        builder.Services.AddScoped<IDeckWriteService, DeckWriteService>();

        // HTTP client for Scryfall API
        builder.Services.AddHttpClient<IScryfallClient, ScryfallClient>(client =>
        {
            client.BaseAddress = new Uri("https://api.scryfall.com/");

            // Scryfall requirements
            client.DefaultRequestHeaders.UserAgent.ParseAdd(
                "DeckBuilder/1.0 (+https://deckbuilder.local/contact)");
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
        });

        var app = builder.Build();

        // Validate connection string at runtime (not design-time)
        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "Missing connection string 'ConnectionStrings:DeckBuilderDb'. " +
                "Set it in appsettings or via environment variable 'ConnectionStrings__DeckBuilderDb'.");
        }

        // Auto-apply EF Core migrations in Development only
        if (app.Environment.IsDevelopment())
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<DeckBuilderDbContext>();
            dbContext.Database.Migrate();
        }

        // Expose OpenAPI document
        app.MapOpenApi();

        // Swagger UI on /swagger
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/openapi/v1.json", "DeckBuilder API v1");
            options.RoutePrefix = "swagger";
        });

        // Health endpoint (used by Docker healthcheck)
        app.MapHealthChecks("/health");

        // Pas de redirection HTTPS en Development (pratique pour Docker + Swagger)
        if (!app.Environment.IsDevelopment())
        {
            app.UseHttpsRedirection();
        }

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}

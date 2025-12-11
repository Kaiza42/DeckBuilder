// <copyright file="Program.cs" company="DeckBuilder">
// Copyright (c) 2025 DeckBuilder. All rights reserved.
// </copyright>

namespace DeckBuilder.Api;

using System;
using System.Threading.Tasks;
using DeckBuilder.Application.Interfaces.Services;
using DeckBuilder.Application.Services;
using DeckBuilder.Infrastructure.Scryfall;
using Microsoft.AspNetCore.Builder;
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

        // HTTP client for Scryfall API
        builder.Services.AddHttpClient<IScryfallClient, ScryfallClient>(client =>
        {
            client.BaseAddress = new Uri("https://api.scryfall.com/");

            // Scryfall requirements
            client.DefaultRequestHeaders.UserAgent.ParseAdd(
                "DeckBuilder/1.0 (+https://deckbuilder.local/contact)");
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
        });

        // Application services
        builder.Services.AddScoped<ICardReadService, CardReadService>();

        var app = builder.Build();

        // Expose OpenAPI document
        app.MapOpenApi();

        // Swagger UI on /swagger
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/openapi/v1.json", "DeckBuilder API v1");
            options.RoutePrefix = "swagger";
        });

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

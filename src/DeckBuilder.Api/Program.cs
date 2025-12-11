// <copyright file="Program.cs" company="DeckBuilder">
// Copyright (c) 2025 DeckBuilder. All rights reserved.
// </copyright>

namespace DeckBuilder.Api;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

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

        // OpenAPI (.NET 9)
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Expose OpenAPI document
        app.MapOpenApi();

        // Swagger UI on /swagger
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/openapi/v1.json", "DeckBuilder API v1");
            options.RoutePrefix = "swagger";
        });

        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
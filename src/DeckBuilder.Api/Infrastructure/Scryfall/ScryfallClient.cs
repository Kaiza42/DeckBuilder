namespace DeckBuilder.Infrastructure.Scryfall
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using DeckBuilder.Infrastructure.Scryfall.Models;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Provides an HTTP-based implementation of <see cref="IScryfallClient"/> using the Scryfall REST API.
    /// </summary>
    public sealed class ScryfallClient : IScryfallClient
    {
        private readonly HttpClient httpClient;
        private readonly ILogger<ScryfallClient> logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScryfallClient"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client configured with the Scryfall base address.</param>
        /// <param name="logger">The logger instance.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="httpClient"/> or <paramref name="logger"/> is null.
        /// </exception>
        public ScryfallClient(HttpClient httpClient, ILogger<ScryfallClient> logger)
        {
            this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc/>
        public async Task<ScryfallCard?> GetCardByIdAsync(string scryfallId, CancellationToken cancellationToken)
        {
            var url = $"cards/{Uri.EscapeDataString(scryfallId)}";

            try
            {
                using var response = await this.httpClient.GetAsync(url, cancellationToken).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    var errorBody = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

                    this.logger.LogWarning(
                        "[Scryfall][GetById] HTTP {StatusCode} for id '{ScryfallId}'. Body: {Body}",
                        (int)response.StatusCode,
                        scryfallId,
                        errorBody);

                    return null;
                }

                var card = await response.Content
                    .ReadFromJsonAsync<ScryfallCard>(cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

                if (card is null)
                {
                    this.logger.LogWarning(
                        "[Scryfall][GetById] Empty JSON payload for id '{ScryfallId}'.",
                        scryfallId);
                }

                return card;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                this.logger.LogError(
                    ex,
                    "[Scryfall][GetById] Unexpected error for id '{ScryfallId}'.",
                    scryfallId);

                return null;
            }
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyCollection<ScryfallCard>> SearchCardsAsync(string query, CancellationToken cancellationToken)
        {
            var url = $"cards/search?q={Uri.EscapeDataString(query)}";

            try
            {
                using var response = await this.httpClient.GetAsync(url, cancellationToken).ConfigureAwait(false);

                if (!response.IsSuccessStatusCode)
                {
                    var errorBody = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);

                    this.logger.LogWarning(
                        "[Scryfall][Search] HTTP {StatusCode} for query '{Query}'. Body: {Body}",
                        (int)response.StatusCode,
                        query,
                        errorBody);

                    return Array.Empty<ScryfallCard>();
                }

                var list = await response.Content
                    .ReadFromJsonAsync<ScryfallListResponse<ScryfallCard>>(cancellationToken: cancellationToken)
                    .ConfigureAwait(false);

                if (list?.Data is null || list.Data.Count == 0)
                {
                    return Array.Empty<ScryfallCard>();
                }

                return list.Data;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                this.logger.LogError(
                    ex,
                    "[Scryfall][Search] Unexpected error for query '{Query}'.",
                    query);

                return Array.Empty<ScryfallCard>();
            }
        }
    }
}

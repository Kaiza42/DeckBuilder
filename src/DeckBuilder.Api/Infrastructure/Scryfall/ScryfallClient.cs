namespace DeckBuilder.Infrastructure.Scryfall
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading;
    using System.Threading.Tasks;
    using DeckBuilder.Infrastructure.Scryfall.Models;

    /// <summary>
    /// Provides an HTTP-based implementation of <see cref="IScryfallClient"/> using the Scryfall REST API.
    /// </summary>
    public sealed class ScryfallClient : IScryfallClient
    {
        private readonly HttpClient httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScryfallClient"/> class.
        /// </summary>
        /// <param name="httpClient">The HTTP client configured with the Scryfall base address.</param>
        public ScryfallClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        /// <inheritdoc/>
        public async Task<ScryfallCard?> GetCardByIdAsync(string scryfallId, CancellationToken cancellationToken)
        {
            var url = $"cards/{Uri.EscapeDataString(scryfallId)}";

            using var response = await this.httpClient.GetAsync(url, cancellationToken).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
                Console.Error.WriteLine(
                    $"[Scryfall][GetById] HTTP {(int)response.StatusCode} {response.StatusCode} for id '{scryfallId}'. Body: {errorBody}");

                return null;
            }

            var card = await response.Content
                .ReadFromJsonAsync<ScryfallCard>(cancellationToken: cancellationToken)
                .ConfigureAwait(false);

            return card;
        }

        /// <inheritdoc/>
        public async Task<IReadOnlyCollection<ScryfallCard>> SearchCardsAsync(
            string query,
            CancellationToken cancellationToken)
        {
            var url = $"cards/search?q={Uri.EscapeDataString(query)}";

            using var response = await this.httpClient.GetAsync(url, cancellationToken).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                var errorBody = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
                Console.Error.WriteLine(
                    $"[Scryfall][Search] HTTP {(int)response.StatusCode} {response.StatusCode} for query '{query}'. Body: {errorBody}");

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
    }
}

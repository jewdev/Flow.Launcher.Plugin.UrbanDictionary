using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Flow.Launcher.Plugin.UrbanDictionary
{
    /// <summary>
    /// Service for interacting with the Urban Dictionary API.
    /// </summary>
    public class UrbanDictionaryService : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://api.urbandictionary.com/v0";

        /// <summary>
        /// Initializes a new instance of the <see cref="UrbanDictionaryService"/> class.
        /// </summary>
        public UrbanDictionaryService()
        {
            _httpClient = new HttpClient();
			_httpClient.DefaultRequestHeaders.Add("User-Agent", "Flow.Launcher.Plugin.UrbanDictionary");
        }

        /// <summary>
        /// Gets definitions for a specified term from Urban Dictionary.
        /// </summary>
        /// <param name="term">The term to search for.</param>
        /// <returns>A list of definitions for the specified term.</returns>
        public async Task<List<Definition>> GetDefinitionsAsync(string term)
        {
            var response = await _httpClient.GetAsync($"{_baseUrl}/define?term={Uri.EscapeDataString(term)}");
            response.EnsureSuccessStatusCode();
            
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<UrbanDictionaryResponse>(content);
            return result.List ?? new List<Definition>();
        }

        /// <summary>
        /// Disposes of the resources used by the service.
        /// </summary>
        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
} 
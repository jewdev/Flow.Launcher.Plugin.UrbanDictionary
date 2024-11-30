using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Flow.Launcher.Plugin.UrbanDictionary
{
    /// <summary>
    /// Represents the response from the Urban Dictionary API.
    /// </summary>
    public class UrbanDictionaryResponse
    {
        /// <summary>
        /// Gets or sets the list of definitions returned by the API.
        /// </summary>
        [JsonPropertyName("list")]
        public List<Definition> List { get; set; }
    }
} 
using System.Text.Json.Serialization;

namespace Flow.Launcher.Plugin.UrbanDictionary
{
    /// <summary>
    /// Represents a definition entry from Urban Dictionary.
    /// </summary>
    public class Definition
    {
        /// <summary>
        /// Gets or sets the actual definition text.
        /// </summary>
        [JsonPropertyName("definition")]
        public string DefinitionText { get; set; }

        /// <summary>
        /// Gets or sets the permanent URL to the definition on Urban Dictionary.
        /// </summary>
        [JsonPropertyName("permalink")]
        public string Permalink { get; set; }

        /// <summary>
        /// Gets or sets the number of upvotes for this definition.
        /// </summary>
        [JsonPropertyName("thumbs_up")]
        public int ThumbsUp { get; set; }

        /// <summary>
        /// Gets or sets the number of downvotes for this definition.
        /// </summary>
        [JsonPropertyName("thumbs_down")]
        public int ThumbsDown { get; set; }
    }
}

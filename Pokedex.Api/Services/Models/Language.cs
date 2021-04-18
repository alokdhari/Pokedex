using System.Text.Json.Serialization;

namespace Pokedex.Api.Services.Models
{
    /// <summary>
    /// Defines the <see cref="Language" />.
    /// </summary>
    public class Language
    {
        /// <summary>
        /// Gets or sets the LanguageName.
        /// </summary>
        [JsonPropertyName("name")]
        public string LanguageName { get; set; }

        /// <summary>
        /// Gets or sets the Url.
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}

using System.Text.Json.Serialization;

namespace Pokedex.Api.Services.Models
{
    /// <summary>
    /// Defines the <see cref="FlavorTextEntry" />.
    /// </summary>
    public class FlavorTextEntry
    {
        /// <summary>
        /// Gets or sets the FlavorText.
        /// </summary>
        [JsonPropertyName("flavor_text")]
        public string FlavorText { get; set; }

        /// <summary>
        /// Gets or sets the Language.
        /// </summary>
        [JsonPropertyName("language")]
        public Language Language { get; set; }

        /// <summary>
        /// Gets or sets the Version.
        /// </summary>
        [JsonPropertyName("version")]
        public Version Version { get; set; }
    }
}

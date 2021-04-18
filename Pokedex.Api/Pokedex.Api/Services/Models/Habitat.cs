using System.Text.Json.Serialization;

namespace Pokedex.Api.Services.Models
{
    /// <summary>
    /// Defines the <see cref="Habitat" />.
    /// </summary>
    public class Habitat
    {
        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Url.
        /// </summary>
        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}

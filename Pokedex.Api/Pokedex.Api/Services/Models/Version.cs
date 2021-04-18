using System.Text.Json.Serialization;

namespace Pokedex.Api.Services.Models
{

    /// <summary>
    /// Defines the <see cref="Version" />.
    /// </summary>
    public class Version
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

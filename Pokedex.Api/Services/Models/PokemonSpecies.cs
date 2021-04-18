using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Pokedex.Api.Services.Models
{
    /// <summary>
    /// Defines the <see cref="PokemonSpecies" />.
    /// </summary>
    public class PokemonSpecies
    {
        /// <summary>
        /// Gets or sets the FlavorTextEntries.
        /// </summary>
        [JsonPropertyName("flavor_text_entries")]
        public List<FlavorTextEntry> FlavorTextEntries { get; set; }

        /// <summary>
        /// Gets or sets the Habitat.
        /// </summary>
        [JsonPropertyName("habitat")]
        public Habitat Habitat { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsLegendary.
        /// </summary>
        [JsonPropertyName("is_legendary")]
        public bool IsLegendary { get; set; }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}

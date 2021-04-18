using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Pokedex.Api.Models
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

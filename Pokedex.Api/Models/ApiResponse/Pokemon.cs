using Pokedex.Api.Services.Models;
using System.Linq;

namespace Pokedex.Api.Models.ApiResponse
{
    /// <summary>
    /// Defines the <see cref="Pokemon" />.
    /// </summary>
    public class Pokemon
    {
        public Pokemon() {}

        public Pokemon(PokemonSpecies pokemonSpecies)
        {
            Habitat = pokemonSpecies.Habitat?.Name ?? string.Empty;
            IsLegendary = pokemonSpecies.IsLegendary;
            Name = pokemonSpecies.Name;
            StandardDescription = pokemonSpecies.FlavorTextEntries != null && pokemonSpecies.FlavorTextEntries.Any(o => o.Language.LanguageName == "en") ?
                pokemonSpecies.FlavorTextEntries?.FirstOrDefault(o => o.Language.LanguageName == "en").FlavorText : string.Empty;
        }

        /// <summary>
        /// Gets or sets the Habitat.
        /// </summary>
        public string Habitat { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether Is_Legendary.
        /// </summary>
        public bool IsLegendary { get; set; }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the StandardDescription.
        /// </summary>
        public string StandardDescription { get; set; }
    }
}

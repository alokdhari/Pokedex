using Pokedex.Api.Services.Models;
using System.Threading.Tasks;

namespace Pokedex.Api.Services.Pokemon
{
    /// <summary>
    /// Defines the <see cref="IPokemonService" />.
    /// </summary>
    public interface IPokemonService
    {
        /// <summary>
        /// Get a pokemon by it's name.
        /// </summary>
        /// <param name="pokemonName">.</param>
        /// <returns>.</returns>
        Task<Result<PokemonSpecies>> GetPokemonByName(string pokemonName);
    }
}

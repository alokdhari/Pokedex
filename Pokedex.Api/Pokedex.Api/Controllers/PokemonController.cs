using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pokedex.Api.Models;
using Pokedex.Api.Models.ApiResponse;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pokedex.Api.Controllers
{

    /// <summary>
    /// Defines the <see cref="PokemonController" />.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class PokemonController : ControllerBase
    {
        /// <summary>
        /// Get details of a <see cref="Pokemon"/> using it's name
        /// </summary>
        /// <param name="pokemonName">The pokemon's name<see cref="string"/></param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [HttpGet]
        [Route("{pokemonName}")]
        [ProducesResponseType(typeof(Pokemon), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPokemon(string pokemonName)
        {
            try
            {
                var pokemonApiClient = new HttpClient
                {
                    BaseAddress = new Uri("https://pokeapi.co")
                };

                var result = await pokemonApiClient.GetAsync($"/api/v2/pokemon-species/{pokemonName}");

                if (!result.IsSuccessStatusCode)
                {
                    return StatusCode((int)result.StatusCode, "This pokemon is so rare that we just could not find the details for it.");
                }

                var jsonContent = await result.Content.ReadAsStringAsync();
                var pokemonDetailsFromTheApi = JsonSerializer.Deserialize<PokemonSpecies>(jsonContent, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                var pokemon = new Pokemon
                {
                    Name = pokemonDetailsFromTheApi.Name,
                    StandardDescription = pokemonDetailsFromTheApi.FlavorTextEntries.FirstOrDefault(o => o.Language.LanguageName == "en").FlavorText,
                    Habitat = pokemonDetailsFromTheApi.Habitat.Name,
                    IsLegendary = pokemonDetailsFromTheApi.IsLegendary
                };

                return Ok(pokemon);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Get translated details of a <see cref="Pokemon"/> details using it's name
        /// </summary>
        /// <param name="pokemonName">The Pokemon's name<see cref="string"/>.</param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [HttpGet]
        [Route("translated/{pokemonName}")]
        [ProducesResponseType(typeof(Pokemon), StatusCodes.Status200OK)]
        public IActionResult GetTranslatedPokemon(string pokemonName)
        {
            try
            {
                var pokemon = new Pokemon
                {
                    Name = pokemonName
                };

                return Ok(pokemon);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

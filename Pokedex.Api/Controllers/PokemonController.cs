using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pokedex.Api.Models.ApiResponse;
using Pokedex.Api.Services.Pokemon;
using Pokedex.Api.Services.Tranlators;
using System;
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
        private readonly IPokemonService pokemonService;
        private readonly ITranslator translator;
        private readonly ILogger<PokemonController> logger;

        public PokemonController(IPokemonService pokemonService, ITranslator translator, ILogger<PokemonController> logger)
        {
            this.pokemonService = pokemonService;
            this.translator = translator;
            this.logger = logger;
        }

        /// <summary>
        /// Get details of a <see cref="Pokemon"/> using it's name
        /// </summary>
        /// <param name="pokemonName">The pokemon's name<see cref="string"/></param>
        /// <returns>The <see cref="IActionResult"/>.</returns>
        [HttpGet]
        [Route("{pokemonName}")]
        [ProducesResponseType(typeof(Pokemon), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPokemonAsync(string pokemonName)
        {
            try
            {
                var pokemonSpeciesResponse = await pokemonService.GetPokemonByName(pokemonName);
                if (!pokemonSpeciesResponse.Successful)
                {
                    return StatusCode((int)pokemonSpeciesResponse.HttpStatusCode, pokemonSpeciesResponse.Message);
                }

                var pokemonSpecies = pokemonSpeciesResponse.Data;
                var pokemon = new Pokemon(pokemonSpecies);
                return Ok(pokemon);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex);
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
        public async Task<IActionResult> GetTranslatedPokemonAsync(string pokemonName)
        {
            try
            {
                var pokemonSpeciesResponse = await pokemonService.GetPokemonByName(pokemonName);
                if (!pokemonSpeciesResponse.Successful)
                {
                    return StatusCode((int)pokemonSpeciesResponse.HttpStatusCode, pokemonSpeciesResponse.Message);
                }

                var pokemonSpecies = pokemonSpeciesResponse.Data;
                var pokemon = new Pokemon(pokemonSpecies);
                var shouldApplyYodaTranslation = pokemon.Habitat == "cave" || pokemon.IsLegendary;

                if (shouldApplyYodaTranslation)
                {
                    pokemon.StandardDescription = await translator.TranslateToYoda(pokemon.StandardDescription);
                }
                else
                {
                    pokemon.StandardDescription = await translator.TranslateToShakespeare(pokemon.StandardDescription);
                }

                return Ok(pokemon);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message, ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

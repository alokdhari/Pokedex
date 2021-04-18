using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

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
        public IActionResult GetPokemon(string pokemonName)
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

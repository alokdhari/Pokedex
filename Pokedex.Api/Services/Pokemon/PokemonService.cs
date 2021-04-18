using Pokedex.Api.Services.Models;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pokedex.Api.Services.Pokemon
{
    /// <summary>
    /// Defines the <see cref="PokemonService" />.
    /// </summary>
    public class PokemonService : IPokemonService
    {
        /// <inheritdoc />
        public async Task<Result<PokemonSpecies>> GetPokemonByName(string pokemonName)
        {
            var pokemonApiClient = new HttpClient
            {
                BaseAddress = new Uri("https://pokeapi.co")
            };

            var result = await pokemonApiClient.GetAsync($"/api/v2/pokemon-species/{pokemonName}");

            if (!result.IsSuccessStatusCode)
            {
                return new Result<PokemonSpecies>
                {
                    HttpStatusCode = result.StatusCode,
                    Successful = false,
                    Message = "This pokemon is so rare that we just could not find the details for it."
                };
            }

            var jsonContent = await result.Content.ReadAsStringAsync();
            var pokemonDetailsFromTheApi = JsonSerializer.Deserialize<PokemonSpecies>(jsonContent, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return new Result<PokemonSpecies>
            {
                Data = pokemonDetailsFromTheApi,
                Successful = true,
                HttpStatusCode = result.StatusCode,
                Message = "Found the pokemon"
            };
        }
    }
}

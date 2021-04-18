using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pokedex.Api.Tests.ControllerTests
{
    public class PokemonControllerTests
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;

        public PokemonControllerTests()
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
               .UseStartup<Startup>());
            _client = _server.CreateClient();
        }

        [Test]
        public async Task Get_PokemonName_ShouldReturnTheCorrectPokemon()
        {
            // Act
            var name = "pokemonName";
            var response = await _client.GetAsync($"/pokemon/{name}");

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status200OK);

            var apiResponse = await response.Content.ReadAsStringAsync();
            var pokemon = JsonSerializer.Deserialize<Pokedex.Api.Models.Pokemon>(apiResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            pokemon.Name.Should().Be(name);
        }

        [Test]
        public async Task Get_TranslatedPokemonName_ShouldReturnTheCorrectPokemon()
        {
            // Act
            var name = "pokemonName";
            var response = await _client.GetAsync($"/pokemon/translated/{name}");

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            var apiResponse = await response.Content.ReadAsStringAsync();
            var pokemon = JsonSerializer.Deserialize<Pokedex.Api.Models.Pokemon>(apiResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            pokemon.Name.Should().Be(name);
        }
    }
}

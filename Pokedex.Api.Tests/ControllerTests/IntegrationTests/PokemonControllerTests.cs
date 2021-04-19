using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;
using Pokedex.Api.Models.ApiResponse;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pokedex.Api.Tests.ControllerTests.IntegrationTests
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
            var name = "mewtwo";
            var response = await _client.GetAsync($"/pokemon/{name}");

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status200OK);

            var apiResponse = await response.Content.ReadAsStringAsync();
            var pokemon = JsonSerializer.Deserialize<Pokemon>(apiResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            pokemon.Name.Should().Be(name);
            pokemon.Habitat.Should().Be("rare");
            pokemon.IsLegendary.Should().Be(true);
        }

        [Test]
        public async Task Get_PokemonName_ShouldReturnNotFoundIfPokemonDoesNotExist()
        {
            // Act
            var name = "mewtwoihishaidhsaidhisad";
            var response = await _client.GetAsync($"/pokemon/{name}");

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status404NotFound);

            var apiResponse = await response.Content.ReadAsStringAsync();

            apiResponse.Should().Be("This pokemon is so rare that we just could not find the details for it.");
        }

        [Test]
        public async Task Get_TranslatedPokemonName_ShouldReturnTheCorrectPokemon_WithYodaTranslation()
        {
            // Act
            var name = "mewtwo";
            var response = await _client.GetAsync($"/pokemon/translated/{name}");

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            var apiResponse = await response.Content.ReadAsStringAsync();
            var pokemon = JsonSerializer.Deserialize<Pokemon>(apiResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            pokemon.Name.Should().Be(name);
            pokemon.Habitat.Should().Be("rare");
            pokemon.IsLegendary.Should().Be(true);
            
            // There is a rate limit on the tranlation api and this fails if it is hit.
            //pokemon.StandardDescription.Should().Be("Created by\na scientist after\nyears of horrific gene splicing and\ndna engineering\nexperiments,  it was.");
        }

        [Test]
        public async Task Get_TranslatedPokemonName_ShouldReturnTheCorrectPokemon_WithShakespeareTranslation()
        {
            // Act
            var name = "bulbasaur";
            var response = await _client.GetAsync($"/pokemon/translated/{name}");

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status200OK);
            var apiResponse = await response.Content.ReadAsStringAsync();
            var pokemon = JsonSerializer.Deserialize<Pokemon>(apiResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            pokemon.Name.Should().Be(name);
            pokemon.Habitat.Should().Be("grassland");
            pokemon.IsLegendary.Should().Be(false);
            
            // There is a rate limit on the tranlation api and this fails if it is hit. 
            // pokemon.StandardDescription.Should().Be("A strange seed wast\nplanted on its\nback at birth. The plant sprouts\nand grows with\nthis pokémon.");
        }

        [Test]
        public async Task Get_TranslatedPokemonName_ShouldReturnNotFoundIfPokemonDoesNotExist()
        {
            // Act
            var name = "mewtwoihishaidhsaidhisad";
            var response = await _client.GetAsync($"/pokemon/translated/{name}");

            // Assert
            response.StatusCode.Should().Be(StatusCodes.Status404NotFound);

            var apiResponse = await response.Content.ReadAsStringAsync();

            apiResponse.Should().Be("This pokemon is so rare that we just could not find the details for it.");
        }
    }
}

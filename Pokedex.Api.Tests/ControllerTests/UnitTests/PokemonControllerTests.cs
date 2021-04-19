using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Pokedex.Api.Controllers;
using Pokedex.Api.Models.ApiResponse;
using Pokedex.Api.Services.Models;
using Pokedex.Api.Services.Pokemon;
using Pokedex.Api.Services.Tranlators;
using System;
using System.Threading.Tasks;

namespace Pokedex.Api.Tests.ControllerTests.UnitTests
{
    public class PokemonControllerTests
    {
        //public async Task Index_ReturnsAViewResult_WithAListOfBrainstormSessions()
        //{
        //    // Arrange
        //    var mockRepo = new Mock<IBrainstormSessionRepository>();
        //    mockRepo.Setup(repo => repo.ListAsync())
        //        .ReturnsAsync(GetTestSessions());
        //    var controller = new HomeController(mockRepo.Object);

        //    // Act
        //    var result = await controller.Index();

        //    // Assert
        //    var viewResult = Assert.IsType<ViewResult>(result);
        //    var model = Assert.IsAssignableFrom<IEnumerable<StormSessionViewModel>>(
        //        viewResult.ViewData.Model);
        //    Assert.Equal(2, model.Count());
        //}

        [Test]
        public async Task GetPokemonAsync_ShouldCallPokemonService()
        {
            // Arrange
            var mockPokemonService = new Mock<IPokemonService>();
            var mockTranslator = new Mock<ITranslator>();
            var loggerMock = new Mock<ILogger<PokemonController>>();
            var mockController = new PokemonController(mockPokemonService.Object, mockTranslator.Object, loggerMock.Object);

            // Action
            await mockController.GetPokemonAsync("somePokemon");

            // Assert
            mockPokemonService.Verify(o => o.GetPokemonByName(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task GetPokemonAsync_ShouldProcessFailedResponse()
        {
            // Arrange
            var mockPokemonService = new Mock<IPokemonService>();

            var messageGuid = Guid.NewGuid().ToString();

            mockPokemonService.Setup(o => o.GetPokemonByName(It.IsAny<string>()))
                .ReturnsAsync(
                new Result<Services.Models.PokemonSpecies> {
                    HttpStatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Message = messageGuid,
                    Successful = false
                });

            var mockTranslator = new Mock<ITranslator>();
            var loggerMock = new Mock<ILogger<PokemonController>>();
            var mockController = new PokemonController(mockPokemonService.Object, mockTranslator.Object, loggerMock.Object);

            // Action
            var result = await mockController.GetPokemonAsync("somePokemon");

            // Assert

            result.Should().BeOfType<ObjectResult>();

            var actionResult = result as ObjectResult;
            actionResult.Value.Should().BeOfType<string>();

            actionResult.Value.ToString().Should().Be(messageGuid);
        }

        [Test]
        public async Task GetPokemonAsync_ShouldProcessSuccessfulResponse()
        {
            // Arrange
            var mockPokemonService = new Mock<IPokemonService>();

            var messageGuid = Guid.NewGuid().ToString();
            var pokemonGuid = Guid.NewGuid().ToString();

            mockPokemonService.Setup(o => o.GetPokemonByName(It.IsAny<string>()))
                .ReturnsAsync(
                new Result<Services.Models.PokemonSpecies>
                {
                    HttpStatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Message = messageGuid,
                    Data = new PokemonSpecies {
                        Name = pokemonGuid,
                        FlavorTextEntries = new System.Collections.Generic.List<FlavorTextEntry>(),
                        Habitat = new Habitat(),
                        IsLegendary = false
                    },
                    Successful = true
                });

            var mockTranslator = new Mock<ITranslator>();
            var loggerMock = new Mock<ILogger<PokemonController>>();
            var mockController = new PokemonController(mockPokemonService.Object, mockTranslator.Object, loggerMock.Object);

            // Action
            var result = await mockController.GetPokemonAsync(pokemonGuid);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            var actionResult = result as OkObjectResult;
            actionResult.Value.Should().BeOfType<Pokemon>();

            var pokemon = actionResult.Value as Pokemon;

            pokemon.Name.Should().Be(pokemonGuid);
        }

        [Test]
        public async Task GetTranslatedPokemonAsync_ShouldCallPokemonService()
        {
            // Arrange
            var mockPokemonService = new Mock<IPokemonService>();
            var mockTranslator = new Mock<ITranslator>();
            var loggerMock = new Mock<ILogger<PokemonController>>();
            var mockController = new PokemonController(mockPokemonService.Object, mockTranslator.Object, loggerMock.Object);

            // Action
            await mockController.GetTranslatedPokemonAsync("somePokemon");

            // Assert
            mockPokemonService.Verify(o => o.GetPokemonByName(It.IsAny<string>()), Times.Once);
        }

        [Test]
        public async Task GetTranslatedPokemonAsync_ShouldProcessFailedResponse()
        {
            // Arrange
            var mockPokemonService = new Mock<IPokemonService>();

            var messageGuid = Guid.NewGuid().ToString();

            mockPokemonService.Setup(o => o.GetPokemonByName(It.IsAny<string>()))
                .ReturnsAsync(
                new Result<Services.Models.PokemonSpecies>
                {
                    HttpStatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Message = messageGuid,
                    Successful = false
                });

            var mockTranslator = new Mock<ITranslator>();
            var loggerMock = new Mock<ILogger<PokemonController>>();
            var mockController = new PokemonController(mockPokemonService.Object, mockTranslator.Object, loggerMock.Object);

            // Action
            var result = await mockController.GetTranslatedPokemonAsync("somePokemon");

            // Assert

            result.Should().BeOfType<ObjectResult>();

            var actionResult = result as ObjectResult;
            actionResult.Value.Should().BeOfType<string>();

            actionResult.Value.ToString().Should().Be(messageGuid);
        }

        [Test]
        public async Task GetTranslatedPokemonAsync_ShouldProcessSuccessfulResponseAndCallYodaTranlatorWhenHabitatIsCave()
        {
            // Arrange
            var mockPokemonService = new Mock<IPokemonService>();

            var messageGuid = Guid.NewGuid().ToString();
            var pokemonGuid = Guid.NewGuid().ToString();
            var pokemonDescriptionGuid = Guid.NewGuid().ToString();
            var pokemonTranslatedDescriptionGuid = Guid.NewGuid().ToString();

            mockPokemonService.Setup(o => o.GetPokemonByName(pokemonGuid))
                .ReturnsAsync(
                new Result<Services.Models.PokemonSpecies>
                {
                    HttpStatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Message = messageGuid,
                    Data = new PokemonSpecies
                    {
                        Name = pokemonGuid,
                        FlavorTextEntries = new System.Collections.Generic.List<FlavorTextEntry>
                        {
                            new FlavorTextEntry
                            {
                                FlavorText = pokemonDescriptionGuid,
                                Language = new Language
                                {
                                    LanguageName = "en"
                                }
                            }
                        },
                        Habitat = new Habitat { Name = "cave"},
                        IsLegendary = false
                    },
                    Successful = true
                });

            var mockTranslator = new Mock<ITranslator>();

            mockTranslator.Setup(o => o.TranslateToYoda(pokemonDescriptionGuid)).ReturnsAsync(pokemonTranslatedDescriptionGuid);
            var loggerMock = new Mock<ILogger<PokemonController>>();
            var mockController = new PokemonController(mockPokemonService.Object, mockTranslator.Object, loggerMock.Object);

            // Action
            var result = await mockController.GetTranslatedPokemonAsync(pokemonGuid);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            var actionResult = result as OkObjectResult;
            actionResult.Value.Should().BeOfType<Pokemon>();

            var pokemon = actionResult.Value as Pokemon;

            pokemon.Name.Should().Be(pokemonGuid);
            pokemon.StandardDescription.Should().Be(pokemonTranslatedDescriptionGuid);
        }

        [Test]
        public async Task GetTranslatedPokemonAsync_ShouldProcessSuccessfulResponseAndCallYodaTranlatorWhenPokemonIsLegendary()
        {
            // Arrange
            var mockPokemonService = new Mock<IPokemonService>();

            var messageGuid = Guid.NewGuid().ToString();
            var pokemonGuid = Guid.NewGuid().ToString();
            var pokemonDescriptionGuid = Guid.NewGuid().ToString();
            var pokemonTranslatedDescriptionGuid = Guid.NewGuid().ToString();

            mockPokemonService.Setup(o => o.GetPokemonByName(pokemonGuid))
                .ReturnsAsync(
                new Result<Services.Models.PokemonSpecies>
                {
                    HttpStatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Message = messageGuid,
                    Data = new PokemonSpecies
                    {
                        Name = pokemonGuid,
                        FlavorTextEntries = new System.Collections.Generic.List<FlavorTextEntry>
                        {
                            new FlavorTextEntry
                            {
                                FlavorText = pokemonDescriptionGuid,
                                Language = new Language
                                {
                                    LanguageName = "en"
                                }
                            }
                        },
                        Habitat = new Habitat { Name = "notcave" },
                        IsLegendary = true
                    },
                    Successful = true
                });

            var mockTranslator = new Mock<ITranslator>();

            mockTranslator.Setup(o => o.TranslateToYoda(pokemonDescriptionGuid)).ReturnsAsync(pokemonTranslatedDescriptionGuid);
            var loggerMock = new Mock<ILogger<PokemonController>>();
            var mockController = new PokemonController(mockPokemonService.Object, mockTranslator.Object, loggerMock.Object);

            // Action
            var result = await mockController.GetTranslatedPokemonAsync(pokemonGuid);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            var actionResult = result as OkObjectResult;
            actionResult.Value.Should().BeOfType<Pokemon>();

            var pokemon = actionResult.Value as Pokemon;

            pokemon.Name.Should().Be(pokemonGuid);
            pokemon.StandardDescription.Should().Be(pokemonTranslatedDescriptionGuid);
        }

        [Test]
        public async Task GetTranslatedPokemonAsync_ShouldProcessSuccessfulResponseAndCallShakespeareTranlatorWhenPokemonIsNotLegendaryOrOfCaveHobitat()
        {
            // Arrange
            var mockPokemonService = new Mock<IPokemonService>();

            var messageGuid = Guid.NewGuid().ToString();
            var pokemonGuid = Guid.NewGuid().ToString();
            var pokemonDescriptionGuid = Guid.NewGuid().ToString();
            var pokemonTranslatedDescriptionGuid = Guid.NewGuid().ToString();

            mockPokemonService.Setup(o => o.GetPokemonByName(pokemonGuid))
                .ReturnsAsync(
                new Result<Services.Models.PokemonSpecies>
                {
                    HttpStatusCode = System.Net.HttpStatusCode.InternalServerError,
                    Message = messageGuid,
                    Data = new PokemonSpecies
                    {
                        Name = pokemonGuid,
                        FlavorTextEntries = new System.Collections.Generic.List<FlavorTextEntry>
                        {
                            new FlavorTextEntry
                            {
                                FlavorText = pokemonDescriptionGuid,
                                Language = new Language
                                {
                                    LanguageName = "en"
                                }
                            }
                        },
                        Habitat = new Habitat { Name = "notcave" },
                        IsLegendary = false
                    },
                    Successful = true
                });

            var mockTranslator = new Mock<ITranslator>();

            mockTranslator.Setup(o => o.TranslateToShakespeare(pokemonDescriptionGuid)).ReturnsAsync(pokemonTranslatedDescriptionGuid);
            var loggerMock = new Mock<ILogger<PokemonController>>();
            var mockController = new PokemonController(mockPokemonService.Object, mockTranslator.Object, loggerMock.Object);

            // Action
            var result = await mockController.GetTranslatedPokemonAsync(pokemonGuid);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            var actionResult = result as OkObjectResult;
            actionResult.Value.Should().BeOfType<Pokemon>();

            var pokemon = actionResult.Value as Pokemon;

            pokemon.Name.Should().Be(pokemonGuid);
            pokemon.StandardDescription.Should().Be(pokemonTranslatedDescriptionGuid);
        }
    }
}

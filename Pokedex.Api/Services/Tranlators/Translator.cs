using Microsoft.Extensions.Logging;
using Pokedex.Api.Services.Models;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Pokedex.Api.Services.Tranlators
{
    /// <inheritdoc />
    public class Translator : ITranslator
    {
        private readonly ILogger<Translator> logger;

        public Translator(ILogger<Translator> logger)
        {
            this.logger = logger;
        }

        /// <inheritdoc />
        public async Task<string> TranslateToShakespeare(string textToTranslate)
        {
            try
            {
                var translator = new HttpClient
                {
                    BaseAddress = new Uri("https://api.funtranslations.com")
                };

                var encodedText = textToTranslate.Replace("\n", "\\n").Replace("\r", "").Replace(" ", "%20");

                var result = await translator.GetAsync($"/translate/shakespeare.json?text={encodedText}");

                if (!result.IsSuccessStatusCode)
                {
                    return textToTranslate;
                }

                var jsonContent = await result.Content.ReadAsStringAsync();
                var translationResponse = JsonSerializer.Deserialize<TranslationResponse>(jsonContent, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return translationResponse.Contents.Translated.Replace("\\n", "\n");
            }
            catch(Exception ex)
            {
                logger.LogError(ex, $"Error translating text :\"{textToTranslate}\" to Shakespeare");
                return textToTranslate;
            }
        }

        /// <inheritdoc />
        public async Task<string> TranslateToYoda(string textToTranslate)
        {
            try
            {
                var translator = new HttpClient
                {
                    BaseAddress = new Uri("https://api.funtranslations.com")
                };

                var encodedText = textToTranslate.Replace("\n", "\\n").Replace("\r", "").Replace(" ", "%20");

                var result = await translator.GetAsync($"/translate/yoda.json?text={encodedText}");

                if (!result.IsSuccessStatusCode)
                {
                    return textToTranslate;
                }

                var jsonContent = await result.Content.ReadAsStringAsync();
                var translationResponse = JsonSerializer.Deserialize<TranslationResponse>(jsonContent, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return translationResponse.Contents.Translated.Replace("\\n", "\n");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error translating text :\"{textToTranslate}\" to Shakespeare");
                return textToTranslate;
            }
        }
    }
}

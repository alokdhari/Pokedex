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
        /// <inheritdoc />
        public async Task<string> TranslateToShakespeare(string textToTranslate)
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

        /// <inheritdoc />
        public async Task<string> TranslateToYoda(string textToTranslate)
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
    }
}

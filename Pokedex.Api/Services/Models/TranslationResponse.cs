using System.Text.Json.Serialization;

namespace Pokedex.Api.Services.Models
{
    public class TranslationResponse
    {
        [JsonPropertyName("success")]
        public Success Success { get; set; }

        [JsonPropertyName("contents")]
        public Contents Contents { get; set; }
    }
}

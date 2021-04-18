using System.Text.Json.Serialization;

namespace Pokedex.Api.Services.Models
{
    public class Success
    {
        [JsonPropertyName("total")]
        public int Total { get; set; }
    }
}

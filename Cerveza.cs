using System.Text.Json;
using System.Text.Json.Serialization;

namespace EspacioPersonaje
{
    public class Cerveza{

        // [JsonProperty("id")]
        // public int id { get; set; }
        [JsonPropertyName("name")]
        public string Nombre { get; set; }
    }
}
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AzureDevopsTracker.DTOs
{
    public class Resource
    {
        [JsonPropertyName("id")]
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonPropertyName("fields")]
        [JsonProperty("fields")]
        public Fields Fields { get; set; }
    }
}

using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AzureDevopsTracker.DTOs.Create
{
    public class CreateWorkItemDTO
    {
        [JsonPropertyName("resource")]
        [JsonProperty("resource")]
        public Resource Resource { get; set; }
    }

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
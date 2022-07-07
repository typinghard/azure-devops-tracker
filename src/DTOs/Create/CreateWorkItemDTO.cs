using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AzureDevopsTracker.DTOs.Create
{
    public record CreateWorkItemDTO
    {
        [JsonPropertyName("resource")]
        [JsonProperty("resource")]
        public Resource Resource { get; init; }
    }
}
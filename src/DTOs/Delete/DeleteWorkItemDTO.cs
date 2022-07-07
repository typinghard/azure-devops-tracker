using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AzureDevopsTracker.DTOs.Delete
{
    public record DeleteWorkItemDTO
    {
        [JsonPropertyName("resource")]
        [JsonProperty("resource")]
        public Resource Resource { get; init; }
    }
}

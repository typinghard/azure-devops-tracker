using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace AzureDevopsTracker.DTOs.Restore
{
    public record RestoreWorkItemDTO
    {
        [JsonPropertyName("resource")]
        [JsonProperty("resource")]
        public Resource Resource { get; init; }
    }
}

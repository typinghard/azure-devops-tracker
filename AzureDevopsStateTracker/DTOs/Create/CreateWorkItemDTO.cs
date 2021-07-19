using Newtonsoft.Json;

namespace AzureDevopsStateTracker.DTOs.Create
{
    public class CreateWorkItemDTO
    {
        [JsonProperty("resource")]
        public Resource Resource { get; set; }
    }

    public class Resource
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("fields")]
        public Fields Fields { get; set; }
    }
}
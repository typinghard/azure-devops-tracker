using Newtonsoft.Json;
using System;

namespace AzureDevopsStateTracker.DTOs.Create
{
    public class CreateWorkItemDTO
    {
        [JsonProperty("resource")]
        public ResourceDTO Resource { get; set; }
    }

    public class ResourceDTO
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("fields")]
        public FieldsDTO Fields { get; set; }
    }

    public class RevisionDTO
    {
        [JsonProperty("fields")]
        public FieldsDTO Fields { get; set; }
    }

    public class FieldsDTO
    {
        [JsonProperty("System.AssignedTo")]
        public string AssignedTo { get; set; }

        [JsonProperty("System.WorkItemType")]
        public string Type { get; set; }

        [JsonProperty("System.CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("System.CreatedBy")]
        public string CreatedBy { get; set; }

        [JsonProperty("System.State")]
        public string State { get; set; }

        [JsonProperty("System.Title")]
        public string Title { get; set; }
    }

}
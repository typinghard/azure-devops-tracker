using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace AzureDevopsTracker.DTOs.Update
{
    public class UpdatedWorkItemDTO
    {
        [JsonPropertyName("resource")]
        [JsonProperty("resource")]
        public Resource Resource { get; set; }
    }

    public class Resource
    {
        [JsonPropertyName("workItemId")]
        [JsonProperty("workItemId")]
        public string WorkItemId { get; set; }

        [JsonPropertyName("fields")]
        [JsonProperty("fields")]
        public ResourceFields Fields { get; set; }

        [JsonPropertyName("revision")]
        [JsonProperty("revision")]
        public Revision Revision { get; set; }
    }

    public class Revision
    {
        [JsonPropertyName("fields")]
        [JsonProperty("fields")]
        public Fields Fields { get; set; }
    }

    public class ResourceFields
    {
        [JsonPropertyName("System.State")]
        [JsonProperty("System.State")]
        public OldNewValues State { get; set; }

        [JsonPropertyName("Microsoft.VSTS.Common.StateChangeDate")]
        [JsonProperty("Microsoft.VSTS.Common.StateChangeDate")]
        public DateTimeOldNewValues StateChangeDate { get; set; }

        [JsonPropertyName("System.ChangedBy")]
        [JsonProperty("System.ChangedBy")]
        public OldNewValues ChangedBy { get; set; }
    }

    public class OldNewValues
    {
        [JsonPropertyName("oldValue")]
        [JsonProperty("oldValue")]
        public string OldValue { get; set; }

        [JsonPropertyName("newValue")]
        [JsonProperty("newValue")]
        public string NewValue { get; set; }
    }

    public class DateTimeOldNewValues
    {
        [JsonPropertyName("oldValue")]
        [JsonProperty("oldValue")]
        public DateTime OldValue { get; set; }

        [JsonPropertyName("newValue")]
        [JsonProperty("newValue")]
        public DateTime NewValue { get; set; }
    }

}
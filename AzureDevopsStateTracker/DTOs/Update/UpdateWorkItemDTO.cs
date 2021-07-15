using Newtonsoft.Json;
using System;

namespace AzureDevopsStateTracker.DTOs.Update
{
    public class UpdatedWorkItemDTO
    {
        [JsonProperty("resource")]
        public Resource Resource { get; set; }
    }

    public class Resource
    {
        [JsonProperty("workItemId")]
        public string WorkItemId { get; set; }

        [JsonProperty("fields")]
        public ResourceFields Fields { get; set; }

        [JsonProperty("revision")]
        public Revision Revision { get; set; }
    }

    public class Revision
    {
        [JsonProperty("fields")]
        public Fields Fields { get; set; }
    }

    public class ResourceFields
    {
        [JsonProperty("System.State")]
        public OldNewValues State { get; set; }

        [JsonProperty("Microsoft.VSTS.Common.StateChangeDate")]
        public DateTimeOldNewValues StateChangeDate { get; set; }

        [JsonProperty("System.ChangedBy")]
        public ChangedByOldNewValues ChangedBy { get; set; }
    }

    public class OldNewValues
    {
        [JsonProperty("oldValue")]
        public string OldValue { get; set; }

        [JsonProperty("newValue")]
        public string NewValue { get; set; }
    }

    public class DateTimeOldNewValues
    {
        [JsonProperty("oldValue")]
        public DateTime OldValue { get; set; }

        [JsonProperty("newValue")]
        public DateTime NewValue { get; set; }
    }

    public class ChangedByOldNewValues
    {
        [JsonProperty("oldValue")]
        public string OldValue { get; set; }

        [JsonProperty("newValue")]
        public string NewValue { get; set; }
    }
}
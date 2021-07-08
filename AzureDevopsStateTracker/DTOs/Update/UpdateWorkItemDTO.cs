using Newtonsoft.Json;
using System;

namespace AzureDevopsStateTracker.DTOs.Update
{
    public class UpdatedWorkItemDTO
    {
        [JsonProperty("resource")]
        public ResourceDTO Resource { get; set; }
    }

    public class ResourceDTO
    {
        [JsonProperty("workItemId")]
        public string WorkItemId { get; set; }

        [JsonProperty("fields")]
        public ResourceFieldsDTO Fields { get; set; }

        [JsonProperty("revision")]
        public RevisionDTO Revision { get; set; }
    }

    public class RevisionDTO
    {
        [JsonProperty("fields")]
        public RevisionFieldsDTO Fields { get; set; }
    }

    public class ResourceFieldsDTO
    {
        [JsonProperty("System.State")]
        public OldNewValues State { get; set; }

        [JsonProperty("Microsoft.VSTS.Common.StateChangeDate")]
        public DateTimeOldNewValues StateChangeDate { get; set; }
    }

    public class RevisionFieldsDTO
    {
        [JsonProperty("System.AssignedTo")]
        public string AssignedTo { get; set; }
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


}
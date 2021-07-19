using Newtonsoft.Json;
using System;

namespace AzureDevopsStateTracker.DTOs
{
    public class Fields
    {
        [JsonProperty("System.AreaPath")]
        public string AreaPath { get; set; }

        [JsonProperty("System.TeamProject")]
        public string TeamProject { get; set; }

        [JsonProperty("System.IterationPath")]
        public string IterationPath { get; set; }

        [JsonProperty("System.AssignedTo")]
        public string AssignedTo { get; set; }

        [JsonProperty("System.WorkItemType")]
        public string Type { get; set; }

        [JsonProperty("System.CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [JsonProperty("System.CreatedBy")]
        public string CreatedBy { get; set; }

        [JsonProperty("System.ChangedBy")]
        public string ChangedBy { get; set; }

        [JsonProperty("System.State")]
        public string State { get; set; }

        [JsonProperty("System.Title")]
        public string Title { get; set; }

        [JsonProperty("System.Tags")]
        public string Tags { get; set; }

        [JsonProperty("System.Parent")]
        public string Parent { get; set; }     

        [JsonProperty("Microsoft.VSTS.Scheduling.StoryPoints")]
        public string StoryPoints { get; set; }

        [JsonProperty("Microsoft.VSTS.Scheduling.OriginalEstimate")]
        public string OriginalEstimate { get; set; }

        [JsonProperty("Microsoft.VSTS.Scheduling.RemainingWork")]
        public string RemainingWork { get; set; }

        [JsonProperty("Microsoft.VSTS.Scheduling.Effort")]
        public string Effort { get; set; }

        [JsonProperty("Microsoft.VSTS.Common.Activity")]
        public string Activity { get; set; }
    }
}
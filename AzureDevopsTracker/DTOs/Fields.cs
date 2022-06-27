using Newtonsoft.Json;
using System;
using System.Text.Json.Serialization;

namespace AzureDevopsTracker.DTOs
{
    public class Fields
    {
        [JsonPropertyName("System.AreaPath")]
        [JsonProperty("System.AreaPath")]
        public string AreaPath { get; set; }

        [JsonPropertyName("System.TeamProject")]
        [JsonProperty("System.TeamProject")]
        public string TeamProject { get; set; }

        [JsonPropertyName("System.IterationPath")]
        [JsonProperty("System.IterationPath")]
        public string IterationPath { get; set; }

        [JsonPropertyName("System.AssignedTo")]
        [JsonProperty("System.AssignedTo")]
        public string AssignedTo { get; set; }

        [JsonPropertyName("System.WorkItemType")]
        [JsonProperty("System.WorkItemType")]
        public string Type { get; set; }

        [JsonPropertyName("System.CreatedDate")]
        [JsonProperty("System.CreatedDate")]
        public DateTime CreatedDate { get; set; }

        [JsonPropertyName("System.CreatedBy")]
        [JsonProperty("System.CreatedBy")]
        public string CreatedBy { get; set; }

        [JsonPropertyName("System.ChangedBy")]
        [JsonProperty("System.ChangedBy")]
        public string ChangedBy { get; set; }

        [JsonPropertyName("System.State")]
        [JsonProperty("System.State")]
        public string State { get; set; }

        [JsonPropertyName("System.Title")]
        [JsonProperty("System.Title")]
        public string Title { get; set; }

        [JsonPropertyName("System.Tags")]
        [JsonProperty("System.Tags")]
        public string Tags { get; set; }

        [JsonPropertyName("System.Parent")]
        [JsonProperty("System.Parent")]
        public string Parent { get; set; }

        [JsonPropertyName("Microsoft.VSTS.Scheduling.StoryPoints")]
        [JsonProperty("Microsoft.VSTS.Scheduling.StoryPoints")]
        public string StoryPoints { get; set; }

        [JsonPropertyName("Microsoft.VSTS.Scheduling.OriginalEstimate")]
        [JsonProperty("Microsoft.VSTS.Scheduling.OriginalEstimate")]
        public string OriginalEstimate { get; set; }

        [JsonPropertyName("Microsoft.VSTS.Scheduling.RemainingWork")]
        [JsonProperty("Microsoft.VSTS.Scheduling.RemainingWork")]
        public string RemainingWork { get; set; }

        [JsonPropertyName("Microsoft.VSTS.Scheduling.Effort")]
        [JsonProperty("Microsoft.VSTS.Scheduling.Effort")]
        public string Effort { get; set; }

        [JsonPropertyName("Microsoft.VSTS.Common.Activity")]
        [JsonProperty("Microsoft.VSTS.Common.Activity")]
        public string Activity { get; set; }

        [JsonProperty("Custom.ChangeLogDescription")]
        public string ChangeLogDescription { get; set; }
    }
}
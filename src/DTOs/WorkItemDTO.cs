using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AzureDevopsTracker.DTOs
{
    public record WorkItemDTO
    {
        [JsonProperty("id")]
        public string Id { get; init; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; init; }

        [JsonProperty("assigned_to")]
        public string AssignedTo { get; init; }

        [JsonProperty("type")]
        public string Type { get; init; }

        [JsonProperty("effort")]
        public string Effort { get; init; }

        [JsonProperty("story_points")]
        public string StoryPoints { get; init; }

        [JsonProperty("original_estimate")]
        public string OriginalEstimate { get; init; }

        [JsonProperty("created_by")]
        public string CreatedBy { get; init; }

        [JsonProperty("title")]
        public string Title { get; init; }

        [JsonProperty("team_project")]
        public string TeamProject { get; init; }

        [JsonProperty("iteration_path")]
        public string IterationPath { get; init; }

        [JsonProperty("area_path")]
        public string AreaPath { get; init; }

        [JsonProperty("current_status")]
        public string CurrentStatus { get; init; }

        [JsonProperty("work_item_parent_id")]
        public string WorkItemParentId { get; init; }

        [JsonProperty("activity")]
        public string Activity { get; init; }

        [JsonProperty("tags")]
        public IEnumerable<string> Tags { get; init; }

        [JsonProperty("workItems_changes")]
        public List<WorkItemChangeDTO> WorkItemsChangesDTO { get; init; }

        [JsonProperty("times_by_state")]
        public List<TimeByStateDTO> TimesByStateDTO { get; init; }
    }

    public record WorkItemChangeDTO
    {
        [JsonProperty("new_date")]
        public DateTime NewDate { get; init; }

        [JsonProperty("new_state")]
        public string NewState { get; init; }

        [JsonProperty("old_state")]
        public string OldState { get; init; }

        [JsonProperty("old_date")]
        public DateTime? OldDate { get; init; }

        [JsonProperty("changed_by")]
        public string ChangedBy { get; init; }
    }


    public record TimeByStateDTO
    {
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; init; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; init; }

        [JsonProperty("state")]
        public string State { get; init; }

        [JsonProperty("total_time")]
        public string TotalTime { get; init; }

        [JsonProperty("total_worked_time")]
        public string TotalWorkedTime { get; init; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AzureDevopsStateTracker.DTOs
{
    public class WorkItemDTO
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("assigned_to")]
        public string AssignedTo { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("effort")]
        public string Effort { get; set; }

        [JsonProperty("story_points")]
        public string StoryPoints { get; set; }

        [JsonProperty("original_estimate")]
        public string OriginalEstimate { get; set; }

        [JsonProperty("created_by")]
        public string CreatedBy { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("team_project")]
        public string TeamProject { get; set; }

        [JsonProperty("iteration_path")]
        public string IterationPath { get; set; }

        [JsonProperty("area_path")]
        public string AreaPath { get; set; }

        [JsonProperty("current_status")]
        public string CurrentStatus { get; set; }

        [JsonProperty("work_item_parent_id")]
        public string WorkItemParentId { get; set; }

        [JsonProperty("activity")]
        public string Activity { get; set; }

        [JsonProperty("tags")]
        public IEnumerable<string> Tags { get; set; }

        [JsonProperty("workItems_changes")]
        public List<WorkItemChangeDTO> WorkItemsChangesDTO { get; set; }

        [JsonProperty("times_by_state")]
        public List<TimeByStateDTO> TimesByStateDTO { get; set; }
    }

    public class WorkItemChangeDTO
    {
        [JsonProperty("new_date")]
        public DateTime NewDate { get; set; }

        [JsonProperty("new_state")]
        public string NewState { get; set; }

        [JsonProperty("old_state")]
        public string OldState { get; set; }

        [JsonProperty("old_date")]
        public DateTime? OldDate { get; set; }

        [JsonProperty("changed_by")]
        public string ChangedBy { get; set; }
    }


    public class TimeByStateDTO
    {
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("total_time")]
        public string TotalTime { get; set; }

        [JsonProperty("total_worked_time")]
        public string TotalWorkedTime { get; set; }
    }
}

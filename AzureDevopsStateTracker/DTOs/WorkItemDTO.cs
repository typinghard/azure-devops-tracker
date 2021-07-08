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

        [JsonProperty("created_by")]
        public string CreatedBy { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("team_project")]
        public string TeamProject { get; set; }

        [JsonProperty("current_status")]
        public string CurrentStatus { get; set; }

        [JsonProperty("workItems_changes")]
        public List<WorkItemChangeDTO> WorkItemsChangesDTO { get; set; }

        [JsonProperty("workItems_status_time")]
        public List<WorkItemStatusTimeDTO> WorkItemsStatusTimeDTO { get; set; }

        [JsonProperty("total_time_by_state")]
        public IEnumerable<Dictionary<string, string>> TotalTimeByState { get; set; }
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
    }


    public class WorkItemStatusTimeDTO
    {
        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("total_time")]
        public string TotalTime { get; set; }
    }
}

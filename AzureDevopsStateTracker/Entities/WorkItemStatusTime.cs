using AzureDevopsStateTracker.Extensions;
using System;

namespace AzureDevopsStateTracker.Entities
{
    public class WorkItemStatusTime : Entity
    {
        public string WorkItemId { get; private set; }
        public string State { get; private set; }
        public TimeSpan TotalTime { get; private set; }
        public WorkItem WorkItem { get; private set; }

        private WorkItemStatusTime() { }

        public WorkItemStatusTime(string workItemId, string state, TimeSpan totalTime)
        {
            WorkItemId = workItemId;
            State = state;
            TotalTime = totalTime;

            Validate();
        }

        public void Validate()
        {
            if (WorkItemId.IsNullOrEmpty() || Convert.ToInt64(WorkItemId) <= 0)
                throw new Exception("WorkItemId is required");

            if (State.IsNullOrEmpty())
                throw new Exception("State is required");

            if (TotalTime == null || TotalTime == TimeSpan.MinValue)
                throw new Exception("TotalTime is required");
        }
    }
}
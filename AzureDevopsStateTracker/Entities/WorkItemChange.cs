using AzureDevopsStateTracker.Extensions;
using System;

namespace AzureDevopsStateTracker.Entities
{
    public class WorkItemChange : Entity
    {
        public string WorkItemId { get; private set; }
        public DateTime NewDate { get; private set; }
        public DateTime? OldDate { get; private set; }
        public string NewState { get; private set; }
        public string OldState { get; private set; }
        public WorkItem WorkItem { get; private set; }

        private WorkItemChange() { }

        public WorkItemChange(string workItemId, DateTime newDate, string newState, string oldState, DateTime? oldDate)
        {
            WorkItemId = workItemId;
            NewDate = newDate;
            OldDate = oldDate;
            NewState = newState;
            OldState = oldState;

            Validate();
        }

        public void Validate()
        {
            if (WorkItemId.IsNullOrEmpty() || Convert.ToInt64(WorkItemId) <= 0)
                throw new Exception("WorkItemId is required");
        }

        public TimeSpan CalculateTotalTime()
        {
            return OldDate == null ? TimeSpan.Zero : NewDate - OldDate.GetValueOrDefault();
        }
    }
}
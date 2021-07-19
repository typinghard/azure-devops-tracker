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
        public string ChangedBy { get; private set; }
        public WorkItem WorkItem { get; private set; }

        private WorkItemChange() { }

        public WorkItemChange(string workItemId, string changedBy, DateTime newDate, string newState, string oldState, DateTime? oldDate)
        {
            WorkItemId = workItemId;
            NewDate = newDate;
            OldDate = oldDate;
            NewState = newState;
            OldState = oldState;
            ChangedBy = changedBy;

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

        public TimeSpan CalculateTotalWorkedTime()
        {
            if (OldDate.GetValueOrDefault() == DateTime.MinValue)
                return TimeSpan.Zero;

            TimeSpan hoursWorked = TimeSpan.Zero;
            for (var i = OldDate.GetValueOrDefault(); i <= NewDate; i = i.AddHours(1))
            {
                if (i.DayOfWeek != DayOfWeek.Saturday && i.DayOfWeek != DayOfWeek.Sunday)
                {
                    if ((i.TimeOfDay.Hours >= 8 && i.TimeOfDay.Hours < 12) || (i.TimeOfDay.Hours >= 14 && i.TimeOfDay.Hours < 18))
                        hoursWorked += (NewDate.TimeOfDay - i.TimeOfDay);
                }
            }

            return hoursWorked;
        }
    }
}
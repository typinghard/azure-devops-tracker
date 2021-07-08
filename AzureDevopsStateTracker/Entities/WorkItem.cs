using AzureDevopsStateTracker.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AzureDevopsStateTracker.Entities
{
    public class WorkItem : Entity
    {
        public string AssignedTo { get; private set; }
        public string Type { get; private set; }
        public string CreatedBy { get; private set; }
        public string Title { get; private set; }
        public string TeamProject { get; private set; }

        private readonly List<WorkItemChange> _workItemsChanges;
        public IReadOnlyCollection<WorkItemChange> WorkItemsChanges => _workItemsChanges;

        private readonly List<WorkItemStatusTime> _workItemsStatusTime;
        public IReadOnlyCollection<WorkItemStatusTime> WorkItemsStatusTime => _workItemsStatusTime;
        public string CurrentStatus => _workItemsChanges?.OrderBy(x => x.CreatedAt)?.LastOrDefault()?.NewState;

        public WorkItem() { }

        public WorkItem(string workItemId, string title, string teamProject, string type, string createdBy) : base(workItemId)
        {
            TeamProject = teamProject;
            Type = type;
            Title = title;
            CreatedBy = createdBy;

            _workItemsChanges = new List<WorkItemChange>();
            _workItemsStatusTime = new List<WorkItemStatusTime>();
            Validate();
        }

        public void Validate()
        {
            if (Id.IsNullOrEmpty())
                throw new Exception("WorkItemId is required");
        }

        public void AddWorkItemChange(WorkItemChange workItemChange)
        {
            if (workItemChange == null)
                throw new Exception("WorkItemChange is null");

            _workItemsChanges.Add(workItemChange);
        }

        public void AddWorkItemStatusTime(WorkItemStatusTime workItemStatusTime)
        {
            if (workItemStatusTime == null)
                throw new Exception("WorkItemStatusTime is null");

            _workItemsStatusTime.Add(workItemStatusTime);
        }

        public void UpdateAssignedTo(string assignedTo)
        {
            AssignedTo = assignedTo;
        }

        public IEnumerable<Dictionary<string, string>> CalculateTotalTimeByState()
        {
            var totalTimeByStateList = new List<Dictionary<string, string>>();
            if (!_workItemsStatusTime.Any())
                return totalTimeByStateList;

            foreach (var item in _workItemsStatusTime.OrderBy(x => x.CreatedAt).GroupBy(x => x.State))
                totalTimeByStateList.Add(new Dictionary<string, string>() {
                    { item.Key, item.Select(x => x.TotalTime).Sum().ToString() }
                });

            return totalTimeByStateList;
        }
    }
}
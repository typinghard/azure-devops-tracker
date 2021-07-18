using AzureDevopsStateTracker.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AzureDevopsStateTracker.Entities
{
    public class WorkItem : Entity
    {
        public string AreaPath { get; private set; }
        public string TeamProject { get; private set; }
        public string IterationPath { get; private set; }
        public string AssignedTo { get; private set; }
        public string Type { get; private set; }
        public string CreatedBy { get; private set; }
        public string Title { get; private set; }
        public string Tags { get; private set; }
        public string Effort { get; private set; }
        public string OriginalEstimate { get; private set; }
        public string StoryPoints { get; private set; }
        public string WorkItemParentId { get; private set; }
        public string Activity { get; private set; }

        private readonly List<WorkItemChange> _workItemsChanges;
        public IReadOnlyCollection<WorkItemChange> WorkItemsChanges => _workItemsChanges;

        private readonly List<TimeByState> _timeByState;
        public IReadOnlyCollection<TimeByState> TimeByStates => _timeByState;
        public string CurrentStatus => _workItemsChanges?.OrderBy(x => x.CreatedAt)?.LastOrDefault()?.NewState;

        private WorkItem()
        {
            _workItemsChanges = new List<WorkItemChange>();
            _timeByState = new List<TimeByState>();
        }

        public WorkItem(string workItemId) : base(workItemId)
        {
            _workItemsChanges = new List<WorkItemChange>();
            _timeByState = new List<TimeByState>();
            Validate();
        }

        public void Update(string title,
                           string teamProject, string areaPath,
                           string iterationPath, string type,
                           string createdBy, string assignedTo,
                           string tags,
                           string workItemParentId,
                           string effort,
                           string storyPoint,
                           string originalEstimate,
                           string activity)
        {
            TeamProject = teamProject;
            AreaPath = areaPath;
            IterationPath = iterationPath;
            Type = type;
            Title = title;
            CreatedBy = createdBy;
            AssignedTo = assignedTo;
            Tags = tags;
            WorkItemParentId = workItemParentId;
            Effort = effort;
            StoryPoints = storyPoint;
            OriginalEstimate = originalEstimate;
            Activity = activity;
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

        public void AddTimeByState(TimeByState timeByState)
        {
            if (timeByState == null)
                throw new Exception("TimeByState is null");

            _timeByState.Add(timeByState);
        }

        public void AddTimesByState(IEnumerable<TimeByState> timesByState)
        {
            if (!timesByState.Any())
                return;

            foreach (var timeByState in timesByState)
                AddTimeByState(timeByState);
        }

        public void ClearTimesByState()
        {
            _timeByState.Clear();
        }

        public IEnumerable<TimeByState> CalculateTotalTimeByState()
        {
            var timesByStateList = new List<TimeByState>();
            if (!_workItemsChanges.Any())
                return timesByStateList;

            foreach (var workItemChange in _workItemsChanges.OrderBy(x => x.CreatedAt).GroupBy(x => x.OldState).Where(x => x.Key != null))
            {
                var totalTime = TimeSpan.Zero;
                var totalWorkedTime = TimeSpan.Zero;
                foreach (var data in workItemChange)
                {
                    totalTime += data.CalculateTotalTime();
                    totalWorkedTime += data.CalculateTotalWorkedTime();
                }

                timesByStateList.Add(new TimeByState(Id, workItemChange.Key, totalTime, totalWorkedTime));
            }

            return timesByStateList;
        }
    }
}
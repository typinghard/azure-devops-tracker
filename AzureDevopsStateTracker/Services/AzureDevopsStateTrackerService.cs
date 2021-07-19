using AzureDevopsStateTracker.DTOs;
using AzureDevopsStateTracker.DTOs.Create;
using AzureDevopsStateTracker.DTOs.Update;
using AzureDevopsStateTracker.Entities;
using AzureDevopsStateTracker.Extensions;
using AzureDevopsStateTracker.Interfaces;
using AzureDevopsStateTracker.Interfaces.Internals;
using System;
using System.Linq;

namespace AzureDevopsStateTracker.Services
{
    public class AzureDevopsStateTrackerService : IAzureDevopsStateTrackerService
    {
        public readonly IWorkItemRepository _workItemRepository;
        public readonly IWorkItemAdapter _workItemAdapter;

        public AzureDevopsStateTrackerService(
            IWorkItemAdapter workItemAdapter, IWorkItemRepository workItemRepository)
        {
            _workItemAdapter = workItemAdapter;
            _workItemRepository = workItemRepository;
        }

        public void Create(CreateWorkItemDTO create)
        {
            var workItem = new WorkItem(create.Resource.Id);

            workItem.Update(create.Resource.Fields.Title,
                            create.Resource.Fields.TeamProject,
                            create.Resource.Fields.AreaPath,
                            create.Resource.Fields.IterationPath,
                            create.Resource.Fields.Type,
                            create.Resource.Fields.CreatedBy.ExtractEmail(),
                            create.Resource.Fields.AssignedTo.ExtractEmail(),
                            create.Resource.Fields.Tags,
                            create.Resource.Fields.Parent,
                            create.Resource.Fields.Effort,
                            create.Resource.Fields.StoryPoints,
                            create.Resource.Fields.OriginalEstimate,
                            create.Resource.Fields.Activity);

            AddWorkItemChange(workItem, create);

            _workItemRepository.Add(workItem);
            _workItemRepository.SaveChanges();
        }

        public void Update(UpdatedWorkItemDTO update)
        {
            var workItem = _workItemRepository.GetByWorkItemId(update.Resource.WorkItemId);
            if (workItem is null)
                return;

            workItem.Update(update.Resource.Revision.Fields.Title,
                            update.Resource.Revision.Fields.TeamProject,
                            update.Resource.Revision.Fields.AreaPath,
                            update.Resource.Revision.Fields.IterationPath,
                            update.Resource.Revision.Fields.Type,
                            update.Resource.Revision.Fields.CreatedBy,
                            update.Resource.Revision.Fields.AssignedTo,
                            update.Resource.Revision.Fields.Tags,
                            update.Resource.Revision.Fields.Parent,
                            update.Resource.Revision.Fields.Effort,
                            update.Resource.Revision.Fields.StoryPoints,
                            update.Resource.Revision.Fields.OriginalEstimate,
                            update.Resource.Revision.Fields.Activity);

            AddWorkItemChange(workItem, update);

            _workItemRepository.Update(workItem);
            _workItemRepository.SaveChanges();
        }

        public WorkItemDTO GetByWorkItemId(string workItemId)
        {
            var workItem = _workItemRepository.GetByWorkItemId(workItemId);
            if (workItem is null)
                return null;

            return _workItemAdapter.ToWorkItemDTO(workItem);
        }

        #region Support Methods
        public WorkItemChange ToWorkItemChange(string workItemId, string changedBy, DateTime newDate, string newState, string oldState = null, DateTime? oldDate = null)
        {
            return new WorkItemChange(workItemId, changedBy.ExtractEmail(), newDate, newState, oldState, oldDate);
        }

        public void AddWorkItemChange(WorkItem workItem, CreateWorkItemDTO create)
        {
            var workItemChange = ToWorkItemChange(workItem.Id,
                                                  create.Resource.Fields.ChangedBy,
                                                  create.Resource.Fields.CreatedDate,
                                                  create.Resource.Fields.State);

            workItem.AddWorkItemChange(workItemChange);
        }

        public void AddWorkItemChange(WorkItem workItem, UpdatedWorkItemDTO update)
        {
            var changedBy = update.Resource.Revision.Fields.ChangedBy ?? update.Resource.Fields.ChangedBy.NewValue;
            var workItemChange = ToWorkItemChange(workItem.Id,
                                      changedBy,
                                      update.Resource.Fields.StateChangeDate.NewValue,
                                      update.Resource.Fields.State.NewValue,
                                      update.Resource.Fields.State.OldValue,
                                      update.Resource.Fields.StateChangeDate.OldValue);

            workItem.AddWorkItemChange(workItemChange);

            UpdateTimeByStates(workItem);
        }

        public void UpdateTimeByStates(WorkItem workItem)
        {
            RemoveTimeByStateFromDataBase(workItem);

            workItem.ClearTimesByState();
            workItem.AddTimesByState(workItem.CalculateTotalTimeByState());
        }

        public void RemoveTimeByStateFromDataBase(WorkItem workItem)
        {
            _workItemRepository.RemoveAllTimeByState(workItem.TimeByStates.ToList());
        }
        #endregion
    }
}
using AzureDevopsStateTracker.DTOs;
using AzureDevopsStateTracker.DTOs.Create;
using AzureDevopsStateTracker.DTOs.Update;
using AzureDevopsStateTracker.Entities;
using AzureDevopsStateTracker.Interfaces;
using AzureDevopsStateTracker.Interfaces.Internals;
using System;

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
            var workItem = new WorkItem(
                create.Resource.Id,
                create.Resource.Fields.Title,
                create.Resource.Fields.AssignedTo,
                create.Resource.Fields.Type,
                create.Resource.Fields.CreatedBy);

            AddWorkItemChange(workItem, create);

            _workItemRepository.Add(workItem);
            _workItemRepository.SaveChanges();
        }

        public void Update(UpdatedWorkItemDTO update)
        {
            var workItem = _workItemRepository.GetByWorkItemId(update.Resource.WorkItemId);
            if (workItem is null)
                return;

            workItem.UpdateAssignedTo(update.Resource.Revision.Fields.AssignedTo);

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
        public WorkItemChange ToWorkItemChange(string workItemId, DateTime newDate, string newState, string oldState = null, DateTime? oldDate = null)
        {
            return new WorkItemChange(workItemId, newDate, newState, oldState, oldDate);
        }

        public void AddWorkItemChange(WorkItem workItem, CreateWorkItemDTO create)
        {
            var workItemChange = ToWorkItemChange(workItem.Id,
                                                  create.Resource.Fields.CreatedDate,
                                                  create.Resource.Fields.State);
            workItem.AddWorkItemChange(workItemChange);
        }

        public void AddWorkItemChange(WorkItem workItem, UpdatedWorkItemDTO update)
        {
            var workItemChange = ToWorkItemChange(workItem.Id,
                                      update.Resource.Fields.StateChangeDate.NewValue,
                                      update.Resource.Fields.State.NewValue,
                                      update.Resource.Fields.State.OldValue,
                                      update.Resource.Fields.StateChangeDate.OldValue);

            workItem.AddWorkItemChange(workItemChange);

            AddWorkItemStatusTime(workItem, update.Resource.Fields.State.OldValue, workItemChange.CalculateTotalTime());
        }


        public WorkItemStatusTime ToWorkItemStatusTime(string workItemId, string state, TimeSpan totalTime)
        {
            return new WorkItemStatusTime(workItemId, state, totalTime);
        }

        public void AddWorkItemStatusTime(WorkItem workItem, string state, TimeSpan totalTime)
        {
            var workItemStatusTime = ToWorkItemStatusTime(workItem.Id, state, totalTime);
            workItem.AddWorkItemStatusTime(workItemStatusTime);
        } 
        #endregion
    }
}
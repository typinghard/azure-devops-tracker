using AzureDevopsStateTracker.DTOs;
using AzureDevopsStateTracker.Entities;
using AzureDevopsStateTracker.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace AzureDevopsStateTracker.Adapters
{
    internal class WorkItemAdapter : IWorkItemAdapter
    {
        public WorkItemDTO ToWorkItemDTO(WorkItem workItem)
        {
            if (workItem == null) return null;

            return new WorkItemDTO()
            {
                Id = workItem.Id,
                CreatedAt = workItem.CreatedAt,
                AssignedTo = workItem.AssignedTo,
                CreatedBy = workItem.CreatedBy,
                CurrentStatus = workItem.CurrentStatus,
                TeamProject = workItem.TeamProject,
                AreaPath = workItem.AreaPath,
                IterationPath = workItem.IterationPath,
                Title = workItem.Title,
                Type = workItem.Type,
                Effort = workItem.Effort,
                StoryPoints = workItem.StoryPoints,
                OriginalEstimate = workItem.OriginalEstimate,
                WorkItemParentId = workItem.WorkItemParentId,
                Activity = workItem.Activity,
                Tags = workItem.Tags == null ? new List<string>() : workItem.Tags.Split(';').ToList(),
                WorkItemsChangesDTO = ToWorkItemsChangeDTO(workItem.WorkItemsChanges.OrderBy(x => x.CreatedAt).ToList()),
                TimesByStateDTO = ToTimeByStatesDTO(workItem.CalculateTotalTimeByState().ToList()),
            };
        }

        public List<WorkItemDTO> ToWorkItemsDTO(List<WorkItem> workItems)
        {
            var workItemsDTO = new List<WorkItemDTO>();

            if (workItems == null) return workItemsDTO;

            workItems.ForEach(
                        workItem =>
                        workItemsDTO.Add(ToWorkItemDTO(workItem)));

            return workItemsDTO
                     .ToList();
        }

        public WorkItemChangeDTO ToWorkItemChangeDTO(WorkItemChange workIteChange)
        {
            if (workIteChange == null) return null;

            return new WorkItemChangeDTO()
            {
                NewDate = workIteChange.NewDate,
                NewState = workIteChange.NewState,
                OldState = workIteChange.OldState,
                OldDate = workIteChange.OldDate,
                ChangedBy = workIteChange.ChangedBy
            };
        }

        public List<WorkItemChangeDTO> ToWorkItemsChangeDTO(List<WorkItemChange> workItemsChanges)
        {
            var workItemsChangeDTO = new List<WorkItemChangeDTO>();

            if (workItemsChanges == null) return workItemsChangeDTO;

            workItemsChanges.ForEach(
                        workItemsChange =>
                        workItemsChangeDTO.Add(ToWorkItemChangeDTO(workItemsChange)));

            return workItemsChangeDTO
                     .Where(w => w != null)
                     .ToList();
        }

        public TimeByStateDTO ToTimeByStateDTO(TimeByState workItemStatusTime)
        {
            if (workItemStatusTime == null) return null;

            return new TimeByStateDTO()
            {
                CreatedAt = workItemStatusTime.CreatedAt,
                State = workItemStatusTime.State,
                //TotalTime = workItemStatusTime.TotalTimeText,
                //TotalWorkedTime = workItemStatusTime.TotalWorkedTimeText
            };
        }

        public List<TimeByStateDTO> ToTimeByStatesDTO(List<TimeByState> workItemStatusTimes)
        {
            var workItemStatusTimeDTO = new List<TimeByStateDTO>();

            if (workItemStatusTimes == null) return workItemStatusTimeDTO;

            workItemStatusTimes.ForEach(
                        workItemStatusTime =>
                        workItemStatusTimeDTO.Add(ToTimeByStateDTO(workItemStatusTime)));

            return workItemStatusTimeDTO
                     .Where(w => w != null)
                     .ToList();
        }
    }
}
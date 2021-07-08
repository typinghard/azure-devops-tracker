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
                Title = workItem.Title,
                Type = workItem.Type,
                WorkItemsChangesDTO = ToWorkItemsChangeDTO(workItem.WorkItemsChanges.OrderBy(x => x.CreatedAt).ToList()),
                WorkItemsStatusTimeDTO = ToWorkItemsStatusTimeDTO(workItem.WorkItemsStatusTime.OrderBy(x => x.CreatedAt).ToList()),
                TotalTimeByState = workItem.CalculateTotalTimeByState()
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
                     .Where(w => w != null)
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
                OldDate = workIteChange.OldDate
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

        public WorkItemStatusTimeDTO ToWorkItemStatusTimeDTO(WorkItemStatusTime workItemStatusTime)
        {
            if (workItemStatusTime == null) return null;

            return new WorkItemStatusTimeDTO()
            {
                State = workItemStatusTime.State,
                TotalTime = workItemStatusTime.TotalTime.ToString()
            };
        }

        public List<WorkItemStatusTimeDTO> ToWorkItemsStatusTimeDTO(List<WorkItemStatusTime> workItemStatusTimes)
        {
            var workItemStatusTimeDTO = new List<WorkItemStatusTimeDTO>();

            if (workItemStatusTimes == null) return workItemStatusTimeDTO;

            workItemStatusTimes.ForEach(
                        workItemStatusTime =>
                        workItemStatusTimeDTO.Add(ToWorkItemStatusTimeDTO(workItemStatusTime)));

            return workItemStatusTimeDTO
                     .Where(w => w != null)
                     .ToList();
        }
    }
}
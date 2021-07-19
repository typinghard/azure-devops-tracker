using AzureDevopsStateTracker.DTOs;
using AzureDevopsStateTracker.Entities;
using System.Collections.Generic;

namespace AzureDevopsStateTracker.Interfaces
{
    public interface IWorkItemAdapter
    {
        WorkItemDTO ToWorkItemDTO(WorkItem workItem);
        List<WorkItemDTO> ToWorkItemsDTO(List<WorkItem> workItems);
    }
}
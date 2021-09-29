using AzureDevopsTracker.DTOs;
using AzureDevopsTracker.Entities;
using System.Collections.Generic;

namespace AzureDevopsTracker.Interfaces
{
    public interface IWorkItemAdapter
    {
        WorkItemDTO ToWorkItemDTO(WorkItem workItem);
        List<WorkItemDTO> ToWorkItemsDTO(List<WorkItem> workItems);
    }
}
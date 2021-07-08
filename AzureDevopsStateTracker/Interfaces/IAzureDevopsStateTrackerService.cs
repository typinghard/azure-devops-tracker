using AzureDevopsStateTracker.DTOs;
using AzureDevopsStateTracker.DTOs.Create;
using AzureDevopsStateTracker.DTOs.Update;

namespace AzureDevopsStateTracker.Interfaces
{
    public interface IAzureDevopsStateTrackerService
    {
        void Create(CreateWorkItemDTO createDto);
        void Update(UpdatedWorkItemDTO updateDto);
        WorkItemDTO GetByWorkItemId(string workItemId);
    }
}
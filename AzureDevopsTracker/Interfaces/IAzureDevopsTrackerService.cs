using AzureDevopsTracker.DTOs;
using AzureDevopsTracker.DTOs.Create;
using AzureDevopsTracker.DTOs.Delete;
using AzureDevopsTracker.DTOs.Restore;
using AzureDevopsTracker.DTOs.Update;
using System.Threading.Tasks;

namespace AzureDevopsTracker.Interfaces
{
    public interface IAzureDevopsTrackerService
    {
        Task Create(CreateWorkItemDTO createDto, bool addWorkItemChange = true);
        Task Update(UpdatedWorkItemDTO updateDto);
        Task Delete(DeleteWorkItemDTO deleteDto);
        Task Restore(RestoreWorkItemDTO restoreDto);
        Task<WorkItemDTO> GetByWorkItemId(string workItemId);
    }
}
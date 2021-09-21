using AzureDevopsTracker.DTOs;
using AzureDevopsTracker.DTOs.Create;
using AzureDevopsTracker.DTOs.Update;
using System.Threading.Tasks;

namespace AzureDevopsTracker.Interfaces
{
    public interface IAzureDevopsStateTrackerService
    {
        Task Create(CreateWorkItemDTO createDto, bool addWorkItemChange = true);
        Task Update(UpdatedWorkItemDTO updateDto);
        Task<WorkItemDTO> GetByWorkItemId(string workItemId);
    }
}
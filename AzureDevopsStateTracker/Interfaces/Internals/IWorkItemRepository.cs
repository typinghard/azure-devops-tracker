using AzureDevopsStateTracker.Entities;

namespace AzureDevopsStateTracker.Interfaces.Internals
{
    public interface IWorkItemRepository : IRepository<WorkItem>
    {
        WorkItem GetByWorkItemId(string workItemId);
    }
}
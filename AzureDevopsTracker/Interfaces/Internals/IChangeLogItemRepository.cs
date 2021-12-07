using AzureDevopsTracker.Entities;

namespace AzureDevopsTracker.Interfaces.Internals
{
    public interface IChangeLogItemRepository : IRepository<ChangeLogItem>
    {
        int CountItemsForRelease();
    }
}
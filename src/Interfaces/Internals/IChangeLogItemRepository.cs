using AzureDevopsTracker.Entities;
using System.Collections.Generic;

namespace AzureDevopsTracker.Interfaces.Internals
{
    public interface IChangeLogItemRepository : IRepository<ChangeLogItem>
    {
        int CountItemsForRelease();
        IEnumerable<ChangeLogItem> ListWaitingForRelease();
    }
}
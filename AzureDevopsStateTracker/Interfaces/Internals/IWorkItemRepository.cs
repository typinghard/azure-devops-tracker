using AzureDevopsStateTracker.Entities;
using System.Collections.Generic;

namespace AzureDevopsStateTracker.Interfaces.Internals
{
    public interface IWorkItemRepository : IRepository<WorkItem>
    {
        WorkItem GetByWorkItemId(string workItemId);
        IEnumerable<WorkItem> ListByWorkItemId(IEnumerable<string> workItemsId);
        IEnumerable<WorkItem> ListByIterationPath(string iterationPath);
        void RemoveAllTimeByState(List<TimeByState> timeByStates);
    }
}
using AzureDevopsStateTracker.Data.Context;
using AzureDevopsStateTracker.Entities;
using AzureDevopsStateTracker.Interfaces.Internals;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AzureDevopsStateTracker.Data
{
    internal class WorkItemRepository : Repository<WorkItem>, IWorkItemRepository
    {
        public WorkItemRepository(AzureDevopsStateTrackerContext context) : base(context) { }

        public WorkItem GetByWorkItemId(string workItemId)
        {
            return DbSet
                .Include(x => x.WorkItemsChanges)
                .Include(x => x.TimeByStates)
                .FirstOrDefault(x => x.Id == workItemId);
        }

        public IEnumerable<WorkItem> ListByWorkItemId(IEnumerable<string> workItemsId)
        {
            return DbSet
                .Include(x => x.WorkItemsChanges)
                .Include(x => x.TimeByStates)
                .Where(x => workItemsId.Contains(x.Id));
        }

        public IEnumerable<WorkItem> ListByIterationPath(string iterationPath)
        {
            return DbSet
                .Include(x => x.WorkItemsChanges)
                .Include(x => x.TimeByStates)
                .Where(x => x.IterationPath == iterationPath);
        }

        public void RemoveAllTimeByState(List<TimeByState> timeByStates)
        {
            Db.TimeByStates.RemoveRange(timeByStates);
        }
    }
}
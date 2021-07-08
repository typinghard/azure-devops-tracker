using AzureDevopsStateTracker.Data.Context;
using AzureDevopsStateTracker.Entities;
using AzureDevopsStateTracker.Interfaces.Internals;
using Microsoft.EntityFrameworkCore;
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
                .Include(x => x.WorkItemsStatusTime)
                .FirstOrDefault(x => x.Id == workItemId);
        }
    }
}
using AzureDevopsTracker.Data.Context;
using AzureDevopsTracker.Entities;
using AzureDevopsTracker.Interfaces.Internals;
using System.Linq;

namespace AzureDevopsTracker.Data
{
    internal class ChangeLogItemRepository : Repository<ChangeLogItem>, IChangeLogItemRepository
    {
        public ChangeLogItemRepository(AzureDevopsTrackerContext context) : base(context) { }

        public int CountItemsForRelease()
        {
            return DbSet.Count(x => string.IsNullOrEmpty(x.ChangeLogId));
        }
    }
}
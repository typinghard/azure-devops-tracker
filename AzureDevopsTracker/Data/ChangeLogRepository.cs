using AzureDevopsTracker.Data.Context;
using AzureDevopsTracker.Entities;
using AzureDevopsTracker.Interfaces.Internals;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AzureDevopsTracker.Data
{
    internal class ChangeLogRepository : Repository<ChangeLog>, IChangeLogRepository
    {
        public ChangeLogRepository(AzureDevopsTrackerContext context) : base(context) { }

        public int CountChangeLogsCreatedToday()
        {
            return DbSet.Count(x => x.CreatedAt.Date == DateTime.Now.Date);
        }
    }
}
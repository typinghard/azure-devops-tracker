using AzureDevopsTracker.Entities;
using System.Collections.Generic;

namespace AzureDevopsTracker.Interfaces.Internals
{
    public interface IChangeLogRepository : IRepository<ChangeLog>
    {
        int CountChangeLogsCreatedToday();
    }
}
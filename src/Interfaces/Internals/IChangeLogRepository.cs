using AzureDevopsTracker.Entities;

namespace AzureDevopsTracker.Interfaces.Internals
{
    public interface IChangeLogRepository : IRepository<ChangeLog>
    {
        int CountChangeLogsCreatedToday();
    }
}
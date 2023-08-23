using AzureDevopsTracker.Entities;

namespace AzureDevopsTracker.Interfaces
{
    public interface IChangeLogService
    {
        int CountItemsForRelease();
        ChangeLog Release();
        string SendToMessengers(ChangeLog changeLog);
    }
}
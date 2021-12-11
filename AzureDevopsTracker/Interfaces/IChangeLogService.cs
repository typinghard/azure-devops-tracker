using AzureDevopsTracker.Entities;
using System.Threading.Tasks;

namespace AzureDevopsTracker.Interfaces
{
    public interface IChangeLogService
    {
        int CountItemsForRelease();
        ChangeLog Release();
        string SendToMessengers(ChangeLog changeLog);
    }
}
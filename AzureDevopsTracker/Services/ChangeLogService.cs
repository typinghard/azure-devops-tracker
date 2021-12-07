using AzureDevopsTracker.Interfaces;
using AzureDevopsTracker.Interfaces.Internals;

namespace AzureDevopsTracker.Services
{
    public class ChangeLogService : IChangeLogService
    {
        private readonly IChangeLogItemRepository _changeLogItemRepository;

        public ChangeLogService(IChangeLogItemRepository changeLogItemRepository)
        {
            _changeLogItemRepository = changeLogItemRepository;
        }

        public int CountItemsForRelease()
        {
            return _changeLogItemRepository.CountItemsForRelease();
        }
    }
}
using AzureDevopsTracker.Entities;
using AzureDevopsTracker.Interfaces;
using AzureDevopsTracker.Interfaces.Internals;
using System.Linq;
using System.Threading.Tasks;

namespace AzureDevopsTracker.Services
{
    public class ChangeLogService : IChangeLogService
    {
        private readonly IChangeLogItemRepository _changeLogItemRepository;
        private readonly IChangeLogRepository _changeLogRepository;

        public ChangeLogService(
            IChangeLogItemRepository changeLogItemRepository,
            IChangeLogRepository changeLogRepository)
        {
            _changeLogItemRepository = changeLogItemRepository;
            _changeLogRepository = changeLogRepository;
        }

        public int CountItemsForRelease()
        {
            return _changeLogItemRepository.CountItemsForRelease();
        }

        public ChangeLog Release()
        {
            var changeLogItems = _changeLogItemRepository.ListWaitingForRelease();
            if (!changeLogItems.Any()) return null;

            var changeLogsQuantity = _changeLogRepository.CountChangeLogsCreatedToday();

            var changeLog = new ChangeLog(changeLogsQuantity + 1);

            changeLog.AddChangeLogItems(changeLogItems);

            _changeLogRepository.Add(changeLog).Wait();
            _changeLogRepository.SaveChangesAsync().Wait();

            return changeLog;
        }

        public string SendToMessengers(ChangeLog changeLog)
        {
            //Chamar o Facade pra enviar pro Destination

            return $"The ChangeLog { changeLog.Number } was released.";
        }
    }
}
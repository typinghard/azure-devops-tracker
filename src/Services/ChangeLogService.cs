using AzureDevopsTracker.Entities;
using AzureDevopsTracker.Integrations;
using AzureDevopsTracker.Interfaces;
using AzureDevopsTracker.Interfaces.Internals;
using AzureDevopsTracker.Statics;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace AzureDevopsTracker.Services
{
    public class ChangeLogService : IChangeLogService
    {
        private readonly IChangeLogItemRepository _changeLogItemRepository;
        private readonly IChangeLogRepository _changeLogRepository;
        private readonly IConfiguration _configuration;
        private readonly MessageIntegration _messageIntegration;

        public ChangeLogService(
            IChangeLogItemRepository changeLogItemRepository,
            IChangeLogRepository changeLogRepository,
            IConfiguration configuration,
            MessageIntegration messageIntegration)
        {
            _changeLogItemRepository = changeLogItemRepository;
            _changeLogRepository = changeLogRepository;
            _configuration = configuration;
            _messageIntegration = messageIntegration;
        }

        public int CountItemsForRelease()
        {
            return _changeLogItemRepository.CountItemsForRelease();
        }

        public ChangeLog Release()
        {
            var changeLogItems = _changeLogItemRepository.ListWaitingForRelease();
            if (!changeLogItems.Any()) return null;

            var changeLog = CreateChangeLog();
            changeLog.AddChangeLogItems(changeLogItems);

            _changeLogRepository.Add(changeLog).Wait();
            _changeLogRepository.SaveChangesAsync().Wait();

            return changeLog;
        }

        public string SendToMessengers(ChangeLog changeLog)
        {
            _messageIntegration.Send(changeLog);

            return $"The ChangeLog {changeLog.Number} was released.";
        }

        private ChangeLog CreateChangeLog()
        {
            if (string.IsNullOrEmpty(_configuration[ConfigurationStatics.ADT_CHANGELOG_VERSION]))
            {
                var changeLogsQuantity = _changeLogRepository.CountChangeLogsCreatedToday();
                return new ChangeLog(changeLogsQuantity + 1);
            }
            return new ChangeLog(_configuration[ConfigurationStatics.ADT_CHANGELOG_VERSION]);
        }
    }
}
using AzureDevopsTracker.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AzureDevopsTracker.Integrations
{
    internal class MessageFacade
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public MessageFacade(
            IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void Send(ChangeLog changeLog)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var messageIntegration = scope.ServiceProvider.GetService<MessageIntegration>();
            if (messageIntegration == null) throw new Exception("Configure the MessageConfig in Startup to send changelog messages");

            messageIntegration.Send(changeLog);
        }
    }
}

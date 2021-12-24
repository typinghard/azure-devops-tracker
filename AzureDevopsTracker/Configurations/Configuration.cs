using AzureDevopsTracker.Adapters;
using AzureDevopsTracker.Data;
using AzureDevopsTracker.Data.Context;
using AzureDevopsTracker.Integrations;
using AzureDevopsTracker.Interfaces;
using AzureDevopsTracker.Interfaces.Internals;
using AzureDevopsTracker.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AzureDevopsTracker.Configurations
{
    public static class Configuration
    {
        public static IServiceCollection AddAzureDevopsTracker(this IServiceCollection services, DataBaseConfig configurations, MessageConfig messageConfig = null)
        {
            services.AddDbContext<AzureDevopsTrackerContext>(options =>
                    options.UseSqlServer(DataBaseConfig.ConnectionsString));

            services.AddMessageIntegrations();

            services.AddScoped<AzureDevopsTrackerContext>();
            services.AddScoped<IWorkItemAdapter, WorkItemAdapter>();
            services.AddScoped<IWorkItemRepository, WorkItemRepository>();

            services.AddScoped<IAzureDevopsTrackerService, AzureDevopsTrackerService>();

            services.AddScoped<IChangeLogService, ChangeLogService>();
            services.AddScoped<IChangeLogItemRepository, ChangeLogItemRepository>();
            services.AddScoped<IChangeLogRepository, ChangeLogRepository>();

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            return services;
        }

        private static IServiceCollection AddMessageIntegrations(this IServiceCollection services)
        {
            if (MessageConfig.Messenger == null) return services;

            if(MessageConfig.Messenger == EMessengers.DISCORD)
                services.AddScoped<MessageIntegration, DiscordIntegration>();
            else
                services.AddScoped<MessageIntegration, MicrosoftTeamsIntegration>();

            return services;
        }

        public static IApplicationBuilder UseAzureDevopsTracker(this IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<AzureDevopsTrackerContext>();
            context.Database.Migrate();

            return app;
        }
    }
}
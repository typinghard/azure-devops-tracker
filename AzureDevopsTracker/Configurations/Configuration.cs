using AzureDevopsTracker.Adapters;
using AzureDevopsTracker.Data;
using AzureDevopsTracker.Data.Context;
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
        public static IServiceCollection AddAzureDevopsStateTracker(this IServiceCollection services, DataBaseConfig configurations)
        {
            services.AddDbContext<AzureDevopsStateTrackerContext>(options =>
                    options.UseSqlServer(DataBaseConfig.ConnectionsString));

            services.AddScoped<AzureDevopsStateTrackerContext>();
            services.AddScoped<IWorkItemAdapter, WorkItemAdapter>();
            services.AddScoped<IWorkItemRepository, WorkItemRepository>();
            services.AddScoped<IAzureDevopsStateTrackerService, AzureDevopsStateTrackerService>();

            return services;
        }

        public static IApplicationBuilder UseAzureDevopsStateTracker(this IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetService<AzureDevopsStateTrackerContext>();
            context.Database.Migrate();

            return app;
        }
    }
}
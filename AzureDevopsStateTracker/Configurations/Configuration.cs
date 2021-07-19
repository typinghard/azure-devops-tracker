using AzureDevopsStateTracker.Adapters;
using AzureDevopsStateTracker.Data;
using AzureDevopsStateTracker.Data.Context;
using AzureDevopsStateTracker.Interfaces;
using AzureDevopsStateTracker.Interfaces.Internals;
using AzureDevopsStateTracker.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace AzureDevopsStateTracker.Configurations
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
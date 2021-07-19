using AzureDevopsStateTracker.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace AzureDevopsStateTracker.Data.Context
{
    public class AzureDevopsStateTrackerContext : DbContext, IDisposable
    {
        public AzureDevopsStateTrackerContext(DbContextOptions options) : base(options)
        { }

        public DbSet<WorkItem> WorkItems { get; set; }
        public DbSet<WorkItemChange> WorkItemsChange { get; set; }
        public DbSet<TimeByState> TimeByStates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(200)");

            modelBuilder.HasDefaultSchema(DataBaseConfig.SchemaName);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(DataBaseConfig.ConnectionsString);
        }
    }
}
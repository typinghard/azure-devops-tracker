using AzureDevopsTracker.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AzureDevopsTracker.Data.Mapping
{
    public class WorkItemCustomFieldMapping : IEntityTypeConfiguration<WorkItemCustomField>
    {
        public void Configure(EntityTypeBuilder<WorkItemCustomField> builder)
        {
            builder.HasKey(k => new { k.WorkItemId, k.Key });
        }
    }
}
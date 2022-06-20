using AzureDevopsTracker.Extensions;
using System;

namespace AzureDevopsTracker.Entities
{
    public class WorkItemCustomField
    {
        public string WorkItemId { get; private set; }
        public string Key { get; private set; }
        public string Value { get; private set; }

        public WorkItem WorkItem { get; private set; }

        public WorkItemCustomField(string workItemId, string key, string value)
        {
            WorkItemId = workItemId;
            Key = key;
            Value = value;
        }

        public void Update(string value)
        {
            Value = value;
        }
    }
}
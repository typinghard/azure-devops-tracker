﻿using AzureDevopsTracker.Extensions;
using System;

namespace AzureDevopsTracker.Entities
{
    public class WorkItemCustomField
    {
        public string WorkItemId { get; private set; }
        public string Key { get; private set; }
        public string Value { get; private set; }

        public WorkItem WorkItem { get; private set; }

        public WorkItemCustomField(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public void LinkWorkItem(string workItemId)
        {
            if (workItemId.IsNullOrEmpty())
                throw new ArgumentException("WorkItemId is required");

            WorkItemId = workItemId;
        }

        public void Update(string value)
        {
            Value = value;
        }
    }
}
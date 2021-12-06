using System;
using System.Collections.Generic;
using System.Linq;

namespace AzureDevopsTracker.Entities
{
    public class ChangeLog : Entity
    {
        public string Response { get; private set; }
        public int Revision { get; set; }

        private readonly List<ChangeLogItem> _changeLogItems = new List<ChangeLogItem>();
        public IReadOnlyCollection<ChangeLogItem> ChangeLogItems => _changeLogItems;
        private ChangeLog() { }

        public ChangeLog(int newRevision)
        {
            Revision = newRevision;
        }

        public void ReleaseItems()
        {
            _changeLogItems.ForEach(c => c.Release(Id));
        }

        public void SetResponse(string response)
        {
            Response = response;
        }

        public void ClearResponse()
        {
            Response = string.Empty;
        }

        public void AddChangeLogItem(ChangeLogItem changeLogItem)
        {
            if (changeLogItem == null)
                throw new Exception("ChangeLogItem is required");

            if (CheckChangeLogItem(changeLogItem))
                return;

            _changeLogItems.Add(changeLogItem);
        }

        public void AddChangeLogItems(IEnumerable<ChangeLogItem> changeLogItems)
        {
            if (changeLogItems == null)
                throw new Exception("WorkItemType is required");

            foreach (var changeLogItem in changeLogItems)
                AddChangeLogItem(changeLogItem);
        }

        private bool CheckChangeLogItem(ChangeLogItem changeLogItem)
        {
            return _changeLogItems.Any(x => x.WorkItemId == changeLogItem.WorkItemId);
        }
    }
}
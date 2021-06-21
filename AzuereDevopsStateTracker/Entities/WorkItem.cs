namespace AzuereDevopsStateTracker.Entities
{
    public class WorkItem
    {
        public int Id { get; private set; }
        public string AssignedTo { get; private set; }
        public string Type { get; private set; }

        public WorkItem(int id, string assignedTo, string type)
        {
            Id = id;
            AssignedTo = assignedTo;
            Type = type;
        }
    }
}
using System;

namespace AzureDevopsTracker.Entities
{
    public abstract class Entity
    {
        public string Id { get; protected set; }
        public DateTime CreatedAt { get; private set; }
        public Entity(string id)
        {
            Id = id;
            CreatedAt = DateTime.UtcNow;
        }

        public Entity()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.UtcNow;
        }
    }
}
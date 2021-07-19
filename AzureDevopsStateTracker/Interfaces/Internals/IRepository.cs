using AzureDevopsStateTracker.Entities;
using System;
using System.Collections.Generic;

namespace AzureDevopsStateTracker.Interfaces.Internals
{
    public interface IRepository<TEntity> : IDisposable where TEntity : Entity
    {
        void Add(TEntity entity);
        void Add(IEnumerable<TEntity> entities);
        TEntity GetById(string id);
        bool Exist(string id);
        void Update(TEntity entity);
        void Update(IEnumerable<TEntity> entities);
        void Delete(TEntity entity);
        void SaveChanges();
    }
}
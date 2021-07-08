using AzureDevopsStateTracker.Data.Context;
using AzureDevopsStateTracker.Entities;
using AzureDevopsStateTracker.Interfaces.Internals;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AzureDevopsStateTracker.Data
{
    internal abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly AzureDevopsStateTrackerContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(AzureDevopsStateTrackerContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public virtual void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Add(IEnumerable<TEntity> entities)
        {
            DbSet.AddRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            DbSet.Update(entity);
        }

        public virtual void Update(IEnumerable<TEntity> entities)
        {
            DbSet.UpdateRange(entities);
        }

        public virtual void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }

        public virtual TEntity GetById(string id)
        {
            return DbSet
                .Where(x => x.Id == id)
                .FirstOrDefault();
        }

        public bool Exist(string id)
        {
            return DbSet
                    .Any(x => x.Id == id);
        }
        
        public void SaveChanges()
        {
            Db.SaveChanges();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
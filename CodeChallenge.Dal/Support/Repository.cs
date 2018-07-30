using C1B.GestionInterna.Entities;
using CodeChallenge.Dal.Contract.Support;
using CodeChallenge.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenge.Dal.Support
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly Context Context;

        private readonly DbSet<T> dbentitySet;

        public Repository(Context context)
        {
            this.Context = context;
            this.dbentitySet = context.Set<T>();
        }

        public virtual IQueryable<T> GetAll()
        {
            return this.dbentitySet;
        }

        public virtual T GetByID(int id)
        {
            return this.dbentitySet.Find(id);

        }

        public virtual Task<T> GetByIdAsync(object id)
        {
            return this.dbentitySet.FindAsync(id);
        }

        public virtual IQueryable<T> Queryable()
        {
            return (IQueryable<T>)this.dbentitySet;
        }

        public virtual IQueryable<T> QueryableAsNoTracking()
        {
            return (IQueryable<T>)this.dbentitySet.AsNoTracking();
        }

        public virtual void Insert(T obj)
        {
            this.dbentitySet.Add(obj);
        }

        public virtual void Delete(int id)
        {
            T entityToDelete = this.dbentitySet.Find(id);

            if (entityToDelete == null)
            {
                var name = typeof(T).Name;

                throw new Exception(string.Format("Entity '{0}' whit ID {1} does not exists.", name, id.ToString()));
            }

             this.Delete(entityToDelete);
        }

        public virtual T Delete(T entityToDelete)
        {
            if (this.Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                this.dbentitySet.Attach(entityToDelete);
            }

            var deletedEntity = this.dbentitySet.Remove(entityToDelete).Entity;
            return deletedEntity;
        }

        public virtual void Update(T obj)
        {
            var entityUpdated = this.dbentitySet.Attach(obj).Entity;
            Context.Entry(entityUpdated).State = EntityState.Modified;
        }
    }
}

using C1B.GestionInterna.Entities;
using CodeChallenge.Dal.Contract.Support;
using CodeChallenge.Entities.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace CodeChallenge.Dal.Support
{
    public class UnitOfWork :IUnitOfWork
    {
        private bool disposed = false;
        private Hashtable repositories;

        public Context Context { get; set; }
        public UnitOfWork(Context context)
        {
            this.Context = context;
        }

        public UnitOfWork()
        {

        }

        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.Context.Dispose();
                }
            }

            this.disposed = true;
        }

        public IRepository<TEntity> Repository<TEntity>() where TEntity : IBaseEntity
        {
            if (this.repositories == null)
            {
                this.repositories = new Hashtable();
            }

            var type = typeof(TEntity).Name;

            if (!this.repositories.ContainsKey(type))
            {
                Type repositoryType = typeof(Repository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), Context);
                this.repositories.Add(type, repositoryInstance);
            }



            return (IRepository<TEntity>)this.repositories[type];
        }

        public void SaveChanges()
        {
            try
            {
                this.Context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

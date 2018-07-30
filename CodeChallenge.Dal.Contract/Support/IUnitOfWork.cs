using C1B.GestionInterna.Entities;
using CodeChallenge.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeChallenge.Dal.Contract.Support
{
    public interface IUnitOfWork : IDisposable
    {
        Context Context { get; set; }

        IRepository<TEntity> Repository<TEntity>() where TEntity : IBaseEntity;

        void SaveChanges();
    }
}

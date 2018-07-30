using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenge.Dal.Contract.Support
{
    public interface IRepository<T>
    {
        IQueryable<T> GetAll();

        T GetByID(int id);

        void Insert(T obj);

        void Delete(int iD);

        void Update(T obj);

        IQueryable<T> Queryable();

        IQueryable<T> QueryableAsNoTracking();

        Task<T> GetByIdAsync(object id);

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Data.Repositories
{
    public interface IRepositoryBase<T>
    {
        Task<T> Get(int id, CancellationToken cancellationToken = default);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        IQueryable<T> Where(Expression<Func<T, bool>> predicatebool, bool readOnly = false);
    }
}

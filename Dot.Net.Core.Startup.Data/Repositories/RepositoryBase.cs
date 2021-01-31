using Dot.Net.Core.Startup.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dot.Net.Core.Startup.Data.Repositories
{
    class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private RepositoryContext _repositoryContext { get; set; }

        public RepositoryBase(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }

        public async Task<T> Get(int id, CancellationToken cancellationToken = default)
        {
            return await _repositoryContext.Set<T>().FindAsync(id);
        }
        public void Create(T entity)
        {
            _repositoryContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            _repositoryContext.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
            _repositoryContext.Set<T>().Remove(entity);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> predicate, bool readOnly = false)
        {
            var set = _repositoryContext.Set<T>();
            if (readOnly)
                set.AsNoTracking();

            return set.Where(predicate);
        }

    }
}

using Dot.Net.Core.Startup.Data.Context;
using Dot.Net.Core.Startup.Data.Entities;
using Dot.Net.Core.Startup.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Dot.Net.Core.Startup.Data.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly RepositoryContext _repositoryContext;
        private readonly IDbContextTransaction _transaction;
        private readonly Dictionary<Type, object> _repositories;
       
        public UnitOfWork(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _transaction = (_repositoryContext as DbContext).Database.BeginTransaction();
            _repositories = new Dictionary<Type, object>();
        }
        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                await _transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await _transaction.RollbackAsync(cancellationToken);
            }
        }

        public IRepositoryBase<TSet> GetRepository<TSet>() where TSet : class
        {
            if (_repositories.ContainsKey(typeof(TSet)))
            {
                return _repositories[typeof(TSet)] as IRepositoryBase<TSet>;
            }

            var repository = new RepositoryBase<TSet>(_repositoryContext);
            _repositories.Add(typeof(TSet), repository);
            return repository;
        }

        public Task RollBackAsync(CancellationToken cancellationToken = default)
        {
            return _transaction.RollbackAsync(cancellationToken);
        }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = _repositoryContext.ChangeTracker.Entries<BaseEntity>().ToArray();
            foreach (var entry in entries)
            {
                var entity = entry.Entity;
                if (entry.State == EntityState.Modified)
                {
                    entity.UpdatedAt = DateTime.UtcNow;
                }
                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = DateTime.UtcNow;
                    entity.UpdatedAt = DateTime.UtcNow;
                }
            }
            return _repositoryContext.SaveChangesAsync(cancellationToken);
        }
    }
}

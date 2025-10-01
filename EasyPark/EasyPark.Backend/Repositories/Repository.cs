using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EasyPark.Backend.Data;
using Microsoft.EntityFrameworkCore;

namespace EasyPark.Backend.Repositories
{
    public sealed class Repository<T> : IRepository<T> where T : class
    {
        private readonly DataContext _ctx;
        private readonly DbSet<T> _set;

        public Repository(DataContext ctx)
        {
            _ctx = ctx;
            _set = ctx.Set<T>();
        }

        public Task<T?> GetByIdAsync(object id) => _set.FindAsync(id).AsTask();

        public Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
            => _set.FirstOrDefaultAsync(predicate);

        public async Task<IReadOnlyList<T>> ListAsync(Expression<Func<T, bool>>? predicate = null)
            => predicate is null ? await _set.ToListAsync() : await _set.Where(predicate).ToListAsync();

        public Task AddAsync(T entity) => _set.AddAsync(entity).AsTask();

        public void Update(T entity) => _set.Update(entity);

        public void Remove(T entity) => _set.Remove(entity);

        public Task<int> SaveChangesAsync() => _ctx.SaveChangesAsync();
    }
}

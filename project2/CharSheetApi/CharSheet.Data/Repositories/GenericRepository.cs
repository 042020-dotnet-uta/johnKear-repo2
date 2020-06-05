using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CharSheet.Domain.Interfaces;

namespace CharSheet.Data.Repositories
{
    public abstract class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal CharSheetContext _context { get; set; }
        internal DbSet<TEntity> dbSet { get; set; }

        protected GenericRepository(CharSheetContext context)
        {
            this._context = context;
            this.dbSet = _context.Set<TEntity>();
        }

        public async virtual Task<IEnumerable<TEntity>> All()
        {
            return await dbSet.ToListAsync();
        }

        public async virtual Task<TEntity> Insert(TEntity entity)
        {
            var _entity = dbSet.Add(entity).Entity;
            return await Task.FromResult(_entity);
        }

        public async virtual Task<TEntity> Update(TEntity entity)
        {
            TEntity _entity = dbSet.Update(entity).Entity;
            return await Task.FromResult(_entity);
        }

        public async virtual Task<TEntity> Find(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public async virtual Task<IEnumerable<TEntity>> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public async virtual Task Remove(TEntity entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            await Task.FromResult(dbSet.Remove(entity));
        }

        public async virtual Task Remove(object id)
        {
            TEntity entity = await dbSet.FindAsync(id);
            await Remove(entity);
        }
    }
}
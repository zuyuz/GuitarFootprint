using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuitarFootprint.Data.Abstraction.Interfaces;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;
using Unit = LanguageExt.Unit;

namespace GuitarFootprint.Data.PostgreSQL.Repositories
{
    public abstract class Repository<TKey, TContext, TEntity> :
        IRepository<TKey, TEntity> where TEntity : class
        where TContext : DbContext
    {
        public TContext Context { get; set; }

        public virtual IQueryable<TEntity> GetAll()
        {
            IQueryable<TEntity> query = Context.Set<TEntity>();
            return query;
        }
        protected Repository(TContext context)
        {
            Context = context;
        }

        public virtual TryAsync<TEntity> GetByIdAsync(TKey id)
        {
            return TryAsync(async () =>
            {
                var entity = await Context.Set<TEntity>().FindAsync(id);
                return entity;
            });
        }

        public virtual TryAsync<TEntity> CreateAsync(TEntity entity)
        {
            return TryAsync(async () =>
            {
                await Context.Set<TEntity>().AddAsync(entity);
                return entity;
            });
        }

        public virtual TryAsync<List<TEntity>> CreateAsync(List<TEntity> entity)
        {
            return TryAsync(async () =>
            {
                await Context.Set<TEntity>().AddRangeAsync(entity);
                return entity;
            });
        }

        public virtual TryAsync<TEntity> DeleteAsync(TEntity entity)
        {
            return TryAsync(async () =>
            {
                Context.Set<TEntity>().Remove(entity);
                return entity;
            });
        }

        public virtual TryAsync<TEntity> UpdateAsync(TEntity entity)
        {
            return TryAsync(async () =>
            {
                Context.Entry(entity).State = EntityState.Modified;
                return entity;
            });
        }
    }
}

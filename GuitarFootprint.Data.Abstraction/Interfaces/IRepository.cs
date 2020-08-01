using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LanguageExt;
using LanguageExt.Common;

namespace GuitarFootprint.Data.Abstraction.Interfaces
{
    public interface IRepository<in TKey, TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> GetAll();
        TryAsync<TEntity> GetByIdAsync(TKey id);
        TryAsync<TEntity> CreateAsync(TEntity entity);
        TryAsync<List<TEntity>> CreateAsync(List<TEntity> entity);
        TryAsync<TEntity> DeleteAsync(TEntity entity);
        TryAsync<TEntity> UpdateAsync(TEntity entity);
    }
}

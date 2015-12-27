using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.FrameworkCore.Domain.Repositories.Interface
{
    public interface IReadOnlyRepositoryBase<TEntity, TKey>
     where TEntity : class
    {
        TEntity Load(TKey id);

        IQueryable<TEntity> Query();

        TEntity GetById(TKey id);

      
    }
}

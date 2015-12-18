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
        IQueryable<TEntity> Query();

        TEntity GetById(TKey id);

        /// <summary>
        /// 清除对象一级缓存(Session.Flush()不会同步到数据库)
        /// </summary>
        /// <param name="entity"></param>
        void Evict(TEntity entity);

        /// <summary>
        /// 清除对象一级缓存(Session.Flush()不会同步到数据库)
        /// </summary>
        /// <param name="list"></param>
        void Evict(IEnumerable<TEntity> list);
    }
}

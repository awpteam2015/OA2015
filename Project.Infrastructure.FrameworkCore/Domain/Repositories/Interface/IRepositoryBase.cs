using System.Collections.Generic;
using System.Linq;

namespace Project.Infrastructure.FrameworkCore.Domain.Repositories.Interface
{
    internal interface IRepositoryBase<TEntity, TKey>   where TEntity : class
    {

        TKey Save(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void SaveOrUpdate(TEntity entity);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
         void Merge(TEntity entity);

         /// <summary>
         /// 
         /// </summary>
         /// <param name="list"></param>
         void Merge(IEnumerable<TEntity> list);


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

using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;



namespace SyncSoft.ROM.Domain.Repository
{
    public interface IRepositoryBaseOracle<TEntity, TKey>
     where TEntity : class 
    {
        IQueryable<TEntity> Query();
        TEntity GetById(TKey id);
        TKey Save(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void SaveOrUpdate(TEntity entity);
      

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

using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using Project.Infrastructure.FrameworkCore.DataNhibernate;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;
using Project.Infrastructure.FrameworkCore.Domain.Repositories.Interface;

namespace Project.Infrastructure.FrameworkCore.Domain.Repositories
{
    /// <summary>
    /// 数据持久基类
    /// </summary>
    public class RepositoryBase<TEntity, TKey> : ReadOnlyRepositoryBase<TEntity, TKey>, IRepositoryBase<TEntity, TKey>
        where TEntity : class
    {

        public TKey Save(TEntity entity)
        {
            ISession session = SessionFactoryManager.GetCurrentSession();
            using (var tx = NhTransactionHelper.BeginTransaction())
            {
                try
                {
                    var key = (TKey)session.Save(entity);
                    tx.Commit();
                    return key;
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }


        public TKey Save2(TEntity entity, ISession session)
        {
            var key = (TKey)session.Save(entity);
            session.Flush();
            return key;
        }

        public void Update(TEntity entity)
        {
            ISession session = SessionFactoryManager.GetCurrentSession();
            using (var tx = NhTransactionHelper.BeginTransaction())
            {
                try
                {
                    session.Update(entity);
                    tx.Commit();
                }
                catch (Exception e)
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

        public void Delete(TEntity entity)
        {
            ISession session = SessionFactoryManager.GetCurrentSession();
            using (var tx = NhTransactionHelper.BeginTransaction())
            {
                try
                {
                    if (entity is ISoftDelete)
                    {
                        (entity as ISoftDelete).IsDeleted = true;
                        Update(entity);
                    }
                    else
                    {
                        session.Delete(entity);
                    }

                    tx.Commit();
                }
                catch (Exception e)
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

        public void SaveOrUpdate(TEntity entity)
        {
            ISession session = SessionFactoryManager.GetCurrentSession();
            using (var tx = NhTransactionHelper.BeginTransaction())
            {
                try
                {
                    session.SaveOrUpdate(entity);
                    tx.Commit();
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Merge(TEntity entity)
        {
            ISession session = SessionFactoryManager.GetCurrentSession();
            using (var tx = NhTransactionHelper.BeginTransaction())
            {
                try
                {
                    session.Merge(entity);
                    tx.Commit();
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

        public virtual void Merge(IEnumerable<TEntity> list)
        {
            ISession session = SessionFactoryManager.GetCurrentSession();
            using (var tx = NhTransactionHelper.BeginTransaction())
            {
                try
                {
                    foreach (var p in list)
                    {
                        session.Merge(p);
                    }
                    tx.Commit();
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// 清除对象一级缓存(Session.Flush()不会同步到数据库)
        /// </summary>
        /// <param name="entity"></param>
        public void Evict(TEntity entity)
        {
            SessionFactoryManager.GetCurrentSession().Evict(entity);
        }

        /// <summary>
        /// 清除对象一级缓存(Session.Flush()不会同步到数据库)
        /// </summary>
        /// <param name="list"></param>
        public void Evict(IEnumerable<TEntity> list)
        {
            foreach (var p in list)
            {
                SessionFactoryManager.GetCurrentSession().Evict(p);
            }
        }     
    }
}

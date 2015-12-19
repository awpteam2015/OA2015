using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using Project.Infrastructure.FrameworkCore.DataNhibernate;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;
using Project.Infrastructure.FrameworkCore.Domain.Repositories.Interface;
using SyncSoft.ROM.Infrastructure.DataNhibernate;

namespace Project.Infrastructure.FrameworkCore.Domain.Repositories
{
    /// <summary>
    /// 数据持久基类
    /// </summary>
    public class RepositoryBase<TEntity, TKey> : ReadOnlyRepositoryBase<TEntity, TKey>, IRepositoryBase<TEntity, TKey>
        where TEntity : class
    {
        public TEntity Load(TKey id)
        {
            ISession session = SessionFactoryManager.GetCurrentSession();
            return session.Load<TEntity>(id);
        }

        public TEntity Merge(TEntity entity)
        {
            ISession session = SessionFactoryManager.GetCurrentSession();
            return session.Merge(entity);
        }


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
                catch(Exception e)
                {
                    tx.Rollback();
                    throw; 
                }
            }
        }

        public void Delete(TEntity entity)
        {
            ISession session = SessionFactoryManager.GetCurrentSession();
            if (entity is ISoftDelete)
            {
                (entity as ISoftDelete).IsDeleted = true;
                Update(entity);
            }
            else
            {
                session.Delete(entity);
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



    }
}

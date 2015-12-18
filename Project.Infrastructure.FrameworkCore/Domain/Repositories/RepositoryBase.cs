using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Linq;
using Project.Infrastructure.FrameworkCore.DataNhibernate;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;
using SyncSoft.ROM.Domain.Repository;
using SyncSoft.ROM.Infrastructure.DataNhibernate;

namespace Project.Infrastructure.FrameworkCore.FrameworkCore.Repository
{
    /// <summary>
    /// 数据持久基类
    /// </summary>
    public class RepositoryBase<TEntity, TKey> : IRepositoryBaseOracle<TEntity, TKey>
        where TEntity : class
    {


        public TEntity GetById(TKey id)
        {

            ISession session = SessionFactoryManager.GetCurrentSession();
            return session.Get<TEntity>(id);
        }


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

        public IQueryable<TEntity> Query()
        {
            ISession session = SessionFactoryManager.GetCurrentSession();

            return session.Query<TEntity>();
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
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

        public void Delete(TEntity entity)
        {
            //ISession session = SessionFactoryManager.GetCurrentSession();
            //using (var tx = NhTransactionHelper.BeginTransaction())
            //{
            //    try
            //    {
            //        session.Delete(entity);
            //        tx.Commit();
            //    }
            //    catch
            //    {
            //        tx.Rollback();
            //        throw;
            //    }
            //}


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

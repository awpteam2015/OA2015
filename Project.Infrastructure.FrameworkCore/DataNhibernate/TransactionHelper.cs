using System;
using System.Data;
using NHibernate;
using Project.Infrastructure.FrameworkCore.DataNhibernate;

namespace SyncSoft.ROM.Infrastructure.DataNhibernate
{
    ///// <summary>
    ///// 嵌套事物
    ///// </summary>
    //public static class NestedTransactions
    //{
    //    /// <summary>
    //    /// 嵌套事物
    //    /// </summary>
    //    /// <param name="level"></param>
    //    /// <param name="transactional"></param>
    //    public static void Transaction(IsolationLevel level, Action transactional)
    //    {
    //        var sessionFactory = ServerConfig.KernelOracle.Get<ISessionFactory>();
    //        //如果已经在一个事物中，就不用在开启一个事物
    //        if (sessionFactory.GetCurrentSession().Transaction.IsActive)
    //        {
    //            transactional();
    //        }
    //        else
    //        {
    //            using (var tx = sessionFactory.GetCurrentSession().BeginTransaction(level))
    //            {
    //                try
    //                {
    //                    transactional();
    //                    tx.Commit();
    //                }
    //                catch
    //                {
    //                    tx.Rollback();
    //                    throw;
    //                }
    //            }
    //        }
    //    }
    //    /// <summary>
    //    /// 嵌套事物
    //    /// </summary>
    //    /// <param name="transactional">业务逻辑</param>
    //    public static void Transaction(Action transactional)
    //    {
    //        Transaction(IsolationLevel.ReadCommitted, transactional);
    //    }
    //}

    /// <summary>
    /// 嵌套事物
    /// </summary>
    public class NhTransactionHelper : IDisposable
    {
        private readonly ISessionFactory m_sessionFactory = SessionFactoryManager.SessionFactory;
        private ISession m_Session;
        private ITransaction m_Transaction;
        private bool IsInActiveTransaction { get; set; }

        private NhTransactionHelper(IsolationLevel level)
        {
            m_Session = SessionFactoryManager.GetCurrentSession();
            m_Transaction = m_Session.Transaction;
            IsInActiveTransaction = m_Transaction.IsActive;

            if (!IsInActiveTransaction)
                m_Transaction = m_Session.BeginTransaction(level);
        }

        /// <summary>
        /// 开启事物
        /// </summary>
        /// <returns></returns>
        public static NhTransactionHelper BeginTransaction()
        {
            return BeginTransaction((IsolationLevel.ReadCommitted));
        }

        /// <summary>
        /// 开启事物
        /// </summary>
        /// <param name="level">事物级别</param>
        /// <returns></returns>
        public static NhTransactionHelper BeginTransaction(IsolationLevel level)
        {
            return new NhTransactionHelper(level);
        }

        /// <summary>
        /// 提交事物
        /// </summary>
        public void Commit()
        {
            if (!IsInActiveTransaction && m_Transaction.IsActive)
                m_Transaction.Commit();
        }

        /// <summary>
        /// 回滚事物
        /// </summary>
        public void Rollback()
        {
            if (!IsInActiveTransaction && m_Transaction.IsActive)
                m_Transaction.Rollback();
        }

        #region Implementation of IDisposable

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            if (!IsInActiveTransaction)
                m_Transaction.Dispose();
        }

        #endregion
    }
}

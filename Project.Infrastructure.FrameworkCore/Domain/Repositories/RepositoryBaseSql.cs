using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using Project.Infrastructure.FrameworkCore.FrameworkCore.Repository.Interface;
using SyncSoft.ROM.Domain.Repository;
using SyncSoft.ROM.Infrastructure.DataNhibernate;

namespace Project.Infrastructure.FrameworkCore.FrameworkCore.Repository
{
    /// <summary>
    /// 数据持久基类
    /// </summary>
    public class RepositoryBaseSql<TEntity, TKey> : RepositoryBase<TEntity, TKey>, IRepositoryBaseExtendSql
        where TEntity : class
    {
        public int ExecuteNoQuery(string sql, params SqlParameter[] cmdParms)
        {
            throw new System.NotImplementedException();
        }

        public void ExecuteProc(string sql, params SqlParameter[] cmdParms)
        {
            throw new System.NotImplementedException();
        }

        public DataTable ExecuteNoQueryToTable(string sql, params SqlParameter[] cmdParms)
        {
            throw new System.NotImplementedException();
        }
    }
}

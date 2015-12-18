using System.Data;
using System.Data.OracleClient;
using Project.Infrastructure.FrameworkCore.Domain.Repositories.Interface;

namespace Project.Infrastructure.FrameworkCore.Domain.Repositories
{
    /// <summary>
    /// 数据持久基类
    /// </summary>
    public class RepositoryBaseOracle<TEntity, TKey> : RepositoryBase<TEntity, TKey>, IRepositoryBaseExtendOracle
        where TEntity : class
    {

        public void ExecuteNoQuery(string sql, params OracleParameter[] cmdParms)
        {
            throw new System.NotImplementedException();
        }

        public void ExecuteProc(string sql, params OracleParameter[] cmdParms)
        {
            throw new System.NotImplementedException();
        }

        public DataTable ExecuteNoQueryToTable(string sql, params OracleParameter[] cmdParms)
        {
            throw new System.NotImplementedException();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.FrameworkCore.FrameworkCore.Repository.Interface
{
    public interface IRepositoryBaseExtendSql
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        int ExecuteNoQuery(string sql, params SqlParameter[] cmdParms);

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cmdParms"></param>
        void ExecuteProc(string sql, params SqlParameter[] cmdParms);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>

        DataTable ExecuteNoQueryToTable(string sql, params SqlParameter[] cmdParms);
    }
}

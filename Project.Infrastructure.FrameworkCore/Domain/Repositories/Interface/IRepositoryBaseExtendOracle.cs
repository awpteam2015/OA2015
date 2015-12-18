using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.FrameworkCore.FrameworkCore.Repository.Interface
{
    public interface IRepositoryBaseExtendOracle
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cmdParms"></param>
        void ExecuteNoQuery(string sql, params OracleParameter[] cmdParms);

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cmdParms"></param>
        void ExecuteProc(string sql, params OracleParameter[] cmdParms);


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cmdParms"></param>
        /// <returns></returns>
        DataTable ExecuteNoQueryToTable(string sql, params OracleParameter[] cmdParms);
    }
}

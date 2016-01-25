using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Infrastructure.FrameworkCore.DataNhibernate;
using Project.Model.ReportManager;

namespace Project.Repository.ReportManager
{
    public class HrReportRepository
    {
        /// <summary>
        /// 角色能查看的页面
        /// </summary>
        /// <param name="logindId">用户Id</param>
        /// <returns></returns>
        public IList<AttendanceViewEntity> GetRoleBrowseFunction(string logindId)
        {
            var list = SessionFactoryManager.GetCurrentSession().CreateSQLQuery(
                @"select distinct f.*
      from pb_module_function f,pb_module m,pb_functionright fr,pb_rolepopedom rp,pb_user_role ur
      where f.moduleid=m.moduleid
      and f.functionid=fr.functionid
      and fr.functionrightid=rp.functionrightid
      and rp.roleid=ur.roleid
      and fr.righttag='浏览'
      and ur.loginid='" + logindId + "' and rp.popedomvalue=1 order by f.orderid").AddEntity(typeof(AttendanceViewEntity));

            return list.List<AttendanceViewEntity>();
        }
    }
}

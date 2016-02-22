using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Transform;
using Project.Infrastructure.FrameworkCore.DataNhibernate;
using Project.Model.ReportManager;

namespace Project.Repository.ReportManager
{
    /// <summary>
    /// 
    //缺勤汇总	缺勤汇总情况统计、缺勤月度汇总报表等。		所属机构、科室、人员、在岗天数、缺勤天数，并支持汇总报表的导出。同时支持月份选择。
    //考勤汇总	考勤汇总情况统计、考勤月度汇总报表等。		所属机构、科室、人数、在岗天数、缺勤天数，并支持汇总报表的导出。同时支持月份选择。
    //考勤月报明细	查看各科室、机构的考勤具体情况。		所属机构、科室、人员、在岗天数、缺勤天数，并支持汇总报表的导出。同时支持月份选择。
    //考勤汇总查询	可以指定考勤类查询的人员的考勤汇总数据。		所属机构、科室、人员、起止月份等信息查询详细考勤数据，并支持汇总报表的导出。
    /// </summary>
    public class HrReportRepository
    {
        /// <summary>
        /// 缺勤汇总
        /// </summary>
        /// <param name="where"></param>
        /// <param name="skipResults"></param>
        /// <param name="maxResults"></param>
        /// <returns></returns>
        public Tuple<IList<AttendanceViewEntity>, int> GerAttendanceReport1(AttendanceViewEntity where, int skipResults, int maxResults, bool ifGetALL = false)
        {
            var whereStr = " ";
            if (!string.IsNullOrWhiteSpace(where.DepartmentCode))
            {
                whereStr += " and a.DepartmentCode=" + where.DepartmentCode;
            }

            if (!string.IsNullOrWhiteSpace(where.EmployeeCode))
            {
                whereStr += " and a.EmployeeCode=" + where.EmployeeCode;
            }

            if (where.Attr_StartDate != null)
            {
                whereStr += " and a.Date>='" + where.Attr_StartDate + "'";
            }

            if (where.Attr_EndDate != null)
            {
                whereStr += " and a.Date<'" + where.Attr_EndDate + "'";
            }

            string sqlStr = @"select a.*,b.WordkDays,c.NotWordkDays from
(select distinct a.EmployeeCode,a.DepartmentCode,a.DepartmentName from  HR_Attendance a) as a
left join 
(select a.EmployeeCode,COUNT(*) as WordkDays from HR_Attendance a where a.State=1 " + whereStr + @" 
  group by a.EmployeeCode) as b
on a.EmployeeCode=b.EmployeeCode
left join 
(select a.EmployeeCode,COUNT(*) as NotWordkDays from HR_Attendance a   where a.State=-1 " + whereStr + @"  group by a.EmployeeCode) as c
on a.EmployeeCode=c.EmployeeCode";

            string countStr = "select count(*) as num from (" + sqlStr + ") as a ";
            var count = SessionFactoryManager.GetCurrentSession().CreateSQLQuery(countStr).AddScalar("num", NHibernateUtil.Int32).UniqueResult<Int32>();

            IList<AttendanceViewEntity> returnList = new List<AttendanceViewEntity>();
            if (ifGetALL)
            {
                returnList = SessionFactoryManager.GetCurrentSession().CreateSQLQuery(sqlStr)
                   .SetResultTransformer(Transformers.AliasToBean(typeof(AttendanceViewEntity))).List<AttendanceViewEntity>();
            }
            else
            {
                returnList = SessionFactoryManager.GetCurrentSession().CreateSQLQuery(sqlStr)
                   .SetFirstResult(skipResults)
                   .SetMaxResults(maxResults)
                   .SetResultTransformer(Transformers.AliasToBean(typeof(AttendanceViewEntity))).List<AttendanceViewEntity>();
            }
            return new Tuple<IList<AttendanceViewEntity>, int>(returnList, count);
        }

        /// <summary>
        /// 考勤汇总
        /// </summary>
        /// <param name="where"></param>
        /// <param name="skipResults"></param>
        /// <param name="maxResults"></param>
        /// <returns></returns>
        public Tuple<IList<AttendanceViewEntity2>, int> GerAttendanceReport2(AttendanceViewEntity2 where, int skipResults, int maxResults, bool ifGetALL = false)
        {
            var whereStr = " ";
            if (!string.IsNullOrWhiteSpace(where.DepartmentCode))
            {
                whereStr += " and a.DepartmentCode=" + where.DepartmentCode;
            }

            if (!string.IsNullOrWhiteSpace(where.EmployeeCode))
            {
                whereStr += " and a.EmployeeCode=" + where.EmployeeCode;
            }

            if (where.Attr_StartDate != null)
            {
                whereStr += " and a.Date>='" + where.Attr_StartDate + "'";
            }

            if (where.Attr_EndDate != null)
            {
                whereStr += " and a.Date<'" + where.Attr_EndDate + "'";
            }

            string sqlStr = @"select a.*,b.WordkDays,c.NotWordkDays,d.EmployeeNum from
(select distinct a.DepartmentCode,a.DepartmentName from  HR_Attendance a) as a
left join 
(select a.DepartmentCode,COUNT(*) as WordkDays from HR_Attendance a where a.State=1 " + whereStr + @"  group by a.DepartmentCode) as b
on a.DepartmentCode=b.DepartmentCode
left join 
(select a.DepartmentCode,COUNT(*) as NotWordkDays from HR_Attendance a   where a.State=-1 " + whereStr + @"  group by a.DepartmentCode) as c
on a.DepartmentCode=c.DepartmentCode
left join 
(select a.DepartmentCode,COUNT(EmployeeCode) as EmployeeNum from dbo.HR_EmployeeInfo a   group by a.DepartmentCode) as d
on a.DepartmentCode=c.DepartmentCode 
";

            string countStr = "select count(*) as num from (" + sqlStr + ") as a ";
            var count = SessionFactoryManager.GetCurrentSession().CreateSQLQuery(countStr).AddScalar("num", NHibernateUtil.Int32).UniqueResult<Int32>();

            IList<AttendanceViewEntity2> returnList = new List<AttendanceViewEntity2>();
            if (ifGetALL)
            {
                returnList = SessionFactoryManager.GetCurrentSession().CreateSQLQuery(sqlStr)
                   .SetResultTransformer(Transformers.AliasToBean(typeof(AttendanceViewEntity2))).List<AttendanceViewEntity2>();
            }
            else
            {
                returnList = SessionFactoryManager.GetCurrentSession().CreateSQLQuery(sqlStr)
                   .SetFirstResult(skipResults)
                   .SetMaxResults(maxResults)
                   .SetResultTransformer(Transformers.AliasToBean(typeof(AttendanceViewEntity2))).List<AttendanceViewEntity2>();
            }
            return new Tuple<IList<AttendanceViewEntity2>, int>(returnList, count);
        }





        /// <summary>
        /// 考勤汇总
        /// </summary>
        /// <param name="where"></param>
        /// <param name="skipResults"></param>
        /// <param name="maxResults"></param>
        /// <returns></returns>
        public Tuple<IList<AttendanceViewEntity>, int> GerAttendanceReport3(AttendanceViewEntity where, int skipResults, int maxResults, bool ifGetALL = false)
        {
            var whereStr = " ";
            if (!string.IsNullOrWhiteSpace(where.DepartmentCode))
            {
                whereStr += " and a.DepartmentCode=" + where.DepartmentCode;
            }

            if (!string.IsNullOrWhiteSpace(where.EmployeeCode))
            {
                whereStr += " and a.EmployeeCode=" + where.EmployeeCode;
            }

            if (where.Attr_StartDate != null)
            {
                whereStr += " and a.Date>='" + where.Attr_StartDate + "'";
            }

            if (where.Attr_EndDate != null)
            {
                whereStr += " and a.Date<'" + where.Attr_EndDate + "'";
            }

            string sqlStr = @"select a.*,b.WordkDays,c.NotWordkDays,d.EmployeeNum from
(select distinct a.DepartmentCode,a.DepartmentName from  HR_Attendance a) as a
left join 
(select a.DepartmentCode,COUNT(*) as WordkDays from HR_Attendance a where a.State=1 " + whereStr + @"  group by a.DepartmentCode) as b
on a.DepartmentCode=b.DepartmentCode
left join 
(select a.DepartmentCode,COUNT(*) as NotWordkDays from HR_Attendance a   where a.State=-1 " + whereStr + @"  group by a.DepartmentCode) as c
on a.DepartmentCode=c.DepartmentCode
left join 
(select a.DepartmentCode,COUNT(EmployeeCode) as EmployeeNum from dbo.HR_EmployeeInfo a   group by a.DepartmentCode) as d
on a.DepartmentCode=c.DepartmentCode 
";

            string countStr = "select count(*) as num from (" + sqlStr + ") as a ";
            var count = SessionFactoryManager.GetCurrentSession().CreateSQLQuery(countStr).AddScalar("num", NHibernateUtil.Int32).UniqueResult<Int32>();

            IList<AttendanceViewEntity> returnList = new List<AttendanceViewEntity>();
            if (ifGetALL)
            {
                returnList = SessionFactoryManager.GetCurrentSession().CreateSQLQuery(sqlStr)
                   .SetResultTransformer(Transformers.AliasToBean(typeof(AttendanceViewEntity))).List<AttendanceViewEntity>();
            }
            else
            {
                returnList = SessionFactoryManager.GetCurrentSession().CreateSQLQuery(sqlStr)
                   .SetFirstResult(skipResults)
                   .SetMaxResults(maxResults)
                   .SetResultTransformer(Transformers.AliasToBean(typeof(AttendanceViewEntity))).List<AttendanceViewEntity>();
            }
            return new Tuple<IList<AttendanceViewEntity>, int>(returnList, count);
        }


        /// <summary>
        /// 考勤汇总
        /// </summary>
        /// <param name="where"></param>
        /// <param name="skipResults"></param>
        /// <param name="maxResults"></param>
        /// <returns></returns>
        public Tuple<IList<AttendanceViewEntity>, int> GerAttendanceReport4(AttendanceViewEntity where, int skipResults, int maxResults, bool ifGetALL = false)
        {
            var whereStr = " ";
            if (!string.IsNullOrWhiteSpace(where.DepartmentCode))
            {
                whereStr += " and a.DepartmentCode=" + where.DepartmentCode;
            }

            if (!string.IsNullOrWhiteSpace(where.EmployeeCode))
            {
                whereStr += " and a.EmployeeCode=" + where.EmployeeCode;
            }

            if (where.Attr_StartDate != null)
            {
                whereStr += " and a.Date>='" + where.Attr_StartDate + "'";
            }

            if (where.Attr_EndDate != null)
            {
                whereStr += " and a.Date<'" + where.Attr_EndDate + "'";
            }

            string sqlStr = @"select a.*,b.WordkDays,c.NotWordkDays,d.EmployeeNum from
(select distinct a.DepartmentCode,a.DepartmentName from  HR_Attendance a) as a
left join 
(select a.DepartmentCode,COUNT(*) as WordkDays from HR_Attendance a where a.State=1 " + whereStr + @"  group by a.DepartmentCode) as b
on a.DepartmentCode=b.DepartmentCode
left join 
(select a.DepartmentCode,COUNT(*) as NotWordkDays from HR_Attendance a   where a.State=-1 " + whereStr + @"  group by a.DepartmentCode) as c
on a.DepartmentCode=c.DepartmentCode
left join 
(select a.DepartmentCode,COUNT(EmployeeCode) as EmployeeNum from dbo.HR_EmployeeInfo a   group by a.DepartmentCode) as d
on a.DepartmentCode=c.DepartmentCode 
";

            string countStr = "select count(*) as num from (" + sqlStr + ") as a ";
            var count = SessionFactoryManager.GetCurrentSession().CreateSQLQuery(countStr).AddScalar("num", NHibernateUtil.Int32).UniqueResult<Int32>();

            IList<AttendanceViewEntity> returnList = new List<AttendanceViewEntity>();
            if (ifGetALL)
            {
                returnList = SessionFactoryManager.GetCurrentSession().CreateSQLQuery(sqlStr)
                   .SetResultTransformer(Transformers.AliasToBean(typeof(AttendanceViewEntity))).List<AttendanceViewEntity>();
            }
            else
            {
                returnList = SessionFactoryManager.GetCurrentSession().CreateSQLQuery(sqlStr)
                   .SetFirstResult(skipResults)
                   .SetMaxResults(maxResults)
                   .SetResultTransformer(Transformers.AliasToBean(typeof(AttendanceViewEntity))).List<AttendanceViewEntity>();
            }
            return new Tuple<IList<AttendanceViewEntity>, int>(returnList, count);
        }

        public Tuple<IList<HREmployeeViewEntity>, int> GerInOutEmployeeReport(HREmployeeViewEntity where, int skipResults, int maxResults, bool ifGetALL = false)
        {
            var whereStr = " ";
            if (!string.IsNullOrWhiteSpace(where.DepartmentCode))
            {
                whereStr += " and a.DepartmentCode=" + where.DepartmentCode;
            }

            if (!string.IsNullOrWhiteSpace(where.EmployeeCode))
            {
                whereStr += " and a.EmployeeCode=" + where.EmployeeCode;
            }

            //if (where.Attr_StartDate != null)
            //{
            //    whereStr += " and a.Date>='" + where.Attr_StartDate + "'";
            //}

            //if (where.Attr_EndDate != null)
            //{
            //    whereStr += " and a.Date<'" + where.Attr_EndDate + "'";
            //}

            string sqlStr = @"select a.*,b.WordkDays,c.NotWordkDays,d.EmployeeNum from
(select distinct a.DepartmentCode,a.DepartmentName from  HR_Attendance a) as a
left join 
(select a.DepartmentCode,COUNT(*) as WordkDays from HR_Attendance a where a.State=1 " + whereStr + @"  group by a.DepartmentCode) as b
on a.DepartmentCode=b.DepartmentCode
left join 
(select a.DepartmentCode,COUNT(*) as NotWordkDays from HR_Attendance a   where a.State=-1 " + whereStr + @"  group by a.DepartmentCode) as c
on a.DepartmentCode=c.DepartmentCode
left join 
(select a.DepartmentCode,COUNT(EmployeeCode) as EmployeeNum from dbo.HR_EmployeeInfo a   group by a.DepartmentCode) as d
on a.DepartmentCode=c.DepartmentCode 
";

            string countStr = "select count(*) as num from (" + sqlStr + ") as a ";
            var count = SessionFactoryManager.GetCurrentSession().CreateSQLQuery(countStr).AddScalar("num", NHibernateUtil.Int32).UniqueResult<Int32>();

            IList<HREmployeeViewEntity> returnList = new List<HREmployeeViewEntity>();
            if (ifGetALL)
            {
                returnList = SessionFactoryManager.GetCurrentSession().CreateSQLQuery(sqlStr)
                   .SetResultTransformer(Transformers.AliasToBean(typeof(AttendanceViewEntity2))).List<HREmployeeViewEntity>();
            }
            else
            {
                returnList = SessionFactoryManager.GetCurrentSession().CreateSQLQuery(sqlStr)
                   .SetFirstResult(skipResults)
                   .SetMaxResults(maxResults)
                   .SetResultTransformer(Transformers.AliasToBean(typeof(HREmployeeViewEntity))).List<HREmployeeViewEntity>();
            }
            return new Tuple<IList<HREmployeeViewEntity>, int>(returnList, count);
        }



    }
}

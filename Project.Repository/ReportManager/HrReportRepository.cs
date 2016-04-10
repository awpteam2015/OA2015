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

        /// <summary>
        /// 进出统计报表
        /// </summary>
        /// <param name="where"></param>
        /// <param name="skipResults"></param>
        /// <param name="maxResults"></param>
        /// <param name="ifGetALL"></param>
        /// <returns></returns>
        public Tuple<IList<EmployeeInOutEntity>, int> GerInOutEmployeeReport(HREmployeeViewEntity where, int skipResults, int maxResults, bool ifGetALL = false)
        {
            var whereStr = " ";
            var inoutType = " ";
            if (!string.IsNullOrWhiteSpace(where.DepartmentCode))
            {
                whereStr += " and a.DepartmentCode in('" + where.DepartmentCode.TrimEnd('\'').Trim(',') + "')";
            }

            if (!string.IsNullOrWhiteSpace(where.EmployeeCode))
            {

                whereStr += " and a.EmployeeCode=" + where.EmployeeCode;
            }

            if (where.CreationTime != null)
            {
                whereStr += " and a.CreationTime>='" + where.CreationTime + "'";
            }

            if (where.CreationTimeEnd != null)
            {
                whereStr += " and a.CreationTime<'" + where.CreationTimeEnd + "'";
            }
            if (where.InOrOut > -1)
            {
                inoutType = " and InOrOut=" + where.InOrOut;
            }
            string exeSqlStr = string.Empty;
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendFormat(@"with tempSour as(  
            select c.*,f.DepartmentCode ParnetDepartmentCode,f.DepartmentName ParnetDepartmentName from
            (");
            sqlStr.AppendFormat(@"select a.EmployeeCode,a.EmployeeName,a.Sex,a.CertNo,a.Birthday,a.EmployeeTypeName,a.DepartmentName,
			a.DepartmentCode,a.WorkState,a.IsDeleted,1 InOrOut,A.CreationTime CreateTime, 5 InorOutType
			from [dbo].[HR_EmployeeInfo] a 				
			where a.IsDeleted=1  {0}
            ", whereStr.Replace("CreationTime", "LastModificationTime"));
            sqlStr.AppendFormat(@" union
            select a.EmployeeCode,a.EmployeeName,a.Sex,a.CertNo,a.Birthday,a.EmployeeTypeName,a.DepartmentName,
            a.DepartmentCode,a.WorkState,a.IsDeleted,0 InOrOut,A.CreateTime,0 InorOutType
			from[dbo].[HR_EmployeeInfoHis]
            a where  a.isInsert=1 {0}", whereStr.Replace("CreationTime", "CreateTime"));
            sqlStr.AppendFormat(@" union
            select a.EmployeeCode,a.EmployeeName,a.Sex,a.CertNo,a.Birthday,a.EmployeeTypeName,a.InDepartmentName DepartmentName,
			a.InDepartmentCode DepartmentCode,a.WorkState, 0 IsDeleted,0 InOrOut,A.CreateTime ,1 InorOutType
			from[dbo].[HR_EmployeeInfoHis]
            a where(a.DepartmentCode<> a.InDepartmentCode and  a.WorkState=a.InWorkState and a.WorkState=1) {0}", whereStr.Replace("CreationTime", "CreateTime"));
            sqlStr.AppendFormat(@"union
            select a.EmployeeCode,a.EmployeeName,a.Sex,a.CertNo,a.Birthday,a.EmployeeTypeName,a.DepartmentName,
             a.DepartmentCode,a.WorkState, 0 IsDeleted,1 InOrOut,A.CreateTime,6 InorOutType
             from[dbo].[HR_EmployeeInfoHis]
             a where(a.DepartmentCode<> a.InDepartmentCode and  a.WorkState= a.InWorkState and a.WorkState= 1) {0}", whereStr.Replace("CreationTime", "CreateTime"));
            sqlStr.AppendFormat(@" union          
             select a.EmployeeCode,a.EmployeeName,a.Sex,a.CertNo,a.Birthday,a.EmployeeTypeName,a.DepartmentName,
            a.DepartmentCode,a.WorkState, 0 IsDeleted,1 InOrOut,A.CreateTime,(case  InWorkState when '2' then 5 else 4 end) InorOutType 
			from[dbo].[HR_EmployeeInfoHis]
            a where(a.WorkState<> a.InWorkState and  a.InWorkState!=1) {0}
            ", whereStr.Replace("CreationTime", "CreateTime"));
            sqlStr.AppendFormat(@" union				          
             select a.EmployeeCode,a.EmployeeName,a.Sex,a.CertNo,a.Birthday,a.EmployeeTypeName,a.DepartmentName,
            a.DepartmentCode,a.WorkState, 0 IsDeleted,0 InOrOut,A.CreateTime,1 InorOutType 
			from[dbo].[HR_EmployeeInfoHis]
            a where(a.WorkState<> a.InWorkState and  a.InWorkState=1) {0}
            ", whereStr.Replace("CreationTime", "CreateTime"));
           
            sqlStr.AppendFormat(@"   ) as c 
			left join PM_Department e  on c.DepartmentCode=e.DepartmentCode
			left join PM_Department f on e.ParentDepartmentCode=f.DepartmentCode	
			where 1=1 )");
            exeSqlStr = string.Format(@"select ParnetDepartmentName,sum(CASE InOrOut WHEN '0' THEN  1 else 0 end) Zjxj,
			sum(CASE InorOutType WHEN 1 THEN  1 else 0 end) Bxdl,
			sum(CASE InorOutType WHEN 0 THEN  1 else 0 end) Ly,
			sum(CASE InorOutType WHEN 3 THEN  1 else 0 end) Dl,
			sum(CASE InOrOut WHEN '1' THEN  1 else 0 end) Jsxj,
			sum(CASE InorOutType WHEN 4 THEN  1 else 0 end) Tx,
			sum(CASE InorOutType WHEN 5 THEN  1 else 0 end) Cz,
			sum(CASE InorOutType WHEN 6 THEN  1 else 0 end) Bxttc
			 from  tempSour a   group by ParnetDepartmentCode,ParnetDepartmentName order by ParnetDepartmentCode");
            //if (!string.IsNullOrWhiteSpace(inoutType))
            //{
            //    sqlStr.AppendFormat("{0}", inoutType);
            //}

            string countStr =string.Format("{0} select count(*) as num from tempSour as b ", sqlStr.ToString());
            var count = SessionFactoryManager.GetCurrentSession().CreateSQLQuery(countStr).AddScalar("num", NHibernateUtil.Int32).UniqueResult<Int32>();
            //sqlStr.AppendFormat("{0}", " order by CreateTime desc");
            IList<EmployeeInOutEntity> returnList = new List<EmployeeInOutEntity>();
            if (ifGetALL)
            {
                returnList = SessionFactoryManager.GetCurrentSession().CreateSQLQuery( string.Format("{0} {1} ", sqlStr.ToString(), exeSqlStr))
                   .SetResultTransformer(Transformers.AliasToBean(typeof(EmployeeInOutEntity))).List<EmployeeInOutEntity>();
            }
            else
            {
                returnList = SessionFactoryManager.GetCurrentSession().CreateSQLQuery(string.Format("{0} {1} ", sqlStr.ToString(), exeSqlStr))
                   .SetFirstResult(skipResults)
                   .SetMaxResults(maxResults)
                   .SetResultTransformer(Transformers.AliasToBean(typeof(EmployeeInOutEntity))).List<EmployeeInOutEntity>();
            }
            return new Tuple<IList<EmployeeInOutEntity>, int>(returnList, count);
        }

        /// <summary>
        /// 人数汇总情况报表
        /// </summary>
        /// <param name="where"></param>
        /// <param name="skipResults"></param>
        /// <param name="maxResults"></param>
        /// <param name="ifGetALL"></param>
        /// <returns></returns>
        public Tuple<IList<EmployeeInOutEntity>, int> GerEmployeeRsReport(HREmployeeViewEntity where, int skipResults, int maxResults, bool ifGetALL = false)
        {
            var whereStr = " ";
            var inoutType = " ";
            if (!string.IsNullOrWhiteSpace(where.DepartmentCode))
            {
                whereStr += " and a.DepartmentCode in('" + where.DepartmentCode.TrimEnd('\'').Trim(',') + "')";
            }

            if (!string.IsNullOrWhiteSpace(where.EmployeeCode))
            {

                whereStr += " and a.EmployeeCode=" + where.EmployeeCode;
            }

            if (where.CreationTime != null)
            {
                whereStr += " and a.CreationTime>='" + where.CreationTime + "'";
            }

            if (where.CreationTimeEnd != null)
            {
                whereStr += " and a.CreationTime<'" + where.CreationTimeEnd + "'";
            }
            if (where.InOrOut > -1)
            {
                inoutType = " and InOrOut=" + where.InOrOut;
            }
            string exeSqlStr = string.Empty;
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendFormat(@"with tempSour as(  
            select c.*,f.DepartmentCode ParnetDepartmentCode,f.DepartmentName ParnetDepartmentName from
            (");
            sqlStr.AppendFormat(@"select a.EmployeeCode,a.EmployeeName,a.Sex,a.CertNo,a.Birthday,a.EmployeeTypeName,a.DepartmentName,
			a.DepartmentCode,a.WorkState,a.IsDeleted,1 InOrOut,A.CreationTime CreateTime, 5 InorOutType
			from [dbo].[HR_EmployeeInfo] a 				
			where a.IsDeleted=1  {0}
            ", whereStr.Replace("CreationTime", "LastModificationTime"));
            sqlStr.AppendFormat(@" union
            select a.EmployeeCode,a.EmployeeName,a.Sex,a.CertNo,a.Birthday,a.EmployeeTypeName,a.DepartmentName,
            a.DepartmentCode,a.WorkState,a.IsDeleted,0 InOrOut,A.CreateTime,0 InorOutType
			from[dbo].[HR_EmployeeInfoHis]
            a where  a.isInsert=1 {0}", whereStr.Replace("CreationTime", "CreateTime"));
            sqlStr.AppendFormat(@" union
            select a.EmployeeCode,a.EmployeeName,a.Sex,a.CertNo,a.Birthday,a.EmployeeTypeName,a.InDepartmentName DepartmentName,
			a.InDepartmentCode DepartmentCode,a.WorkState, 0 IsDeleted,0 InOrOut,A.CreateTime ,1 InorOutType
			from[dbo].[HR_EmployeeInfoHis]
            a where(a.DepartmentCode<> a.InDepartmentCode and  a.WorkState=a.InWorkState and a.WorkState=1) {0}", whereStr.Replace("CreationTime", "CreateTime"));
            sqlStr.AppendFormat(@"union
            select a.EmployeeCode,a.EmployeeName,a.Sex,a.CertNo,a.Birthday,a.EmployeeTypeName,a.DepartmentName,
             a.DepartmentCode,a.WorkState, 0 IsDeleted,1 InOrOut,A.CreateTime,6 InorOutType
             from[dbo].[HR_EmployeeInfoHis]
             a where(a.DepartmentCode<> a.InDepartmentCode and  a.WorkState= a.InWorkState and a.WorkState= 1) {0}", whereStr.Replace("CreationTime", "CreateTime"));
            sqlStr.AppendFormat(@" union          
             select a.EmployeeCode,a.EmployeeName,a.Sex,a.CertNo,a.Birthday,a.EmployeeTypeName,a.DepartmentName,
            a.DepartmentCode,a.WorkState, 0 IsDeleted,1 InOrOut,A.CreateTime,(case  InWorkState when '2' then 5 else 4 end) InorOutType 
			from[dbo].[HR_EmployeeInfoHis]
            a where(a.WorkState<> a.InWorkState and  a.InWorkState!=1) {0}
            ", whereStr.Replace("CreationTime", "CreateTime"));
            sqlStr.AppendFormat(@" union				          
             select a.EmployeeCode,a.EmployeeName,a.Sex,a.CertNo,a.Birthday,a.EmployeeTypeName,a.DepartmentName,
            a.DepartmentCode,a.WorkState, 0 IsDeleted,0 InOrOut,A.CreateTime,1 InorOutType 
			from[dbo].[HR_EmployeeInfoHis]
            a where(a.WorkState<> a.InWorkState and  a.InWorkState=1) {0}
            ", whereStr.Replace("CreationTime", "CreateTime"));

            sqlStr.AppendFormat(@"   ) as c 
			left join PM_Department e  on c.DepartmentCode=e.DepartmentCode
			left join PM_Department f on e.ParentDepartmentCode=f.DepartmentCode	
			where 1=1 )");
            exeSqlStr = string.Format(@"select ParnetDepartmentName,sum(CASE InOrOut WHEN '0' THEN  1 else 0 end) Zjxj,
			sum(CASE InorOutType WHEN 1 THEN  1 else 0 end) Bxdl,
			sum(CASE InorOutType WHEN 0 THEN  1 else 0 end) Ly,
			sum(CASE InorOutType WHEN 3 THEN  1 else 0 end) Dl,
			sum(CASE InOrOut WHEN '1' THEN  1 else 0 end) Jsxj,
			sum(CASE InorOutType WHEN 4 THEN  1 else 0 end) Tx,
			sum(CASE InorOutType WHEN 5 THEN  1 else 0 end) Cz,
			sum(CASE InorOutType WHEN 6 THEN  1 else 0 end) Bxttc
			 from  tempSour a   group by ParnetDepartmentCode,ParnetDepartmentName order by ParnetDepartmentCode");
            //if (!string.IsNullOrWhiteSpace(inoutType))
            //{
            //    sqlStr.AppendFormat("{0}", inoutType);
            //}

            string countStr = string.Format("{0} select count(*) as num from tempSour as b ", sqlStr.ToString());
            var count = SessionFactoryManager.GetCurrentSession().CreateSQLQuery(countStr).AddScalar("num", NHibernateUtil.Int32).UniqueResult<Int32>();
            //sqlStr.AppendFormat("{0}", " order by CreateTime desc");
            IList<EmployeeInOutEntity> returnList = new List<EmployeeInOutEntity>();
            if (ifGetALL)
            {
                returnList = SessionFactoryManager.GetCurrentSession().CreateSQLQuery(string.Format("{0} {1} ", sqlStr.ToString(), exeSqlStr))
                   .SetResultTransformer(Transformers.AliasToBean(typeof(EmployeeInOutEntity))).List<EmployeeInOutEntity>();
            }
            else
            {
                returnList = SessionFactoryManager.GetCurrentSession().CreateSQLQuery(string.Format("{0} {1} ", sqlStr.ToString(), exeSqlStr))
                   .SetFirstResult(skipResults)
                   .SetMaxResults(maxResults)
                   .SetResultTransformer(Transformers.AliasToBean(typeof(EmployeeInOutEntity))).List<EmployeeInOutEntity>();
            }
            return new Tuple<IList<EmployeeInOutEntity>, int>(returnList, count);
        }

        public Tuple<IList<HREmployeeViewEntity>, int> GerEmployeeZHReport(HREmployeeViewEntity where, int skipResults,
            int maxResults, bool ifGetAll = false)
        {
            var whereStr = " ";
            if (!string.IsNullOrWhiteSpace(where.DepartmentCode))
            {
                whereStr += " and a.DepartmentCode in('" + where.DepartmentCode.TrimEnd('\'').Trim(',') + "')";
            }

            if (!string.IsNullOrWhiteSpace(where.EmployeeCode))
            {

                whereStr += " and a.EmployeeCode=" + where.EmployeeCode;
            }

            if (where.CreationTime != null)
            {
                whereStr += " and a.CreationTime>='" + where.CreationTime + "'";
            }

            if (where.CreationTimeEnd != null)
            {
                whereStr += " and a.CreationTime<'" + where.CreationTimeEnd + "'";
            }
            if (where.IsCommy > -1)
            {
                if (where.IsCommy == 1)
                    whereStr += " and JoinCommy is not null";
                else

                    whereStr += " and JoinCommy is  null";
            }
            if (where.Sex.HasValue && where.Sex.Value > -1)
            {
                whereStr += " and Sex=" + where.Sex.Value;
            }
            if (!string.IsNullOrWhiteSpace(where.EmployeeType))
            {
                whereStr += " and EmployeeType=" + where.EmployeeType;
            }
            if (!string.IsNullOrWhiteSpace(where.PostLevel))
            {
                whereStr += " and PostLevel=" + where.PostLevel;
            }
            if (!string.IsNullOrWhiteSpace(where.PostProperty))
            {
                whereStr += " and PostProperty=" + where.PostProperty;
            }
            if (!string.IsNullOrWhiteSpace(where.Duties))
            {
                whereStr += " and Duties=" + where.Duties;
            }
            if (!string.IsNullOrWhiteSpace(where.Education))
            {
                whereStr +=
                    string.Format(
                        " and a.PkId in(select c.EmployeeID  from HR_LearningExperiences c where c.Education='{0}')", where.Education);
            }
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendFormat(@"select  a.EmployeeCode,a.EmployeeName,a.Sex,a.CertNo,a.Birthday,a.EmployeeTypeName,a.DepartmentName,a.DepartmentName InDepartmentName,
                a.WorkStateName,a.WorkStateName InWorkStateName,a.IsDeleted,1 InOrOut,JoinCommy,Duties,DutiesName,PostLevel,PostLevelName,PostProperty,PostPropertyName,
			(	CASE WHEN ISNULL(JoinCommy,0)=0 THEN 0
            ELSE 1
            END) IsCommy ,(select t.KeyName from SM_Dictionary t where t.ParentKeyCode='Education' and t.KeyValue =(select Max(b.Education) from HR_LearningExperiences b where b.EmployeeID=a.PkId))
            EducationName    from [HR_EmployeeInfo] a where  a.IsDeleted=0 {0}", whereStr);

            string countStr = "select count(*) as num from (" + sqlStr.ToString() + ") as b ";
            var count = SessionFactoryManager.GetCurrentSession().CreateSQLQuery(countStr).AddScalar("num", NHibernateUtil.Int32).UniqueResult<Int32>();

            IList<HREmployeeViewEntity> returnList = new List<HREmployeeViewEntity>();
            if (ifGetAll)
            {
                returnList = SessionFactoryManager.GetCurrentSession().CreateSQLQuery(sqlStr.ToString())
                   .SetResultTransformer(Transformers.AliasToBean(typeof(HREmployeeViewEntity))).List<HREmployeeViewEntity>();
            }
            else
            {
                returnList = SessionFactoryManager.GetCurrentSession().CreateSQLQuery(sqlStr.ToString())
                   .SetFirstResult(skipResults)
                   .SetMaxResults(maxResults)
                   .SetResultTransformer(Transformers.AliasToBean(typeof(HREmployeeViewEntity))).List<HREmployeeViewEntity>();
            }
            return new Tuple<IList<HREmployeeViewEntity>, int>(returnList, count);
        }

        /// <summary>
        /// 党员统计
        /// </summary>
        /// <param name="where"></param>
        /// <param name="skipResults"></param>
        /// <param name="maxResults"></param>
        /// <param name="ifGetALL"></param>
        /// <returns></returns>
        public Tuple<IList<HREmployeeViewEntity>, int> GerEmployeeDYReport(HREmployeeViewEntity where, int skipResults, int maxResults, bool ifGetALL = false)
        {
            var whereStr = " ";
            if (!string.IsNullOrWhiteSpace(where.DepartmentCode))
            {
                whereStr += " and a.DepartmentCode in('" + where.DepartmentCode.TrimEnd('\'').Trim(',') + "')";

            }

            if (!string.IsNullOrWhiteSpace(where.EmployeeCode))
            {

                whereStr += " and a.EmployeeCode=" + where.EmployeeCode;
            }

            if (where.CreationTime != null)
            {
                whereStr += " and a.CreationTime>='" + where.CreationTime + "'";
            }

            if (where.CreationTimeEnd != null)
            {
                whereStr += " and a.CreationTime<'" + where.CreationTimeEnd + "'";
            }
            if (where.IsCommy > -1)
            {
                if (where.IsCommy == 1)
                    whereStr += " and JoinCommy is not null";
                else

                    whereStr += " and JoinCommy is  null";
            }
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendFormat(@"select  a.EmployeeCode,a.EmployeeName,a.Sex,a.CertNo,a.Birthday,a.EmployeeTypeName,a.DepartmentName,a.DepartmentName InDepartmentName,
                a.WorkStateName,a.WorkStateName InWorkStateName,a.IsDeleted,1 InOrOut,JoinCommy,
			(	CASE WHEN ISNULL(JoinCommy,0)=0 THEN 0
            ELSE 1
            END) IsCommy    from [HR_EmployeeInfo] a where a.IsDeleted=0 {0}", whereStr);

            string countStr = "select count(*) as num from (" + sqlStr.ToString() + ") as b ";
            var count = SessionFactoryManager.GetCurrentSession().CreateSQLQuery(countStr).AddScalar("num", NHibernateUtil.Int32).UniqueResult<Int32>();

            IList<HREmployeeViewEntity> returnList = new List<HREmployeeViewEntity>();
            if (ifGetALL)
            {
                returnList = SessionFactoryManager.GetCurrentSession().CreateSQLQuery(sqlStr.ToString())
                   .SetResultTransformer(Transformers.AliasToBean(typeof(HREmployeeViewEntity))).List<HREmployeeViewEntity>();
            }
            else
            {
                returnList = SessionFactoryManager.GetCurrentSession().CreateSQLQuery(sqlStr.ToString())
                   .SetFirstResult(skipResults)
                   .SetMaxResults(maxResults)
                   .SetResultTransformer(Transformers.AliasToBean(typeof(HREmployeeViewEntity))).List<HREmployeeViewEntity>();
            }
            return new Tuple<IList<HREmployeeViewEntity>, int>(returnList, count);
        }


        /// <summary>
        /// 学历统计
        /// </summary>
        /// <param name="where"></param>
        /// <param name="skipResults"></param>
        /// <param name="maxResults"></param>
        /// <param name="ifGetALL"></param>
        /// <returns></returns>
        public Tuple<IList<HREmployeeViewEntity>, int> GerEmployeeXLReport(HREmployeeViewEntity where, int skipResults, int maxResults, bool ifGetALL = false)
        {
            var whereStr = " ";
            if (!string.IsNullOrWhiteSpace(where.DepartmentCode))
            {
                whereStr += " and a.DepartmentCode in('" + where.DepartmentCode.TrimEnd('\'').Trim(',') + "')";

            }

            if (!string.IsNullOrWhiteSpace(where.EmployeeCode))
            {

                whereStr += " and a.EmployeeCode=" + where.EmployeeCode;
            }

            if (where.CreationTime != null)
            {
                whereStr += " and a.CreationTime>='" + where.CreationTime + "'";
            }

            if (where.CreationTimeEnd != null)
            {
                whereStr += " and a.CreationTime<'" + where.CreationTimeEnd + "'";
            }

            StringBuilder sqlStr = new StringBuilder();
            sqlStr.AppendFormat(@"select  a.EmployeeCode,a.EmployeeName,a.Sex,a.CertNo,a.Birthday,a.EmployeeTypeName,a.DepartmentName,a.DepartmentName InDepartmentName,
                a.WorkStateName,a.WorkStateName InWorkStateName,a.IsDeleted,1 InOrOut,JoinCommy,
			(	CASE WHEN ISNULL(JoinCommy,0)=0 THEN 0
            ELSE 1
            END) IsCommy,
        (select t.KeyName from SM_Dictionary t where t.ParentKeyCode='Education' and t.KeyValue =(select Max(b.Education) from HR_LearningExperiences b where b.EmployeeID=a.PkId))
        EducationName    from [HR_EmployeeInfo] a where a.IsDeleted=0 {0}", whereStr);

            string countStr = "select count(*) as num from (" + sqlStr.ToString() + ") as b ";
            var count = SessionFactoryManager.GetCurrentSession().CreateSQLQuery(countStr).AddScalar("num", NHibernateUtil.Int32).UniqueResult<Int32>();

            IList<HREmployeeViewEntity> returnList = new List<HREmployeeViewEntity>();
            if (ifGetALL)
            {
                returnList = SessionFactoryManager.GetCurrentSession().CreateSQLQuery(sqlStr.ToString())
                   .SetResultTransformer(Transformers.AliasToBean(typeof(HREmployeeViewEntity))).List<HREmployeeViewEntity>();
            }
            else
            {
                returnList = SessionFactoryManager.GetCurrentSession().CreateSQLQuery(sqlStr.ToString())
                   .SetFirstResult(skipResults)
                   .SetMaxResults(maxResults)
                   .SetResultTransformer(Transformers.AliasToBean(typeof(HREmployeeViewEntity))).List<HREmployeeViewEntity>();
            }
            return new Tuple<IList<HREmployeeViewEntity>, int>(returnList, count);
        }

    }
}

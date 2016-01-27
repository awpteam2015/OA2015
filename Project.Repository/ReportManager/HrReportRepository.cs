﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Transform;
using Project.Infrastructure.FrameworkCore.DataNhibernate;
using Project.Model.ReportManager;

namespace Project.Repository.ReportManager
{
    public class HrReportRepository
    {

        public IList<AttendanceViewEntity> GerAttendanceView(AttendanceViewEntity where, int skipResults, int maxResults)
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
                whereStr += " and a.Date<='" + where.Attr_EndDate + "'";
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

            var list = SessionFactoryManager.GetCurrentSession().CreateSQLQuery(sqlStr)
                    .SetFirstResult(skipResults)
                    .SetMaxResults(maxResults)
                    .SetResultTransformer(Transformers.AliasToBean(typeof(AttendanceViewEntity))).List<AttendanceViewEntity>();

            return list;
        }
    }
}

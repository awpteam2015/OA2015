using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.ToolKit;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;
using Project.Infrastructure.FrameworkCore.WebMvc.Controllers.Results;
using Project.Infrastructure.FrameworkCore.WebMvc.Models;
using Project.Model.HRManager;
using Project.Model.ReportManager;
using Project.Service.HRManager;
using Project.Service.ReportManager;

namespace Project.WebApplication.Areas.ReportManager.Controllers
{
    public class HrReportController : Controller
    {
        // GET: ReportManager/HrReport
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AttendanceReport()
        {
            return View();
        }

        public ActionResult AttendanceReport2()
        {
            return View();
        }

        public AbpJsonResult GetAttendanceReport1()
        {
            var pIndex = this.Request["page"].ConvertTo<int>();
            var pSize = this.Request["rows"].ConvertTo<int>();
            var where = new AttendanceViewEntity();
            if (!string.IsNullOrWhiteSpace(RequestHelper.GetFormString("Date")))
            {
                var date = RequestHelper.GetDateTime("Date").GetValueOrDefault();
                int days = DateTime.DaysInMonth(date.Year, date.Month);
                where.Attr_StartDate = date;
                where.Attr_EndDate = where.Attr_StartDate.GetValueOrDefault().AddDays(days);
            }
            where.DepartmentCode = RequestHelper.GetFormString("DepartmentCode");

            //where.PkId = RequestHelper.GetFormString("PkId");
            //where.AttendanceUploadRecordId = RequestHelper.GetFormString("AttendanceUploadRecordId");
            //where.EmployeeCode = RequestHelper.GetFormString("EmployeeCode");
            //where.DepartmentCode = RequestHelper.GetFormString("DepartmentCode");
            //where.DepartmentName = RequestHelper.GetFormString("DepartmentName");
            //where.State = RequestHelper.GetFormString("State");
            //where.Date = RequestHelper.GetFormString("Date");
            //where.Remark = RequestHelper.GetFormString("Remark");
            //where.CreatorUserCode = RequestHelper.GetFormString("CreatorUserCode");
            //where.CreatorUserName = RequestHelper.GetFormString("CreatorUserName");
            //where.CreateTime = RequestHelper.GetFormString("CreateTime");
            //where.IsDelete = RequestHelper.GetFormString("IsDelete");
            var searchList = HrReportService.GetInstance().GerAttendanceReport1(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        public AbpJsonResult GetAttendanceReport2()
        {
            var pIndex = this.Request["page"].ConvertTo<int>();
            var pSize = this.Request["rows"].ConvertTo<int>();
            var where = new AttendanceViewEntity2();
            if (!string.IsNullOrWhiteSpace(RequestHelper.GetFormString("Date")))
            {
                var date = RequestHelper.GetDateTime("Date").GetValueOrDefault();
                int days = DateTime.DaysInMonth(date.Year, date.Month);
                where.Attr_StartDate = date;
                where.Attr_EndDate = where.Attr_StartDate.GetValueOrDefault().AddDays(days);
            }
            where.DepartmentCode = RequestHelper.GetFormString("DepartmentCode");
            //where.PkId = RequestHelper.GetFormString("PkId");
            //where.AttendanceUploadRecordId = RequestHelper.GetFormString("AttendanceUploadRecordId");
            //where.EmployeeCode = RequestHelper.GetFormString("EmployeeCode");
            //where.DepartmentCode = RequestHelper.GetFormString("DepartmentCode");
            //where.DepartmentName = RequestHelper.GetFormString("DepartmentName");
            //where.State = RequestHelper.GetFormString("State");
            //where.Date = RequestHelper.GetFormString("Date");
            //where.Remark = RequestHelper.GetFormString("Remark");
            //where.CreatorUserCode = RequestHelper.GetFormString("CreatorUserCode");
            //where.CreatorUserName = RequestHelper.GetFormString("CreatorUserName");
            //where.CreateTime = RequestHelper.GetFormString("CreateTime");
            //where.IsDelete = RequestHelper.GetFormString("IsDelete");
            var searchList = HrReportService.GetInstance().GerAttendanceReport2(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


    }
}
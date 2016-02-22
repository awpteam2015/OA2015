using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Aspose.Cells;
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

            var searchList = HrReportService.GetInstance().GerAttendanceReport1(GetWhere1(), (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }

        public void ExportReport1()
        {
            string excelFileName =DateTime.Now.ToString()+ ".xls";
            //防止中文文件名IE下乱码的问题
            // if (Request.Browser.Browser == "IE" || Request.Browser.Browser == "InternetExplorer")
            if (Request.ServerVariables["http_user_agent"].ToLower().IndexOf("firefox") != -1)
            {

            }
            else
                excelFileName = HttpUtility.UrlPathEncode(excelFileName);

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=" + excelFileName);
            Response.AddHeader("Cache-Control", "max-age=0");
            Response.Charset = "GB2312";
            Response.ContentEncoding = Encoding.GetEncoding("GB2312");
            Response.ContentType = "application/vnd.xls";


            var searchList = HrReportService.GetInstance().GerAttendanceReport1(GetWhere1(), 0, 0, true);

            var jsonBuilder = new StringBuilder();
            jsonBuilder.AppendFormat(@"<table class='GridViewStyle' style='border-collapse:collapse;width:1000px;' cellspacing='0' rules='all' border='1'>
                <tr>
                    <th>部门编码</th>
                    <th>部门名称</th>
                    <th>在岗天数</th>
                    <th>缺勤天数</th>
                    <th>工号</th></tr>");
            searchList.Item1.ForEach(p =>
            {
                jsonBuilder.Append("<tr>");
                jsonBuilder.AppendFormat("<td>{0}</td>", p.DepartmentCode);
                jsonBuilder.AppendFormat("<td>{0}</td>", p.DepartmentName);
                jsonBuilder.AppendFormat("<td>{0}</td>", p.WordkDays);
                jsonBuilder.AppendFormat("<td>{0}</td>", p.NotWordkDays);
                jsonBuilder.AppendFormat("<td>{0}</td>", p.EmployeeCode);
                jsonBuilder.Append("</tr>");
            });

            jsonBuilder.Append("</table>");
            Response.Write(jsonBuilder.ToString());
            Response.End();
        }

        public AttendanceViewEntity GetWhere1()
        {
            var where = new AttendanceViewEntity();
            if (!string.IsNullOrWhiteSpace(RequestHelper.GetString("Date")))
            {
                var date = RequestHelper.GetDateTime("Date").GetValueOrDefault();
                int days = DateTime.DaysInMonth(date.Year, date.Month);
                where.Attr_StartDate = date;
                where.Attr_EndDate = where.Attr_StartDate.GetValueOrDefault().AddDays(days);
            }
            where.DepartmentCode = RequestHelper.GetString("DepartmentCode");
            return where;
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

        public ActionResult EmployeeInOutReport()
        {
            return View();
        }

        public AbpJsonResult GetEmployeeInOutReport()
        {
            var pIndex = this.Request["page"].ConvertTo<int>();
            var pSize = this.Request["rows"].ConvertTo<int>();
            var where = new HREmployeeViewEntity();
            if (!string.IsNullOrWhiteSpace(RequestHelper.GetFormString("Date")))
            {
                var date = RequestHelper.GetDateTime("Date").GetValueOrDefault();
                var dateEnd = RequestHelper.GetDateTime("EndDate").GetValueOrDefault();
                //int days = DateTime.DaysInMonth(date.Year, date.Month);
                where.CreationTime = date;
                where.CreationTimeEnd = dateEnd.AddDays(1);
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
            var searchList = HrReportService.GetInstance().GetHREmployeeReport(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }

    }
}
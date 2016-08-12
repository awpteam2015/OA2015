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
using Project.Service.PermissionManager;
using Project.WebApplication.Controllers;

namespace Project.WebApplication.Areas.ReportManager.Controllers
{
    public class HrReportController : BaseController
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
            string excelFileName = DateTime.Now.ToString() + ".xls";
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
                    <th>日班天数</th>
                    <th>夜班天数</th>
                    <th>公休天数</th>
                    <th>值班天数</th>
                    <th>缺勤天数</th>
                    <th>工号</th></tr>");
            searchList.Item1.ForEach(p =>
            {
                jsonBuilder.Append("<tr>");
                jsonBuilder.AppendFormat("<td>{0}</td>", p.DepartmentCode);
                jsonBuilder.AppendFormat("<td>{0}</td>", p.DepartmentName);
                jsonBuilder.AppendFormat("<td>{0}</td>", p.RiDays);
                jsonBuilder.AppendFormat("<td>{0}</td>", p.YeDays);
                jsonBuilder.AppendFormat("<td>{0}</td>", p.GongDays);
                jsonBuilder.AppendFormat("<td>{0}</td>", p.ZhiDays);
                jsonBuilder.AppendFormat("<td>{0}</td>", p.QueDays);
                jsonBuilder.AppendFormat("<td>{0}</td>", p.EmployeeCode);
                jsonBuilder.Append("</tr>");
            });

            jsonBuilder.Append("</table>");
            Response.Write(jsonBuilder.ToString());
            Response.End();
        }


        public AbpJsonResult ExportEmployeeExcel()
        {
            var type = RequestHelper.GetQueryString("ReportType");
            var filepath = "/UploadFile/Temp/";
            var rootpath = this.Request.MapPath("/");
            var fileName = DateTime.Now.ToString("yyyyMMHHddmmssfff") + ".xls";

            var where = new HREmployeeViewEntity();
            if (!string.IsNullOrWhiteSpace(RequestHelper.GetQueryString("Date")))
            {
                var date = RequestHelper.GetDateTime("Date").GetValueOrDefault();
                where.CreationTime = date;
            }
            if (!string.IsNullOrWhiteSpace(RequestHelper.GetQueryString("EndDate")))
            {
                var dateEnd = RequestHelper.GetDateTime("EndDate").GetValueOrDefault();
                where.CreationTimeEnd = dateEnd.AddDays(1);
            }
            where.DepartmentCode = RequestHelper.GetQueryString("DepartmentCode");
            if (!string.IsNullOrWhiteSpace(where.DepartmentCode))
            {
                where.DepartmentCode = string.Join("','", (DepartmentService.GetInstance().GetChiledArr(where.DepartmentCode, LoginUserInfo.UserDepartmentList.ToList(), LoginUserInfo.IsAdmin)));
            }
            where.Sex = RequestHelper.GetQueryInt("Sex", -1);
            where.IsCommy = RequestHelper.GetQueryInt("IsCommy", -1);

            where.EmployeeType = RequestHelper.GetQueryString("EmployeeType");
            where.Duties = RequestHelper.GetQueryString("Duties");
            where.PostLevel = RequestHelper.GetQueryString("PostLevel");
            where.PostProperty = RequestHelper.GetQueryString("PostProperty");
            where.Education = RequestHelper.GetQueryString("Education");
            where.Degree = RequestHelper.GetQueryString("Degree");

            where.PoliticsName = RequestHelper.GetQueryString("PoliticsName");

            where.LevNum = RequestHelper.GetQueryString("LevNum");
            where.KHComment = RequestHelper.GetQueryString("KHComment");
            //where.DepartmentCode = RequestHelper.GetFormString("DepartmentCode");
            var searchList = HrReportService.GetInstance().GetEmployeeZHReport(where, 0, 0, true);
            //var searchList = EmployeeInfoService.GetInstance().GetList(where, false);
            searchList.Item1.ForEach(p => p.SexName = (p.Sex == 0 ? "女" : "男"));
            var tempFileName = "EmployeeReportExport.xlsx";//模板名称
                                                           //switch(type)
                                                           //{

            //    case "1":
            //        tempFileName = "LXEmployeeReportExport.xlsx";//员工类型统计报表
            //        break;
            //    case "2":
            //        ViewBag.TitleStr = "员工岗位统计报表";
            //        break;
            //    case "3":
            //        ViewBag.TitleStr = "员工部门统计报表";
            //        break;
            //    case "4":
            //        ViewBag.TitleStr = "员工党员统计报表";
            //        break;
            //    case "5":
            //        ViewBag.TitleStr = "员工学历统计报表";
            //        break;
            //    default:
            //        break;
            //}
            var bl = AsposeCellsHelper.ExportToExcel<IList<HREmployeeViewEntity>>(searchList.Item1, "EmployeeInfo",
                $"{rootpath}/TemplateFile/{tempFileName}", $"{rootpath}{filepath}{fileName}"
                , new Dictionary<string, object>());
            //var bl = true;

            var result = new AjaxResponse<EmployeeInfoEntity>()
            {
                success = bl,
                targeturl = filepath + fileName
            };
            return new AbpJsonResult(result, new NHibernateContractResolver());

        }


        public AbpJsonResult ExportRsEmployeeExcel()
        {
            var type = RequestHelper.GetQueryString("ReportType");
            var filepath = "/UploadFile/Temp/";
            var rootpath = this.Request.MapPath("/");
            var fileName = DateTime.Now.ToString("yyyyMMHHddmmssfff") + ".xls";

            var where = new HREmployeeViewEntity();
            if (!string.IsNullOrWhiteSpace(RequestHelper.GetQueryString("Date")))
            {
                var date = RequestHelper.GetDateTime("Date").GetValueOrDefault();
                where.CreationTime = date;
            }
            if (!string.IsNullOrWhiteSpace(RequestHelper.GetQueryString("EndDate")))
            {
                var dateEnd = RequestHelper.GetDateTime("EndDate").GetValueOrDefault();
                where.CreationTimeEnd = dateEnd.AddDays(1);
            }
            where.DepartmentCode = RequestHelper.GetQueryString("DepartmentCode");
            if (!string.IsNullOrWhiteSpace(where.DepartmentCode))
            {
                where.DepartmentCode = string.Join("','", (DepartmentService.GetInstance().GetChiledArr(where.DepartmentCode, LoginUserInfo.UserDepartmentList.ToList(), LoginUserInfo.IsAdmin)));
            }


            var searchList = HrReportService.GetInstance().GerEmployeeRsReport(where, 0, 0, true);
            var tempFileName = "EmployeeRsReportExport.xlsx";//模板名称
                                                         
            var bl = AsposeCellsHelper.ExportToExcel<IList<EmployeeRsEntity>>(searchList.Item1, "EmployeeInfo",
                $"{rootpath}/TemplateFile/{tempFileName}", $"{rootpath}{filepath}{fileName}"
                , new Dictionary<string, object>());
            //var bl = true;

            var result = new AjaxResponse<EmployeeInfoEntity>()
            {
                success = bl,
                targeturl = filepath + fileName
            };
            return new AbpJsonResult(result, new NHibernateContractResolver());

        }

        public AbpJsonResult ExportInOrOutEmployeeExcel()
        {
            var type = RequestHelper.GetQueryString("ReportType");
            var filepath = "/UploadFile/Temp/";
            var rootpath = this.Request.MapPath("/");
            var fileName = DateTime.Now.ToString("yyyyMMHHddmmssfff") + ".xls";

            var where = new HREmployeeViewEntity();
            if (!string.IsNullOrWhiteSpace(RequestHelper.GetQueryString("Date")))
            {
                var date = RequestHelper.GetDateTime("Date").GetValueOrDefault();
                where.CreationTime = date;
            }
            if (!string.IsNullOrWhiteSpace(RequestHelper.GetQueryString("EndDate")))
            {
                var dateEnd = RequestHelper.GetDateTime("EndDate").GetValueOrDefault();
                where.CreationTimeEnd = dateEnd.AddDays(1);
            }
            where.DepartmentCode = RequestHelper.GetQueryString("DepartmentCode");
            if (!string.IsNullOrWhiteSpace(where.DepartmentCode))
            {
                where.DepartmentCode = string.Join("','", (DepartmentService.GetInstance().GetChiledArr(where.DepartmentCode, LoginUserInfo.UserDepartmentList.ToList(), LoginUserInfo.IsAdmin)));
            }


            var searchList = HrReportService.GetInstance().GetHREmployeeReport(where, 0, 0, true);
            var tempFileName = "EmployeeInOrOutReportExport.xlsx";//模板名称

            var bl = AsposeCellsHelper.ExportToExcel<IList<EmployeeInOutEntity>>(searchList.Item1, "EmployeeInfo",
                $"{rootpath}/TemplateFile/{tempFileName}", $"{rootpath}{filepath}{fileName}"
                , new Dictionary<string, object>());
            //var bl = true;

            var result = new AjaxResponse<EmployeeInfoEntity>()
            {
                success = bl,
                targeturl = filepath + fileName
            };
            return new AbpJsonResult(result, new NHibernateContractResolver());

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

        /// <summary>
        /// 人数统计情况报表
        /// </summary>
        /// <returns></returns>
        public ActionResult EmployeeRSReport()
        {
            return View();
        }

        /// <summary>
        /// 综合报表
        /// </summary>
        /// <returns></returns>
        public ActionResult EmployeeZHReport(string type)
        {
            ViewBag.ReportType = type;
            switch (type)
            {
                case null:
                    ViewBag.TitleStr = "员工综合统计报表";
                    break;
                case "1":
                    ViewBag.TitleStr = "员工类型统计报表";
                    break;
                case "2":
                    ViewBag.TitleStr = "员工岗位统计报表";
                    break;
                case "3":
                    ViewBag.TitleStr = "员工部门统计报表";
                    break;
                case "4":
                    ViewBag.TitleStr = "员工党员统计报表";
                    break;
                case "5":
                    ViewBag.TitleStr = "员工学历统计报表";
                    break;
            }
            return View();
        }

        /// <summary>
        /// 党员统计报表
        /// </summary>
        /// <returns></returns>
        public ActionResult EmployeeDYReport()
        {
            return View();
        }
        /// <summary>
        /// 学历统计报表
        /// </summary>
        /// <returns></returns>
        public ActionResult EmployeeXLReport()
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

            where.DepartmentCode = string.Join("','", (DepartmentService.GetInstance().GetChiledArr(where.DepartmentCode, LoginUserInfo.UserDepartmentList.ToList(), LoginUserInfo.IsAdmin)));

            where.InOrOut = RequestHelper.GetFormInt("InOrOut", -1);
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

        public AbpJsonResult GetEmployeeRsReport()
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

            where.DepartmentCode = string.Join("','", (DepartmentService.GetInstance().GetChiledArr(where.DepartmentCode, LoginUserInfo.UserDepartmentList.ToList(), LoginUserInfo.IsAdmin)));

            //where.InOrOut = RequestHelper.GetFormInt("InOrOut", -1);
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
            var searchList = HrReportService.GetInstance().GerEmployeeRsReport(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }

        public AbpJsonResult GetEmployeeZHReport()
        {
            var pIndex = this.Request["page"].ConvertTo<int>();
            var pSize = this.Request["rows"].ConvertTo<int>();
            var where = new HREmployeeViewEntity();
            if (!string.IsNullOrWhiteSpace(RequestHelper.GetFormString("Date")))
            {
                var date = RequestHelper.GetDateTime("Date").GetValueOrDefault();
                where.CreationTime = date;
            }
            if (!string.IsNullOrWhiteSpace(RequestHelper.GetFormString("EndDate")))
            {
                var dateEnd = RequestHelper.GetDateTime("EndDate").GetValueOrDefault();
                where.CreationTimeEnd = dateEnd.AddDays(1);
            }
            where.DepartmentCode = RequestHelper.GetFormString("DepartmentCode");
            if (!string.IsNullOrWhiteSpace(where.DepartmentCode))
            {
                where.DepartmentCode = string.Join("','", (DepartmentService.GetInstance().GetChiledArr(where.DepartmentCode, LoginUserInfo.UserDepartmentList.ToList(), LoginUserInfo.IsAdmin)));
            }
            where.Sex = RequestHelper.GetFormInt("Sex", -1);
            where.IsCommy = RequestHelper.GetFormInt("IsCommy", -1);

            where.EmployeeType = RequestHelper.GetFormString("EmployeeType");
            where.Duties = RequestHelper.GetFormString("Duties");
            where.PostLevel = RequestHelper.GetFormString("PostLevel");
            where.PostProperty = RequestHelper.GetFormString("PostProperty");
            where.Education = RequestHelper.GetFormString("Education");
            where.Degree = RequestHelper.GetFormString("Degree");

            where.PoliticsName = RequestHelper.GetFormString("PoliticsName");

            where.LevNum = RequestHelper.GetFormString("LevNum");
            where.KHComment = RequestHelper.GetFormString("KHComment");
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
            var searchList = HrReportService.GetInstance().GetEmployeeZHReport(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }

        public AbpJsonResult GetEmployeeDYReport()
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
            if (!string.IsNullOrWhiteSpace(where.DepartmentCode))
            {
                where.DepartmentCode = string.Join("','", (DepartmentService.GetInstance().GetChiledArr(where.DepartmentCode, LoginUserInfo.UserDepartmentList.ToList(), LoginUserInfo.IsAdmin)));
            }
            where.IsCommy = RequestHelper.GetFormInt("IsCommy", -1);
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
            var searchList = HrReportService.GetInstance().GerEmployeeDYReport(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        public AbpJsonResult GetEmployeXLReport()
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
            if (!string.IsNullOrWhiteSpace(where.DepartmentCode))
            {
                where.DepartmentCode = string.Join("','", (DepartmentService.GetInstance().GetChiledArr(where.DepartmentCode, LoginUserInfo.UserDepartmentList.ToList(), LoginUserInfo.IsAdmin)));
            }
            //where.IsCommy = RequestHelper.GetFormInt("IsCommy", -1);
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
            var searchList = HrReportService.GetInstance().GerEmployeeXLReport(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }
    }
}
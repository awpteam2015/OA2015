

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Aspose.Cells;
using AutoMapper;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.ToolKit;
using Project.Infrastructure.FrameworkCore.ToolKit.Extensions;
using Project.Infrastructure.FrameworkCore.ToolKit.JsonHandler;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;
using Project.Infrastructure.FrameworkCore.WebMvc.Controllers.Results;
using Project.Infrastructure.FrameworkCore.WebMvc.Models;
using Project.Model.HRManager;
using Project.Model.Other;
using Project.Model.ReportManager;
using Project.Service.HRManager;
using Project.Service.PermissionManager;
using Project.Service.ReportManager;
using Project.WebApplication.Controllers;

namespace Project.WebApplication.Areas.HRManager.Controllers
{
    public class AttendanceController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = AttendanceService.GetInstance().GetModelByPk(pkId);
                ViewBag.BindEntity = JsonHelper.JsonSerializer(entity);
            }
            return View();
        }


        public ActionResult List()
        {
            return View();
        }

        public AbpJsonResult GetList()
        {
            var pIndex = this.Request["page"].ConvertTo<int>();
            var pSize = this.Request["rows"].ConvertTo<int>();
            var searchList = AttendanceService.GetInstance().Search(GetWhere(), (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }

        public void ExportReport()
        {
            var where = new AttendanceEntity();
            where.DepartmentCode = RequestHelper.GetString("DepartmentCode");
            var date = RequestHelper.GetDateTime("Attr_ExportDate").GetValueOrDefault();
            date = new DateTime(date.Year, date.Month, 1);

            where.Attr_StartDate = date;
            where.Attr_EndDate = date.AddMonths(1).AddDays(-1);
            var searchList = AttendanceService.GetInstance().GetList(where).GroupBy(p => new { EmployeeCode = p.EmployeeCode, EmployeeName = p.EmployeeName })
  .Select(g => g.First())
  .ToList();


            string[] Day = new string[] { "日", "一", "二", "三", "四", "五", "六" };

            var exportMonth = date;
            int days = DateTime.DaysInMonth(exportMonth.Year, exportMonth.Month);
            var DayOfWeek = new ExcelData() { title = "星期" };
            var DayOfMonth = new ExcelData() { title = "日期" };
            for (int i = 0; i < days; i++)
            {
                var data1 = Day[Convert.ToInt32(exportMonth.AddDays(i).DayOfWeek.ToString("d"))].ToString();
                var dateInfo = exportMonth.AddDays(i);
                var data2 = dateInfo.Day.ToString();

                var property1 = DayOfWeek.GetType().GetProperty("data" + (i + 1));
                property1.SetValue(DayOfWeek, data1);

                var property2 = DayOfMonth.GetType().GetProperty("data" + (i + 1));
                property2.SetValue(DayOfMonth, data2);

                searchList.ForEach(p =>
                {
                    var stateStr = AttendanceService.GetInstance().GetStateStr(p.EmployeeCode, dateInfo);
                    var property3 = p.GetType().GetProperty("data" + (i + 1));
                    property3.SetValue(p, stateStr);
                });
            }

            var count = 1;
            searchList.ForEach(p =>
            {
                p.xh = count.ToString();
                //p.EmployeeName = EmployeeInfoService.GetInstance().GetEmployeeNameByCode(p.EmployeeCode);
                count++;
            });


            var excellist = new List<ExcelData>();
            excellist.Add(DayOfWeek);
            excellist.Add(DayOfMonth);



            var designer = new WorkbookDesigner();
            designer.Open(Server.MapPath("~/TemplateFile/考勤模版2.xlsx"));
            var datatable = excellist.ToDataTable();
            datatable.TableName = "Table3";
            designer.SetDataSource(datatable);

            var datatable2 = searchList.ToDataTable();
            datatable2.TableName = "Table4";
            designer.SetDataSource(datatable2);

            var departmentInfo = DepartmentService.GetInstance().GetModelByDepartmentCode(where.DepartmentCode);
            var parentDepartmentCode =
                DepartmentService.GetInstance().GetModelByDepartmentCode(departmentInfo.ParentDepartmentCode).DepartmentName;

            designer.SetDataSource("BM", parentDepartmentCode);
            designer.SetDataSource("KS", departmentInfo.DepartmentName);
            designer.SetDataSource("RQ", date.ToString("yyyy年MM月"));

            designer.Process();

            designer.Save("_report.xlsx", SaveType.OpenInExcel, FileFormatType.Excel95, System.Web.HttpContext.Current.Response);

            Response.Flush();
            Response.Close();
            designer = null;
            Response.End();
        }

        public AttendanceEntity GetWhere()
        {
            var where = new AttendanceEntity();
            //where.PkId = RequestHelper.GetFormString("PkId");
            //where.AttendanceUploadRecordId = RequestHelper.GetFormString("AttendanceUploadRecordId");
            where.EmployeeCode = RequestHelper.GetString("EmployeeCode");
            where.Attr_StartDate = RequestHelper.GetDateTime("Attr_StartDate");
            where.Attr_EndDate = RequestHelper.GetDateTime("Attr_EndDate");
            where.DepartmentCode = string.Join(",", (DepartmentService.GetInstance().GetChiledArr(where.DepartmentCode, LoginUserInfo.UserDepartmentList.ToList(), LoginUserInfo.IsAdmin)));
            // RequestHelper.GetString("DepartmentCode");
            //where.DepartmentName = RequestHelper.GetFormString("DepartmentName");
            where.State = RequestHelper.GetString("State");
            return where;
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<AttendanceEntity> postData)
        {
            var addResult = AttendanceService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<AttendanceEntity>()
            {
                success = true,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit(AjaxRequest<AttendanceEntity> postData)
        {
            var newInfo = postData.RequestEntity;
            var orgInfo = AttendanceService.GetInstance().GetModelByPk(postData.RequestEntity.PkId);
            var mergInfo = Mapper.Map(newInfo, orgInfo);
            var updateResult = AttendanceService.GetInstance().Update(mergInfo);
            var result = new AjaxResponse<AttendanceEntity>()
            {
                success = updateResult.Item1,
                result = postData.RequestEntity,
                error = updateResult.Item1 ? null : new ErrorInfo(updateResult.Item2)
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = AttendanceService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<AttendanceEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }



    }
}





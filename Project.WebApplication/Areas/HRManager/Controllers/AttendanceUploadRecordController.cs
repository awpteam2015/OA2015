

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Aspose.Cells;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.ToolKit;
using Project.Infrastructure.FrameworkCore.ToolKit.JsonHandler;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;
using Project.Infrastructure.FrameworkCore.WebMvc.Controllers.Results;
using Project.Infrastructure.FrameworkCore.WebMvc.Models;
using Project.Model.HRManager;
using Project.Service.HRManager;
using Project.Service.PermissionManager;
using Project.Service.PermissionManager.Validate;
using Project.WebApplication.Controllers;

namespace Project.WebApplication.Areas.HRManager.Controllers
{
    public class AttendanceUploadRecordController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = AttendanceUploadRecordService.GetInstance().GetModelByPk(pkId);
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
            var where = new AttendanceUploadRecordEntity();
            //where.PkId = RequestHelper.GetFormString("PkId");
            where.DepartmentCode = RequestHelper.GetFormString("DepartmentCode");
            where.Date = RequestHelper.GetDateTime("Date");
            //where.Remark = RequestHelper.GetFormString("Remark");
            //where.CreatorUserCode = RequestHelper.GetFormString("CreatorUserCode");
            //where.CreatorUserName = RequestHelper.GetFormString("CreatorUserName");
            //where.CreateTime = RequestHelper.GetFormString("CreateTime");
            //where.FileUrl = RequestHelper.GetFormString("FileUrl");
            //where.IsDelete = RequestHelper.GetFormString("IsDelete");
            var searchList = AttendanceUploadRecordService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<AttendanceUploadRecordEntity> postData)
        {
            var path = Server.MapPath(postData.RequestEntity.FileUrl + "/" + postData.RequestEntity.FileName);

            Workbook workbook = new Workbook(path);
            Worksheet sheet = workbook.Worksheets[0];
            Cells cells = sheet.Cells;

            var dateStr = cells[2, 9].StringValue.ToString();

            var date = new DateTime(int.Parse(dateStr.Substring(0, 4)), int.Parse(dateStr.Substring(5, 2)), 1);

            for (int i = 5; i < cells.MaxDataRow + 1; i++)
            {

                for (int j = 3; j < 34; j++)
                {
                    if (!string.IsNullOrEmpty(cells[4, j].StringValue))
                    {
                        var row = new AttendanceEntity();
                        row.EmployeeCode = cells[i, 1].StringValue.Trim();
                        
                        var employeeInfo = EmployeeInfoService.GetInstance().GetEmployeeNameByCode2(row.EmployeeCode);
                        row.EmployeeName = employeeInfo.EmployeeName;
                        row.DepartmentCode = employeeInfo.DepartmentCode;
                        row.DepartmentName = employeeInfo.DepartmentName;
                        row.Date = date.AddDays(int.Parse(cells[4, j].StringValue) - 1);
                        row.State = cells[i, j].StringValue;
                        if (row.State=="")
                        {
                            row.State = "缺";
                        }
                        postData.RequestEntity.AttendanceList.Add(row);
                    }
                }

            }


            var addResult = AttendanceUploadRecordService.GetInstance().Add(postData.RequestEntity);

            var result = new AjaxResponse<string>()
            {
                success = addResult.Item1,
                result = addResult.Item2,
                error = addResult.Item1 ? null : new ErrorInfo(addResult.Item2)
            };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit(AjaxRequest<AttendanceUploadRecordEntity> postData)
        {
            var updateResult = AttendanceUploadRecordService.GetInstance().Update(postData.RequestEntity);
            var result = new AjaxResponse<AttendanceUploadRecordEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = AttendanceUploadRecordService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<AttendanceUploadRecordEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}





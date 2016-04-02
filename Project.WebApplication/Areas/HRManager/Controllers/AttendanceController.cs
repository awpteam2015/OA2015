

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
using Project.Model.ReportManager;
using Project.Service.HRManager;
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
            var searchList = AttendanceService.GetInstance().GetList(GetWhere());
            var designer = new WorkbookDesigner();
            designer.Open(Server.MapPath("~/TemplateFile/考勤模版2.xlsx"));
            var datatable = searchList.ToDataTable();
            datatable.TableName = "Table1";
            designer.SetDataSource(datatable);

            designer.SetDataSource("KS", "KS顶顶顶顶");
            designer.SetDataSource("RQ", "RQDDDDDDD");


            designer.Process();

            designer.Save("门店促销导出商品.xls");
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
            where.DepartmentCode = RequestHelper.GetString("DepartmentCode");
            //where.DepartmentName = RequestHelper.GetFormString("DepartmentName");
            where.State = RequestHelper.GetInt("State");
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





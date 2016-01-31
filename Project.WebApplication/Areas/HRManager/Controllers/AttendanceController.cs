

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.ToolKit;
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


            var searchList = AttendanceService.GetInstance().GetList(GetWhere());

            var jsonBuilder = new StringBuilder();
            jsonBuilder.AppendFormat(@"<table class='GridViewStyle' style='border-collapse:collapse;width:1000px;' cellspacing='0' rules='all' border='1'>
                <tr>
                    <th>工号</th>
                    <th>部门编号</th>
                    <th>部门名称</th>
                    <th>状态</th>
                    <th>考勤日期</th>
                   </tr>");
            searchList.ForEach(p =>
            {
                jsonBuilder.Append("<tr>");
                jsonBuilder.AppendFormat("<td>{0}</td>", p.EmployeeCode);
                jsonBuilder.AppendFormat("<td>{0}</td>", p.DepartmentCode);
                jsonBuilder.AppendFormat("<td>{0}</td>", p.DepartmentName);
                jsonBuilder.AppendFormat("<td>{0}</td>", p.Attr_State);
                jsonBuilder.AppendFormat("<td>{0}</td>", p.Date.SetDate());
                jsonBuilder.Append("</tr>");
            });
            jsonBuilder.Append("</table>");
            Response.Write(jsonBuilder.ToString());
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







using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.ToolKit.JsonHandler;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;
using Project.Model.HRManager;
using Project.Mvc.Controllers.Results;
using Project.Mvc.Models;
using Project.Service.HRManager;
using Project.WebApplication.Controllers;
using Project.Infrastructure.FrameworkCore.ToolKit;
using Project.Infrastructure.FrameworkCore.ToolKit.StringHandler;

namespace Project.WebApplication.Areas.HRManager.Controllers
{
    public class WorkExperienceController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = WorkExperienceService.GetInstance().GetModelByPk(pkId);
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
            var where = new WorkExperienceEntity();
            //where.PkId = RequestHelper.GetFormString("PkId");
            where.EmployeeID = RequestHelper.GetFormInt("EmployeeCode", 0);
            //where.DepartmentCode = RequestHelper.GetFormString("DepartmentCode");
            //where.WorkCompany = RequestHelper.GetFormString("WorkCompany");
            //where.Duties = RequestHelper.GetFormString("Duties");
            //where.BeginDate = RequestHelper.GetFormString("BeginDate");
            //where.EndDate = RequestHelper.GetFormString("EndDate");
            //where.WorkContent = RequestHelper.GetFormString("WorkContent");
            //where.LeaveReason = RequestHelper.GetFormString("LeaveReason");
            //where.Remark = RequestHelper.GetFormString("Remark");
            //where.CreatorUserCode = RequestHelper.GetFormString("CreatorUserCode");
            //where.CreatorUserName = RequestHelper.GetFormString("CreatorUserName");
            //where.CreateTime = RequestHelper.GetFormString("CreateTime");
            //where.LastModificationTime = RequestHelper.GetFormString("LastModificationTime");
            var searchList = WorkExperienceService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }

        public AbpJsonResult GetAllList()
        {
            var where = new WorkExperienceEntity();
            where.EmployeeID = TypeParse.StrToInt(RequestHelper.QueryString["EmployeeID"],0);
            //where.DepartmentCode = RequestHelper.GetFormString("DepartmentCode");
            var searchList = WorkExperienceService.GetInstance().GetList(where);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Count,
                rows = searchList
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }

        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<WorkExperienceEntity> postData)
        {
            var addResult = WorkExperienceService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<WorkExperienceEntity>()
               {
                   success = true,
                   result = postData.RequestEntity
               };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit(AjaxRequest<WorkExperienceEntity> postData)
        {
            var updateResult = WorkExperienceService.GetInstance().Update(postData.RequestEntity);
            var result = new AjaxResponse<WorkExperienceEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = WorkExperienceService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<WorkExperienceEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}





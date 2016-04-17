

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.ToolKit;
using Project.Infrastructure.FrameworkCore.ToolKit.JsonHandler;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;
using Project.Infrastructure.FrameworkCore.ToolKit.StringHandler;
using Project.Infrastructure.FrameworkCore.WebMvc.Controllers.Results;
using Project.Infrastructure.FrameworkCore.WebMvc.Models;
using Project.Model.HRManager;
using Project.Service.HRManager;
using Project.WebApplication.Controllers;

namespace Project.WebApplication.Areas.HRManager.Controllers
{
    public class ContinEducationController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = ContinEducationService.GetInstance().GetModelByPk(pkId);
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
            var where = new ContinEducationEntity();
			//where.PkId = RequestHelper.GetFormString("PkId");
			//where.EmployeeID = RequestHelper.GetFormString("EmployeeID");
			//where.EmployeeCode = RequestHelper.GetFormString("EmployeeCode");
			//where.DepartmentCode = RequestHelper.GetFormString("DepartmentCode");
			//where.CreditType = RequestHelper.GetFormString("CreditType");
			//where.CreditTypeName = RequestHelper.GetFormString("CreditTypeName");
			//where.Score = RequestHelper.GetFormString("Score");
			//where.GetTime = RequestHelper.GetFormString("GetTime");
			//where.CreatorUserCode = RequestHelper.GetFormString("CreatorUserCode");
			//where.CreattorUserName = RequestHelper.GetFormString("CreattorUserName");
			//where.CreationTime = RequestHelper.GetFormString("CreationTime");
			//where.LastModificationTime = RequestHelper.GetFormString("LastModificationTime");
			//where.LastModifierUserCode = RequestHelper.GetFormString("LastModifierUserCode");
            var searchList = ContinEducationService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }

        public AbpJsonResult GetAllList()
        {
            var where = new ContinEducationEntity();
            where.EmployeeID = TypeParse.StrToInt(RequestHelper.QueryString["EmployeeID"], 0);
            //where.DepartmentCode = RequestHelper.GetFormString("DepartmentCode");
            var searchList = ContinEducationService.GetInstance().GetList(where);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Count,
                rows = searchList
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<ContinEducationEntity> postData)
        {
            var addResult = ContinEducationService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<ContinEducationEntity>()
               {
                   success = true,
                   result = postData.RequestEntity
               };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit( AjaxRequest<ContinEducationEntity> postData)
        {
            var updateResult = ContinEducationService.GetInstance().Update(postData.RequestEntity);
            var result = new AjaxResponse<ContinEducationEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = ContinEducationService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<ContinEducationEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}





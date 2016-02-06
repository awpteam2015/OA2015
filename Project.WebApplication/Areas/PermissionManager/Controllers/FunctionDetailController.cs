

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.ToolKit.JsonHandler;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;
using Project.Infrastructure.FrameworkCore.WebMvc.Controllers.Results;
using Project.Infrastructure.FrameworkCore.WebMvc.Models;
using Project.Model.PermissionManager;
using Project.Service.PermissionManager;
using Project.WebApplication.Controllers;

namespace Project.WebApplication.Areas.PermissionManager.Controllers
{
    public class FunctionDetailController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = FunctionDetailService.GetInstance().GetModelByPk(pkId);
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
            var where = new FunctionDetailEntity();
			//where.PkId = RequestHelper.GetFormString("PkId");
			//where.FunctionDetailName = RequestHelper.GetFormString("FunctionDetailName");
			//where.FunctionDetailCode = RequestHelper.GetFormString("FunctionDetailCode");
			//where.FunctionId = RequestHelper.GetFormString("FunctionId");
			//where.Area = RequestHelper.GetFormString("Area");
			//where.Controller = RequestHelper.GetFormString("Controller");
			//where.Action = RequestHelper.GetFormString("Action");
			//where.CreatorUserCode = RequestHelper.GetFormString("CreatorUserCode");
			//where.CreationTime = RequestHelper.GetFormString("CreationTime");
			//where.LastModifierUserCode = RequestHelper.GetFormString("LastModifierUserCode");
			//where.LastModificationTime = RequestHelper.GetFormString("LastModificationTime");
            var searchList = FunctionDetailService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<FunctionDetailEntity> postData)
        {
            var addResult = FunctionDetailService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<FunctionDetailEntity>()
               {
                   success = true,
                   result = postData.RequestEntity
               };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit( AjaxRequest<FunctionDetailEntity> postData)
        {
            var updateResult = FunctionDetailService.GetInstance().Update(postData.RequestEntity);
            var result = new AjaxResponse<FunctionDetailEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = FunctionDetailService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<FunctionDetailEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}





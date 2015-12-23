

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.ToolKit.JsonHandler;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;
using Project.Model.PermissionManager;
using Project.Mvc.Controllers.Results;
using Project.Mvc.Models;
using Project.Service.PermissionManager;
using Project.WebApplication.Controllers;

namespace Project.WebApplication.Areas.PermissionManager.Controllers
{
    public class FunctionController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = FunctionService.GetInstance().GetModelByPk(pkId);
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
            var where = new FunctionEntity();
			//where.PkId = RequestHelper.GetFormString("PkId");
			//where.FunctionnName = RequestHelper.GetFormString("FunctionnName");
			//where.ModuleId = RequestHelper.GetFormString("ModuleId");
			//where.FunctionUrl = RequestHelper.GetFormString("FunctionUrl");
			//where.Area = RequestHelper.GetFormString("Area");
			//where.Controller = RequestHelper.GetFormString("Controller");
			//where.Action = RequestHelper.GetFormString("Action");
			//where.IsDisplayOnMenu = RequestHelper.GetFormString("IsDisplayOnMenu");
			//where.RankId = RequestHelper.GetFormString("RankId");
			//where.Remark = RequestHelper.GetFormString("Remark");
            var searchList = FunctionService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<FunctionEntity> postData)
        {
            var addResult = FunctionService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<FunctionEntity>()
               {
                   success = true,
                   result = postData.RequestEntity
               };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit( AjaxRequest<FunctionEntity> postData)
        {
            var updateResult = FunctionService.GetInstance().Update(postData.RequestEntity);
            var result = new AjaxResponse<FunctionEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = FunctionService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<FunctionEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}





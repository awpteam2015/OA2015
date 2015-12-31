

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.ToolKit;
using Project.Infrastructure.FrameworkCore.ToolKit.JsonHandler;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;
using Project.Model.PermissionManager;
using Project.Mvc.Controllers.Results;
using Project.Mvc.Models;
using Project.Service.PermissionManager;
using Project.WebApplication.Controllers;

namespace Project.WebApplication.Areas.PermissionManager.Controllers
{
    public class RoleController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = RoleService.GetInstance().GetModelByPk(pkId);
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
            var where = new RoleEntity();
			//where.PkId = RequestHelper.GetFormString("PkId");
			//where.RoleName = RequestHelper.GetFormString("RoleName");
			//where.Remark = RequestHelper.GetFormString("Remark");
            var searchList = RoleService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }

        public ActionResult RoleFunctionDetailList()
        {
            return View();
        }

        public AbpJsonResult GetRoleFunctionDetailList()
        {
            var pIndex = this.Request["page"].ConvertTo<int>();
            var pSize = this.Request["rows"].ConvertTo<int>();
            var where = new FunctionEntity();
            //where.PkId = RequestHelper.GetFormString("PkId");
            //where.RoleName = RequestHelper.GetFormString("RoleName");
            //where.Remark = RequestHelper.GetFormString("Remark");
            var searchList = FunctionService.GetInstance().GetList(where);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Count,
                rows = searchList
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }



        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<RoleEntity> postData)
        {
            var addResult = RoleService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<RoleEntity>()
               {
                   success = true,
                   result = postData.RequestEntity
               };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit( AjaxRequest<RoleEntity> postData)
        {
            var updateResult = RoleService.GetInstance().Update(postData.RequestEntity);
            var result = new AjaxResponse<RoleEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = RoleService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<RoleEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }


        [HttpPost]
        public AbpJsonResult SetRowFunction()
        {
            var t = this.Request["RolePkId"];
            var rolePkId = RequestHelper.GetInt("RolePkId");
            var functionPkId = RequestHelper.GetInt("FunctionPkId");
            var functionDetailPkId = RequestHelper.GetInt("FunctionDetailPkId");
            var isCheck = RequestHelper.GetInt("IsCheck")==1;
            var addResult = RoleService.GetInstance().SetRowFunction(rolePkId, functionPkId, functionDetailPkId,isCheck);
            var result = new AjaxResponse<RoleEntity>()
            {
                success = addResult
               // result = postData.RequestEntity
            };
            return new AbpJsonResult(result, null);
        }
    }
}





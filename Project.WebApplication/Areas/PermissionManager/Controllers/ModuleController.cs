

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.ToolKit;
using Project.Infrastructure.FrameworkCore.ToolKit.JsonHandler;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;
using Project.Infrastructure.FrameworkCore.WebMvc.Controllers.Results;
using Project.Infrastructure.FrameworkCore.WebMvc.Models;
using Project.Model.PermissionManager;
using Project.Service.PermissionManager;
using Project.WebApplication.Controllers;

namespace Project.WebApplication.Areas.PermissionManager.Controllers
{
    public class ModuleController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = ModuleService.GetInstance().GetModelByPk(pkId);
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
            var where = new ModuleEntity();
			//where.PkId = RequestHelper.GetFormString("PkId");
			where.ModuleName = RequestHelper.GetFormString("ModuleName");
			//where.ParentId = RequestHelper.GetFormString("ParentId");
			//where.ModuleLevel = RequestHelper.GetFormString("ModuleLevel");
			//where.RankId = RequestHelper.GetFormString("RankId");
			//where.Remark = RequestHelper.GetFormString("Remark");
            var searchList = ModuleService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }

        public AbpJsonResult GetListAll()
        {
            var where = new ModuleEntity();
            //where.PkId = RequestHelper.GetFormString("PkId");
            //where.ModuleName = RequestHelper.GetFormString("ModuleName");
            //where.ParentId = RequestHelper.GetFormString("ParentId");
            //where.ModuleLevel = RequestHelper.GetFormString("ModuleLevel");
            //where.RankId = RequestHelper.GetFormString("RankId");
            //where.Remark = RequestHelper.GetFormString("Remark");
            var searchList = ModuleService.GetInstance().GetList(where);
            searchList.ForEach(p =>
            {
                p.Att_RoleId = RequestHelper.GetInt("RoleId");
                p.Att_UserCode = RequestHelper.GetString("UserCode");
            });

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Count(),
                rows = searchList
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }

        public AbpJsonResult GetListAll_ForCombobox()
        {
            var where = new ModuleEntity();
            //where.PkId = RequestHelper.GetFormString("PkId");
            //where.ModuleName = RequestHelper.GetFormString("ModuleName");
            //where.ParentId = RequestHelper.GetFormString("ParentId");
            //where.ModuleLevel = RequestHelper.GetFormString("ModuleLevel");
            //where.RankId = RequestHelper.GetFormString("RankId");
            //where.Remark = RequestHelper.GetFormString("Remark");
            var searchList = ModuleService.GetInstance().GetList(where);
            return new AbpJsonResult(searchList, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<ModuleEntity> postData)
        {
            var addResult = ModuleService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<ModuleEntity>()
               {
                   success = true,
                   result = postData.RequestEntity
               };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit( AjaxRequest<ModuleEntity> postData)
        {
            var updateResult = ModuleService.GetInstance().Update(postData.RequestEntity);
            var result = new AjaxResponse<ModuleEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = ModuleService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<ModuleEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}





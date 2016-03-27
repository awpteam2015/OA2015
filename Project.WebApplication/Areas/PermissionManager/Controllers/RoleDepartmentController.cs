
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
    public class RoleDepartmentController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = RoleDepartmentService.GetInstance().GetModelByPk(pkId);
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
            var where = new RoleDepartmentEntity();
			//where.PkId = RequestHelper.GetFormString("PkId");
			//where.RoleId = RequestHelper.GetFormString("RoleId");
			//where.DepartId = RequestHelper.GetFormString("DepartId");
            var searchList = RoleDepartmentService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<RoleDepartmentEntity> postData)
        {
            var addResult = RoleDepartmentService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<RoleDepartmentEntity>()
               {
                   success = true,
                   result = postData.RequestEntity
               };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit( AjaxRequest<RoleDepartmentEntity> postData)
        {
            var updateResult = RoleDepartmentService.GetInstance().Update(postData.RequestEntity);
            var result = new AjaxResponse<RoleDepartmentEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = RoleDepartmentService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<RoleDepartmentEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}





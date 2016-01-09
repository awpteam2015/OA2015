

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
    public class DepartmentController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = DepartmentService.GetInstance().GetModelByPk(pkId);
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
            var where = new DepartmentEntity();
            where.DepartmentCode = RequestHelper.GetFormString("DepartmentCode");
            where.DepartmentName = RequestHelper.GetFormString("DepartmentName");
            var searchList = DepartmentService.GetInstance().GetList(where);
            var dataGridEntity = new DataGridTreeResponse<DepartmentEntity>(searchList.Count, searchList);
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }

        public AbpJsonResult GetList_Combotree()
        {
            var where = new DepartmentEntity();
            where.DepartmentCode = RequestHelper.GetFormString("DepartmentCode");
            where.DepartmentName = RequestHelper.GetFormString("DepartmentName");
            var searchList = DepartmentService.GetInstance().GetTreeList(where,true);

            return new AbpJsonResult(searchList, new NHibernateContractResolver(new[] { "children" }));
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<DepartmentEntity> postData)
        {
            var addResult = DepartmentService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<DepartmentEntity>()
            {
                success = addResult.Item1,
                result = postData.RequestEntity,
                error = addResult.Item1 ? null : new ErrorInfo(addResult.Item2) 
            };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit(AjaxRequest<DepartmentEntity> postData)
        {
            var updateResult = DepartmentService.GetInstance().Update(postData.RequestEntity);
            var result = new AjaxResponse<DepartmentEntity>()
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
            var deleteResult = DepartmentService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<DepartmentEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}





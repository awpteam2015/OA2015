

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
            var pIndex = this.Request["page"].ConvertTo<int>();
            var pSize = this.Request["rows"].ConvertTo<int>();
            var where = new DepartmentEntity();
            where.DepartmentCode = RequestHelper.GetFormString("DepartmentCode");
            where.DepartmentName = RequestHelper.GetFormString("DepartmentName");
            //where.ParentDepartmentCode = RequestHelper.GetFormString("ParentDepartmentCode");
            where.Remark = RequestHelper.GetFormString("Remark");


            var searchList = DepartmentService.GetInstance().GetList(where);
            var dataGridEntity = new DataGridTreeResponse<DepartmentEntity>(searchList.Count, searchList);

            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<DepartmentEntity> postData)
        {
            var addResult = DepartmentService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<DepartmentEntity>()
            {
                success = true,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit(AjaxRequest<DepartmentEntity> postData)
        {
            var updateResult = DepartmentService.GetInstance().Update(postData.RequestEntity);
            var result = new AjaxResponse<DepartmentEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
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





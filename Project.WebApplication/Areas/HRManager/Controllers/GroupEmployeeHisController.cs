

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

namespace Project.WebApplication.Areas.HRManager.Controllers
{
    public class GroupEmployeeHisController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = GroupEmployeeHisService.GetInstance().GetModelByPk(pkId);
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
            var where = new GroupEmployeeHisEntity();
			//where.PkId = RequestHelper.GetFormString("PkId");
			//where.GroupCode = RequestHelper.GetFormString("GroupCode");
			//where.EmployeeCode = RequestHelper.GetFormString("EmployeeCode");
			//where.CreatorUserCode = RequestHelper.GetFormString("CreatorUserCode");
			//where.CreatorUserName = RequestHelper.GetFormString("CreatorUserName");
			//where.CreateTime = RequestHelper.GetFormString("CreateTime");
            var searchList = GroupEmployeeHisService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<GroupEmployeeHisEntity> postData)
        {
            var addResult = GroupEmployeeHisService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<GroupEmployeeHisEntity>()
               {
                   success = true,
                   result = postData.RequestEntity
               };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit( AjaxRequest<GroupEmployeeHisEntity> postData)
        {
            var updateResult = GroupEmployeeHisService.GetInstance().Update(postData.RequestEntity);
            var result = new AjaxResponse<GroupEmployeeHisEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = GroupEmployeeHisService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<GroupEmployeeHisEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}





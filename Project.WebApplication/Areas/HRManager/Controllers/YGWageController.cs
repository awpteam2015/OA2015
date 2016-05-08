

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
    public class YGWageController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = YGWageService.GetInstance().GetModelByPk(pkId);
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
            var where = new YGWageEntity();
			//where.PkId = RequestHelper.GetFormString("PkId");
			where.EmployeeID = RequestHelper.GetFormInt("EmployeeID",0);
			//where.GWGZ = RequestHelper.GetFormString("GWGZ");
			//where.XZGZ = RequestHelper.GetFormString("XZGZ");
			//where.CreatorUserCode = RequestHelper.GetFormString("CreatorUserCode");
			//where.CreattorUserName = RequestHelper.GetFormString("CreattorUserName");
			//where.CreationTime = RequestHelper.GetFormString("CreationTime");
			//where.LastModificationTime = RequestHelper.GetFormString("LastModificationTime");
			//where.LastModifierUserCode = RequestHelper.GetFormString("LastModifierUserCode");
            var searchList = YGWageService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }
        public AbpJsonResult GetAllList()
        {
            var where = new YGWageEntity();
            where.EmployeeID = TypeParse.StrToInt(RequestHelper.QueryString["EmployeeID"], 0);
            var searchList = YGWageService.GetInstance().GetList(where);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Count,
                rows = searchList
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }

        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<YGWageEntity> postData)
        {
            var addResult = YGWageService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<YGWageEntity>()
               {
                   success = true,
                   result = postData.RequestEntity
               };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit( AjaxRequest<YGWageEntity> postData)
        {
            var updateResult = YGWageService.GetInstance().Update(postData.RequestEntity);
            var result = new AjaxResponse<YGWageEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = YGWageService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<YGWageEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}





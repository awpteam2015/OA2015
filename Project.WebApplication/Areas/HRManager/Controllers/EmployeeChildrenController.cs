

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
    public class EmployeeChildrenController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = EmployeeChildrenService.GetInstance().GetModelByPk(pkId);
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
            var where = new EmployeeChildrenEntity();
			//where.PkId = RequestHelper.GetFormString("PkId");
			//where.EmployeeCode = RequestHelper.GetFormString("EmployeeCode");
			//where.ChildrenName = RequestHelper.GetFormString("ChildrenName");
			//where.Sex = RequestHelper.GetFormString("Sex");
			//where.Relation = RequestHelper.GetFormString("Relation");
			//where.Certificate = RequestHelper.GetFormString("Certificate");
			//where.JoinDate = RequestHelper.GetFormString("JoinDate");
			//where.Hospital = RequestHelper.GetFormString("Hospital");
			//where.Remark = RequestHelper.GetFormString("Remark");
			//where.CreatorUserCode = RequestHelper.GetFormString("CreatorUserCode");
			//where.CreatorUserName = RequestHelper.GetFormString("CreatorUserName");
			//where.CreateTime = RequestHelper.GetFormString("CreateTime");
			//where.LastModificationTime = RequestHelper.GetFormString("LastModificationTime");
            var searchList = EmployeeChildrenService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<EmployeeChildrenEntity> postData)
        {
            var addResult = EmployeeChildrenService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<EmployeeChildrenEntity>()
               {
                   success = true,
                   result = postData.RequestEntity
               };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit( AjaxRequest<EmployeeChildrenEntity> postData)
        {
            var updateResult = EmployeeChildrenService.GetInstance().Update(postData.RequestEntity);
            var result = new AjaxResponse<EmployeeChildrenEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = EmployeeChildrenService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<EmployeeChildrenEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}





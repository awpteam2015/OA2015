

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.ToolKit.JsonHandler;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;
using Project.Model.HRManager;
using Project.Infrastructure.FrameworkCore.WebMvc.Controllers.Results;
using Project.Infrastructure.FrameworkCore.WebMvc.Models;
using Project.Service.HRManager;
using Project.WebApplication.Controllers;
using AutoMapper;

namespace Project.WebApplication.Areas.HRManager.Controllers
{
    public class ProfessionController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = ProfessionService.GetInstance().GetModelByPk(pkId);
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
            var where = new ProfessionEntity();
			//where.PkId = RequestHelper.GetFormString("PkId");
			//where.EmployeeID = RequestHelper.GetFormString("EmployeeID");
			//where.EmployeeCode = RequestHelper.GetFormString("EmployeeCode");
			//where.DepartmentCode = RequestHelper.GetFormString("DepartmentCode");
			//where.Title = RequestHelper.GetFormString("Title");
			//where.TypeName = RequestHelper.GetFormString("TypeName");
			//where.RangeName = RequestHelper.GetFormString("RangeName");
			//where.GetDate = RequestHelper.GetFormString("GetDate");
			//where.CerNo = RequestHelper.GetFormString("CerNo");
			//where.CreatorUserCode = RequestHelper.GetFormString("CreatorUserCode");
			//where.CreatorUserName = RequestHelper.GetFormString("CreatorUserName");
			//where.CreationTime = RequestHelper.GetFormString("CreationTime");
			//where.LastModificationTime = RequestHelper.GetFormString("LastModificationTime");
			//where.LastModifierUserCode = RequestHelper.GetFormString("LastModifierUserCode");
            var searchList = ProfessionService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<ProfessionEntity> postData)
        {
            var addResult = ProfessionService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<ProfessionEntity>()
               {
                   success = true,
                   result = postData.RequestEntity
               };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit( AjaxRequest<ProfessionEntity> postData)
        {
            var newInfo = postData.RequestEntity;
            var orgInfo = ProfessionService.GetInstance().GetModelByPk(postData.RequestEntity.PkId);
            var mergInfo = Mapper.Map(newInfo, orgInfo);
            var updateResult = ProfessionService.GetInstance().Update(mergInfo);
            
            var result = new AjaxResponse<ProfessionEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = ProfessionService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<ProfessionEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}





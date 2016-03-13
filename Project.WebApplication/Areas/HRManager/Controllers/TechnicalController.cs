

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.ToolKit.JsonHandler;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;
using Project.Model.HRManager;
using Project.Service.HRManager;
using Project.WebApplication.Controllers;
using Project.Infrastructure.FrameworkCore.ToolKit;
using Project.Infrastructure.FrameworkCore.ToolKit.StringHandler;
using Project.Infrastructure.FrameworkCore.WebMvc.Controllers.Results;
using Project.Infrastructure.FrameworkCore.WebMvc.Models;
using AutoMapper;

namespace Project.WebApplication.Areas.HRManager.Controllers
{
    public class TechnicalController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = TechnicalService.GetInstance().GetModelByPk(pkId);
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
            var where = new TechnicalEntity();
			//where.PkId = RequestHelper.GetFormString("PkId");
			//where.EmployeeCode = RequestHelper.GetFormString("EmployeeCode");
			//where.DepartmentCode = RequestHelper.GetFormString("DepartmentCode");
			//where.Title = RequestHelper.GetFormString("Title");
			//where.LevNum = RequestHelper.GetFormString("LevNum");
			//where.GetDate = RequestHelper.GetFormString("GetDate");
			//where.CerNo = RequestHelper.GetFormString("CerNo");
			//where.CreatorUserCode = RequestHelper.GetFormString("CreatorUserCode");
			//where.CreatorUserName = RequestHelper.GetFormString("CreatorUserName");
			//where.CreationTime = RequestHelper.GetFormString("CreationTime");
			//where.LastModificationTime = RequestHelper.GetFormString("LastModificationTime");
			//where.LastModifierUserCode = RequestHelper.GetFormString("LastModifierUserCode");
            var searchList = TechnicalService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }
        public AbpJsonResult GetAllList()
        {
            var where = new TechnicalEntity();
            where.EmployeeID = TypeParse.StrToInt(RequestHelper.QueryString["EmployeeID"], 0);
            //where.DepartmentCode = RequestHelper.GetFormString("DepartmentCode");
            var searchList = TechnicalService.GetInstance().GetList(where);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Count,
                rows = searchList
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }

        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<TechnicalEntity> postData)
        {

            postData.RequestEntity.EmployeeID = postData.RequestEntity.PkId;
            postData.RequestEntity.PkId = 0;
            var addResult = TechnicalService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<TechnicalEntity>()
               {
                   success = true,
                   result = postData.RequestEntity
               };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit( AjaxRequest<TechnicalEntity> postData)
        {
            var newInfo = postData.RequestEntity;
            var orgInfo = TechnicalService.GetInstance().GetModelByPk(postData.RequestEntity.PkId);
            var mergInfo = Mapper.Map(newInfo, orgInfo);
            var updateResult = TechnicalService.GetInstance().Update(mergInfo);
            
            var result = new AjaxResponse<TechnicalEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = TechnicalService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<TechnicalEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}





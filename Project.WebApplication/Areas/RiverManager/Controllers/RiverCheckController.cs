

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.ToolKit;
using Project.Infrastructure.FrameworkCore.ToolKit.JsonHandler;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;
using Project.Infrastructure.FrameworkCore.WebMvc.Controllers.Results;
using Project.Infrastructure.FrameworkCore.WebMvc.Models;
using Project.Model.RiverManager;
using Project.Service.RiverManager;
using Project.WebApplication.Controllers;

namespace Project.WebApplication.Areas.RiverManager.Controllers
{
    public class RiverCheckController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = RiverCheckService.GetInstance().GetModelByPk(pkId);
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
            var where = new RiverCheckEntity();
            //where.PkId = RequestHelper.GetFormString("PkId");
            //where.RiverId = RequestHelper.GetFormString("RiverId");
            where.RiverName = RequestHelper.GetFormString("RiverName");
            where.UserName = RequestHelper.GetFormString("UserName");
            where.UserCode = RequestHelper.GetFormString("UserCode");
            //where.Coords = RequestHelper.GetFormString("Coords");
            //where.IsActive = RequestHelper.GetFormString("IsActive");
            //where.CreatorUserCode = RequestHelper.GetFormString("CreatorUserCode");
            //where.CreationTime = RequestHelper.GetFormString("CreationTime");
            //where.LastModifierUserCode = RequestHelper.GetFormString("LastModifierUserCode");
            //where.LastModificationTime = RequestHelper.GetFormString("LastModificationTime");
            //where.Remark = RequestHelper.GetFormString("Remark");
            var searchList = RiverCheckService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<RiverCheckEntity> postData)
        {
            postData.RequestEntity.RiverName =
                RiverService.GetInstance().GetModelByPk(postData.RequestEntity.RiverId.GetValueOrDefault()).RiverName;

            var addResult = RiverCheckService.GetInstance().Add(postData.RequestEntity);


            var result = new AjaxResponse<RiverCheckEntity>()
               {
                   success = true,
                   result = postData.RequestEntity
               };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit( AjaxRequest<RiverCheckEntity> postData)
        {
            var newInfo = postData.RequestEntity;
            var orgInfo = RiverCheckService.GetInstance().GetModelByPk(postData.RequestEntity.PkId);
            var mergInfo = Mapper.Map(newInfo, orgInfo);
            var updateResult = RiverCheckService.GetInstance().Update(mergInfo);
            
            var result = new AjaxResponse<RiverCheckEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = RiverCheckService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<RiverCheckEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}





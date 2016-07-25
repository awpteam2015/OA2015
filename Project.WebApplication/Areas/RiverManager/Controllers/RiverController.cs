

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
    public class RiverController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = RiverService.GetInstance().GetModelByPk(pkId);
                ViewBag.BindEntity = JsonHelper.JsonSerializer(entity);
            }
            return View();
        }
        public ActionResult TdMap()
        {
           
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
            var where = new RiverEntity();
            //where.PkId = RequestHelper.GetFormString("PkId");
            where.RiverName = RequestHelper.GetFormString("RiverName");
            where.RiverRank = RequestHelper.GetFormString("RiverRank");
            //where.RiverArea = RequestHelper.GetFormString("RiverArea");
            //where.RiverLength = RequestHelper.GetFormString("RiverLength");
            //where.RiverCrossArea = RequestHelper.GetFormString("RiverCrossArea");
            //where.Coords = RequestHelper.GetFormString("Coords");
            //where.IsActive = RequestHelper.GetFormString("IsActive");
            //where.CreatorUserCode = RequestHelper.GetFormString("CreatorUserCode");
            //where.CreationTime = RequestHelper.GetFormString("CreationTime");
            //where.LastModifierUserCode = RequestHelper.GetFormString("LastModifierUserCode");
            //where.LastModificationTime = RequestHelper.GetFormString("LastModificationTime");
            //where.Remark = RequestHelper.GetFormString("Remark");
            var searchList = RiverService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }



        public AbpJsonResult GetListNoPage()
        {
         
            var where = new RiverEntity();
            //where.PkId = RequestHelper.GetFormString("PkId");
            where.RiverName = RequestHelper.GetFormString("RiverName");
            where.RiverRank = RequestHelper.GetFormString("RiverRank");
            //where.RiverArea = RequestHelper.GetFormString("RiverArea");
            //where.RiverLength = RequestHelper.GetFormString("RiverLength");
            //where.RiverCrossArea = RequestHelper.GetFormString("RiverCrossArea");
            //where.Coords = RequestHelper.GetFormString("Coords");
            //where.IsActive = RequestHelper.GetFormString("IsActive");
            //where.CreatorUserCode = RequestHelper.GetFormString("CreatorUserCode");
            //where.CreationTime = RequestHelper.GetFormString("CreationTime");
            //where.LastModifierUserCode = RequestHelper.GetFormString("LastModifierUserCode");
            //where.LastModificationTime = RequestHelper.GetFormString("LastModificationTime");
            //where.Remark = RequestHelper.GetFormString("Remark");
            var searchList = RiverService.GetInstance().GetList(where);

          
            return new AbpJsonResult(searchList, new NHibernateContractResolver());
        }

        public AbpJsonResult GetRiverDetail(int PkId)
        {      
            var riverInfo = RiverService.GetInstance().GetModelByPk(PkId);
            return new AbpJsonResult(riverInfo, new NHibernateContractResolver(new string[] { "RiverOwerList" }));
        }



        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<RiverEntity> postData)
        {
            var addResult = RiverService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<RiverEntity>()
            {
                success = true,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit(AjaxRequest<RiverEntity> postData)
        {
            var newInfo = postData.RequestEntity;
            var orgInfo = RiverService.GetInstance().GetModelByPk(postData.RequestEntity.PkId);
            var mergInfo = Mapper.Map(newInfo, orgInfo);
            var updateResult = RiverService.GetInstance().Update(mergInfo);

            var result = new AjaxResponse<RiverEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = RiverService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<RiverEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}





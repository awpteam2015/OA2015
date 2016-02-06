

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
using Project.Infrastructure.FrameworkCore.WebMvc.Controllers.Results;
using Project.Infrastructure.FrameworkCore.WebMvc.Models;

namespace Project.WebApplication.Areas.HRManager.Controllers
{
    public class DictionaryController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = DictionaryService.GetInstance().GetModelByPk(pkId);
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
            var where = new DictionaryEntity();
            //where.PkId = RequestHelper.GetFormString("PkId");
            //where.KeyCode = RequestHelper.GetFormString("KeyCode");
            //where.ParentKeyCode = RequestHelper.GetFormString("ParentKeyCode");
            //where.KeyName = RequestHelper.GetFormString("KeyName");
            //where.KeyValue = RequestHelper.GetFormString("KeyValue");
            //var searchList = DictionaryService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            //var dataGridEntity = new DataGridResponse()
            //{
            //    total = searchList.Item2,
            //    rows = searchList.Item1
            //};
            //return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
            var searchList = DictionaryService.GetInstance().GetList(where);
            var dataGridEntity = new DataGridTreeResponse<DictionaryEntity>(searchList.Count, searchList);
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }

        public AbpJsonResult GetList_Combotree()
        {
            var where = new DictionaryEntity();
            where.KeyCode = RequestHelper.GetFormString("KeyCode");
            where.KeyName = RequestHelper.GetFormString("KeyName");
            var searchList = DictionaryService.GetInstance().GetTreeList(where, true);

            return new AbpJsonResult(searchList, new NHibernateContractResolver(new[] { "children" }));
        }

        public AbpJsonResult GetListByCode()
        {
            var where = new DictionaryEntity();
            //where.PkId = RequestHelper.GetFormString("PkId");
            //where.KeyCode = RequestHelper.GetFormString("KeyCode");
            where.ParentKeyCode = RequestHelper.GetQueryString("ParentKeyCode");
            //where.KeyName = RequestHelper.GetFormString("KeyName");
            //where.KeyValue = RequestHelper.GetFormString("KeyValue");
            var searchList = DictionaryService.GetInstance().GetList(where);
            if (!string.IsNullOrEmpty(RequestHelper.GetQueryString("AllFlag")))
            {
                searchList.Insert(0, new DictionaryEntity() { KeyName = "全部", KeyValue = "" });
            }
            return new AbpJsonResult(searchList, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<DictionaryEntity> postData)
        {
            var addResult = DictionaryService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<DictionaryEntity>()
            {
                success = true,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit(AjaxRequest<DictionaryEntity> postData)
        {
            var updateResult = DictionaryService.GetInstance().Update(postData.RequestEntity);
            var result = new AjaxResponse<DictionaryEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = DictionaryService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<DictionaryEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}





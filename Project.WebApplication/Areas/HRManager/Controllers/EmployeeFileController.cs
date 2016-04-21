

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.ToolKit;
using Project.Infrastructure.FrameworkCore.ToolKit.JsonHandler;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;
using Project.Infrastructure.FrameworkCore.WebMvc.Controllers.Results;
using Project.Infrastructure.FrameworkCore.WebMvc.Models;
using Project.Model.HRManager;
using Project.Service.HRManager;
using Project.WebApplication.Controllers;

namespace Project.WebApplication.Areas.HRManager.Controllers
{
    public class EmployeeFileController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = EmployeeFileService.GetInstance().GetModelByPk(pkId);
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
            var where = new EmployeeFileEntity();
            //where.PkId = RequestHelper.GetFormString("PkId");
            //where.EmployeeID = RequestHelper.GetFormString("EmployeeID");
            //where.FName = RequestHelper.GetFormString("FName");
            //where.FileUrl = RequestHelper.GetFormString("FileUrl");
            //where.CreatorUserCode = RequestHelper.GetFormString("CreatorUserCode");
            //where.CreattorUserName = RequestHelper.GetFormString("CreattorUserName");
            //where.CreationTime = RequestHelper.GetFormString("CreationTime");
            //where.LastModificationTime = RequestHelper.GetFormString("LastModificationTime");
            //where.LastModifierUserCode = RequestHelper.GetFormString("LastModifierUserCode");
            var searchList = EmployeeFileService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        public AbpJsonResult GetAllList()
        {
           
            var where = new EmployeeFileEntity();
            //where.PkId = RequestHelper.GetFormString("PkId");
            where.EmployeeID = RequestHelper.GetInt("EmployeeID", -1);
            //where.FName = RequestHelper.GetFormString("FName");
            //where.FileUrl = RequestHelper.GetFormString("FileUrl");
            //where.CreatorUserCode = RequestHelper.GetFormString("CreatorUserCode");
            //where.CreattorUserName = RequestHelper.GetFormString("CreattorUserName");
            //where.CreationTime = RequestHelper.GetFormString("CreationTime");
            //where.LastModificationTime = RequestHelper.GetFormString("LastModificationTime");
            //where.LastModifierUserCode = RequestHelper.GetFormString("LastModifierUserCode");
            var searchList = EmployeeFileService.GetInstance().GetList(where);

            
            return new AbpJsonResult(searchList, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<EmployeeFileEntity> postData)
        {
            var addResult = EmployeeFileService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<EmployeeFileEntity>()
            {
                success = true,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit(AjaxRequest<EmployeeFileEntity> postData)
        {
            var updateResult = EmployeeFileService.GetInstance().Update(postData.RequestEntity);
            var result = new AjaxResponse<EmployeeFileEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = EmployeeFileService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<EmployeeFileEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}





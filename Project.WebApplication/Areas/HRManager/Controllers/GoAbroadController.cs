

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
using Project.Service.PermissionManager;

namespace Project.WebApplication.Areas.HRManager.Controllers
{
    public class GoAbroadController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = GoAbroadService.GetInstance().GetModelByPk(pkId);
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
            var where = new GoAbroadEntity();
            //where.PkId = RequestHelper.GetFormString("PkId");
            where.EmployeeCode = RequestHelper.GetFormString("EmployeeCode");
            where.DepartmentCode = RequestHelper.GetFormString("DepartmentCode");
            where.DepartmentCode = string.Join(",", (DepartmentService.GetInstance().GetChiledArr(where.DepartmentCode)));
            where.EmployeeName = RequestHelper.GetFormString("EmployeeName");
            //where.Country = RequestHelper.GetFormString("Country");
            //where.BeginDate = RequestHelper.GetFormString("BeginDate");
            //where.EndDate = RequestHelper.GetFormString("EndDate");
            //where.DaySum = RequestHelper.GetFormString("DaySum");
            //where.Reason = RequestHelper.GetFormString("Reason");
            //where.Remark = RequestHelper.GetFormString("Remark");
            //where.CreatorUserCode = RequestHelper.GetFormString("CreatorUserCode");
            //where.CreatorUserName = RequestHelper.GetFormString("CreatorUserName");
            where.CreateTime = RequestHelper.GetFormDateTime("CreateTime");
            where.CreateTimeEnd = RequestHelper.GetFormDateTime("CreateTimeEnd");
            //where.LastModificationTime = RequestHelper.GetFormString("LastModificationTime");
            var searchList = GoAbroadService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<GoAbroadEntity> postData)
        {
            postData.RequestEntity.CreateTime = DateTime.Now;
            postData.RequestEntity.CreatorUserCode = LoginUserInfo.UserCode;
            postData.RequestEntity.CreatorUserName = LoginUserInfo.UserName;
            postData.RequestEntity.Reason = Base64Helper.DecodeBase64(postData.RequestEntity.Reason);
            postData.RequestEntity.Remark = Base64Helper.DecodeBase64(postData.RequestEntity.Remark);
            var addResult = GoAbroadService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<GoAbroadEntity>()
               {
                   success = true,
                   result = postData.RequestEntity
               };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit(AjaxRequest<GoAbroadEntity> postData)
        {
            postData.RequestEntity.LastModificationTime = DateTime.Now;
            postData.RequestEntity.Reason = Base64Helper.DecodeBase64(postData.RequestEntity.Reason);
            postData.RequestEntity.Remark = Base64Helper.DecodeBase64(postData.RequestEntity.Remark);
            
            var updateResult = GoAbroadService.GetInstance().Update(postData.RequestEntity);
            var result = new AjaxResponse<GoAbroadEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = GoAbroadService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<GoAbroadEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}





﻿

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
    public class AttendanceController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = AttendanceService.GetInstance().GetModelByPk(pkId);
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
            var where = new AttendanceEntity();
			//where.PkId = RequestHelper.GetFormString("PkId");
			//where.EmployeeCode = RequestHelper.GetFormString("EmployeeCode");
			//where.DepartmentCode = RequestHelper.GetFormString("DepartmentCode");
			//where.State = RequestHelper.GetFormString("State");
			//where.Date = RequestHelper.GetFormString("Date");
			//where.Remark = RequestHelper.GetFormString("Remark");
			//where.CreatorUserCode = RequestHelper.GetFormString("CreatorUserCode");
			//where.CreatorUserName = RequestHelper.GetFormString("CreatorUserName");
			//where.CreateTime = RequestHelper.GetFormString("CreateTime");
            var searchList = AttendanceService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<AttendanceEntity> postData)
        {
            var addResult = AttendanceService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<AttendanceEntity>()
               {
                   success = true,
                   result = postData.RequestEntity
               };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit( AjaxRequest<AttendanceEntity> postData)
        {
            var updateResult = AttendanceService.GetInstance().Update(postData.RequestEntity);
            var result = new AjaxResponse<AttendanceEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = AttendanceService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<AttendanceEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}




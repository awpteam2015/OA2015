

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
using Project.Infrastructure.FrameworkCore.ToolKit;

namespace Project.WebApplication.Areas.HRManager.Controllers
{
    public class EmployeeInfoHisController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = EmployeeInfoHisService.GetInstance().GetModelByPk(pkId);
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
            var where = new EmployeeInfoHisEntity();
            //where.PkId = RequestHelper.GetFormString("PkId");
            //where.EmployeeID = RequestHelper.GetFormString("EmployeeID");
            where.EmployeeCode = RequestHelper.GetQueryString("EmployeeCode");
            //where.EmployeeName = RequestHelper.GetFormString("EmployeeName");
            //where.DepartmentCode = RequestHelper.GetFormString("DepartmentCode");
            //where.JobName = RequestHelper.GetFormString("JobName");
            //where.PayCode = RequestHelper.GetFormString("PayCode");
            //where.Sex = RequestHelper.GetFormString("Sex");
            //where.CertNo = RequestHelper.GetFormString("CertNo");
            //where.Birthday = RequestHelper.GetFormString("Birthday");
            //where.TechnicalTitleName = RequestHelper.GetFormString("TechnicalTitleName");
            //where.TechnicalTitle = RequestHelper.GetFormString("TechnicalTitle");
            //where.DutiesName = RequestHelper.GetFormString("DutiesName");
            //where.Duties = RequestHelper.GetFormString("Duties");
            //where.WorkingYears = RequestHelper.GetFormString("WorkingYears");
            //where.WorkState = RequestHelper.GetFormString("WorkState");
            //where.EmployeeType = RequestHelper.GetFormString("EmployeeType");
            //where.EmployeeTypeName = RequestHelper.GetFormString("EmployeeTypeName");
            //where.HomeAddress = RequestHelper.GetFormString("HomeAddress");
            //where.MobileNO = RequestHelper.GetFormString("MobileNO");
            //where.ImageUrl = RequestHelper.GetFormString("ImageUrl");
            //where.Sort = RequestHelper.GetFormString("Sort");
            //where.State = RequestHelper.GetFormString("State");
            //where.Remark = RequestHelper.GetFormString("Remark");
            //where.CreatorUserCode = RequestHelper.GetFormString("CreatorUserCode");
            //where.CreatorUserName = RequestHelper.GetFormString("CreatorUserName");
            //where.CreateTime = RequestHelper.GetFormString("CreateTime");
            //where.LastModificationTime = RequestHelper.GetFormString("LastModificationTime");
            //where.WorkStateName = RequestHelper.GetFormString("WorkStateName");
            var searchList = EmployeeInfoHisService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<EmployeeInfoHisEntity> postData)
        {
            var addResult = EmployeeInfoHisService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<EmployeeInfoHisEntity>()
            {
                success = true,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit(AjaxRequest<EmployeeInfoHisEntity> postData)
        {
            var newInfo = postData.RequestEntity;
            var orgInfo = EmployeeInfoHisService.GetInstance().GetModelByPk(postData.RequestEntity.PkId);
            var mergInfo = Mapper.Map(newInfo, orgInfo);
            var updateResult = EmployeeInfoHisService.GetInstance().Update(mergInfo);

            var result = new AjaxResponse<EmployeeInfoHisEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = EmployeeInfoHisService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<EmployeeInfoHisEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}





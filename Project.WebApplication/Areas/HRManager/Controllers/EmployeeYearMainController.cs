using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.ToolKit.JsonHandler;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;
using Project.Infrastructure.FrameworkCore.WebMvc.Controllers.Results;
using Project.Infrastructure.FrameworkCore.WebMvc.Models;
using Project.Model.HRManager;
using Project.Service.HRManager;
using Project.WebApplication.Controllers;
using AutoMapper;
using Project.Infrastructure.FrameworkCore.ToolKit;
using Project.Service.PermissionManager;

namespace Project.WebApplication.Areas.HRManager.Controllers
{
    public class EmployeeYearMainController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = EmployeeYearMainService.GetInstance().GetModelByPk(pkId);
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
            var where = new EmployeeYearMainEntity();
            //where.PkId = RequestHelper.GetFormString("PkId");
            where.DepartmentCode = RequestHelper.GetFormString("DepartmentCode");
            where.DepartmentCode = string.Join(",", (DepartmentService.GetInstance().GetChiledArr(where.DepartmentCode)));
            where.EmployeeCode = RequestHelper.GetFormString("EmployeeCode");
            //where.LeftCount = RequestHelper.GetFormString("LeftCount");
            //where.Remark = RequestHelper.GetFormString("Remark");
            //where.CreatorUserCode = RequestHelper.GetFormString("CreatorUserCode");
            //where.CreatorUserName = RequestHelper.GetFormString("CreatorUserName");
            //where.CreateTime = RequestHelper.GetFormString("CreateTime");
            //where.LastModificationTime = RequestHelper.GetFormString("LastModificationTime");
            var searchList = EmployeeYearMainService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver(new string[] { "DepartmentEntity" }, new string[] { "Remark" }));
        }


        [HttpPost]
        public AbpJsonResult<string> Add(AjaxRequest<EmployeeYearMainEntity> postData)
        {
            postData.RequestEntity.CreatorUserName = LoginUserInfo.UserName;
            var addResult = EmployeeYearMainService.GetInstance().Add(postData.RequestEntity);

            return new AbpJsonResult<string>(addResult);
        }


        [HttpPost]
        public AbpJsonResult<string> Edit(AjaxRequest<EmployeeYearMainEntity> postData)
        {
            postData.RequestEntity.LastModificationTime = DateTime.Now;

            var newInfo = postData.RequestEntity;
            var orgInfo = EmployeeYearMainService.GetInstance().GetModelByPk(postData.RequestEntity.PkId);
            var mergInfo = Mapper.Map(newInfo, orgInfo);
            var updateResult = EmployeeYearMainService.GetInstance().Update(mergInfo);
            return new AbpJsonResult<string>(updateResult);
            //var updateResult = EmployeeYearMainService.GetInstance().Update(postData.RequestEntity);
            //var result = new AjaxResponse<EmployeeYearMainEntity>()
            //{
            //    success = updateResult,
            //    result = postData.RequestEntity
            //};
            //return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {

            var deleteResult = EmployeeYearMainService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<EmployeeYearMainEntity>()
            {
                success = deleteResult.Item1,
                error = deleteResult.Item1 ? null : new ErrorInfo(deleteResult.Item2)
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}





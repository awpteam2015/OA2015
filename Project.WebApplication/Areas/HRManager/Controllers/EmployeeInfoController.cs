

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.ToolKit.JsonHandler;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;
using Project.Infrastructure.FrameworkCore.ToolKit.StringHandler;
using Project.Model.HRManager;
using Project.Service.HRManager;
using Project.WebApplication.Controllers;
using Project.Infrastructure.FrameworkCore.ToolKit;
using Project.Infrastructure.FrameworkCore.WebMvc.Controllers.Results;
using Project.Infrastructure.FrameworkCore.WebMvc.Models;
using Project.Service.PermissionManager;

namespace Project.WebApplication.Areas.HRManager.Controllers
{
    public class EmployeeInfoController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = EmployeeInfoService.GetInstance().GetModelByPk(pkId);
                ViewBag.BindEntity = JsonHelper.JsonSerializer(entity);
            }
            else
            {
                var maxCode = ((TypeParse.StrToInt(EmployeeInfoService.GetInstance().GetMaxEmployeeCode(), 0) + 1) + "").PadLeft(8, '0');
                ViewBag.BindEntity = JsonHelper.JsonSerializer(new EmployeeInfoEntity()
                {
                    EmployeeCode = maxCode,
                    Duties = "0",
                    EmployeeType = "0",
                    Sex = 0,
                    WorkingYears = 1,
                    WorkState = "1",
                    State = 1
                });
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
            var where = new EmployeeInfoEntity();
            //where.PkId = RequestHelper.GetFormString("PkId");
            where.EmployeeCode = RequestHelper.GetFormString("EmployeeCode");
            where.EmployeeName = RequestHelper.GetFormString("EmployeeName");
            where.DepartmentCode = RequestHelper.GetFormString("DepartmentCode");
            where.DepartmentCode = string.Join(",", (DepartmentService.GetInstance().GetChiledArr(where.DepartmentCode)));
            where.JobName = RequestHelper.GetFormString("JobName");
            //where.PayCode = RequestHelper.GetFormString("PayCode");
            //where.Sex = RequestHelper.GetFormString("Sex");
            //where.CertNo = RequestHelper.GetFormString("CertNo");
            //where.Birthday = RequestHelper.GetFormString("Birthday");
            //where.TechnicalTitle = RequestHelper.GetFormString("TechnicalTitle");
            //where.Duties = RequestHelper.GetFormString("Duties");
            where.WorkState = RequestHelper.GetFormString("WorkState");
            where.EmployeeType = RequestHelper.GetFormString("EmployeeType");
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
            var searchList = EmployeeInfoService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }

        public AbpJsonResult GetAllList()
        {
            var where = new EmployeeInfoEntity();
            where.DepartmentCode = RequestHelper.QueryString["DepartmentCode"];
            where.DepartmentCode = string.Join(",", (DepartmentService.GetInstance().GetChiledArr(where.DepartmentCode)));
            //where.DepartmentCode = RequestHelper.GetFormString("DepartmentCode");
            var searchList = EmployeeInfoService.GetInstance().GetList(where, true);


            return new AbpJsonResult(searchList, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<EmployeeInfoEntity> postData)
        {
            //var where = new EmployeeInfoEntity();
            //where.EmployeeCode = postData.RequestEntity.EmployeeCode;
            //EmployeeInfoService.GetInstance().GetList(where);
            postData.RequestEntity.CreatorUserCode = LoginUserInfo.UserCode;
            postData.RequestEntity.CreatorUserName = LoginUserInfo.UserName;
            postData.RequestEntity.CreationTime = DateTime.Now;
            if (!string.IsNullOrEmpty(postData.RequestEntity.EmployeeName))
            {
                postData.RequestEntity.PayCode = postData.RequestEntity.EmployeeName.GetStringSpellCode();
            }
            var addResult = EmployeeInfoService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<EmployeeInfoEntity>()
            {
                success = addResult.Item1,
                result = postData.RequestEntity,
                error = addResult.Item1 ? null : new ErrorInfo(addResult.Item2)
            };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit(AjaxRequest<EmployeeInfoEntity> postData)
        {
            postData.RequestEntity.LastModificationTime = DateTime.Now;
            if (!string.IsNullOrEmpty(postData.RequestEntity.EmployeeName))
            {
                postData.RequestEntity.PayCode = postData.RequestEntity.EmployeeName.GetStringSpellCode();
            }
            var updateResult = EmployeeInfoService.GetInstance().Update(postData.RequestEntity);
            var result = new AjaxResponse<EmployeeInfoEntity>()
            {
                success = updateResult.Item1,
                result = postData.RequestEntity,
                error = updateResult.Item1 ? null : new ErrorInfo(updateResult.Item2)
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = EmployeeInfoService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<EmployeeInfoEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }


        public AbpJsonResult GetEmployeeInfo(string employeeCode)
        {

            var list = EmployeeInfoService.GetInstance().GetList(new EmployeeInfoEntity() { EmployeeCode = employeeCode });

            if (list.Any())
            {
                var result = new AjaxResponse<EmployeeInfoEntity>()
                {
                    success = true,
                    result = list.FirstOrDefault()
                };
                return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
            }
            else
            {
                return new AbpJsonResult(new AjaxResponse<string>() { success = false, error = new ErrorInfo() { message = "请输入正确的员工号！" } });
            }


        }
    }
}





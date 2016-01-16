

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.ToolKit.JsonHandler;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;
using Project.Infrastructure.FrameworkCore.ToolKit.StringHandler;
using Project.Model.HRManager;
using Project.Mvc.Controllers.Results;
using Project.Mvc.Models;
using Project.Service.HRManager;
using Project.WebApplication.Controllers;

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
                ViewBag.BindEntity = JsonHelper.JsonSerializer(new EmployeeInfoEntity() { EmployeeCode = maxCode });
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
            //where.EmployeeCode = RequestHelper.GetFormString("EmployeeCode");
            //where.EmployeeName = RequestHelper.GetFormString("EmployeeName");
            //where.DepartmentCode = RequestHelper.GetFormString("DepartmentCode");
            //where.JobName = RequestHelper.GetFormString("JobName");
            //where.PayCode = RequestHelper.GetFormString("PayCode");
            //where.Sex = RequestHelper.GetFormString("Sex");
            //where.CertNo = RequestHelper.GetFormString("CertNo");
            //where.Birthday = RequestHelper.GetFormString("Birthday");
            //where.TechnicalTitle = RequestHelper.GetFormString("TechnicalTitle");
            //where.Duties = RequestHelper.GetFormString("Duties");
            //where.WorkState = RequestHelper.GetFormString("WorkState");
            //where.EmployeeType = RequestHelper.GetFormString("EmployeeType");
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


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<EmployeeInfoEntity> postData)
        {
            var where = new EmployeeInfoEntity();
            where.EmployeeCode = postData.RequestEntity.EmployeeCode;
            EmployeeInfoService.GetInstance().GetList(where);
            postData.RequestEntity.CreatorUserCode = LoginUserInfo.UserCode;
            postData.RequestEntity.CreateTime = DateTime.Now;
            postData.RequestEntity.PayCode = postData.RequestEntity.EmployeeName.GetStringSpellCode();
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
            postData.RequestEntity.PayCode = postData.RequestEntity.EmployeeName.GetStringSpellCode();
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
    }
}





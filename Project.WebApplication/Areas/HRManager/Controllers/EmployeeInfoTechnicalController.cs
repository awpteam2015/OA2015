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
using Project.Infrastructure.FrameworkCore.ToolKit.StringHandler;
using Project.Infrastructure.FrameworkCore.WebMvc.Controllers.Results;
using Project.Infrastructure.FrameworkCore.WebMvc.Models;
using Project.Service.PermissionManager;

namespace Project.WebApplication.Areas.HRManager.Controllers
{
    public class EmployeeInfoTechnicalController : BaseController
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

        [HttpPost]
        public AbpJsonResult Edit(AjaxRequest<EmployeeInfoEntity> postData)
        {

            var oldEmployeeInfo = EmployeeInfoService.GetInstance().GetModelByPk(postData.RequestEntity.PkId);
            var tempEntiy = oldEmployeeInfo.Clone() as EmployeeInfoEntity;
            if (tempEntiy == null)
                tempEntiy = new EmployeeInfoEntity();
            tempEntiy.Duties = postData.RequestEntity.Duties;
            tempEntiy.DutiesName = postData.RequestEntity.DutiesName;
            var updateResult = EmployeeInfoService.GetInstance().Update(tempEntiy);
            var result = new AjaxResponse<EmployeeInfoEntity>()
            {
                success = updateResult.Item1,
                result = postData.RequestEntity,
                error = updateResult.Item1 ? null : new ErrorInfo(updateResult.Item2)
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }


    }
}
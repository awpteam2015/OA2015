

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
    public class SanctionController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = SanctionService.GetInstance().GetModelByPk(pkId);
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
            var where = new SanctionEntity();
            //where.PkId = RequestHelper.GetFormString("PkId");
            where.SanctionType = RequestHelper.GetFormInt("SanctionType",-1);
            where.DepartmentCode = RequestHelper.GetFormString("DepartmentCode");
            where.DepartmentCode = string.Join(",", (DepartmentService.GetInstance().GetChiledArr(where.DepartmentCode, LoginUserInfo.UserDepartmentList.ToList(), LoginUserInfo.UserCode.ToUpper() == "ADMIN")));
            //where.SanctionObjType = RequestHelper.GetFormInt("SanctionObjType", 0);
            //where.SanctionObj = RequestHelper.GetFormString("SanctionObj");
            //where.SanctionTitle = RequestHelper.GetFormString("SanctionTitle");
            //where.SanctionMoney = RequestHelper.GetFormString("SanctionMoney");
            where.SanctionDate = RequestHelper.GetFormDateTime("SanctionDate");
            where.SanctionDateEnd = RequestHelper.GetFormDateTime("SanctionDateEnd");
            //where.Remark = RequestHelper.GetFormString("Remark");
            //where.CreatorUserCode = RequestHelper.GetFormString("CreatorUserCode");
            //where.CreatorUserName = RequestHelper.GetFormString("CreatorUserName");
            //where.CreateTime = RequestHelper.GetFormString("CreateTime");
            //where.LastModificationTime = RequestHelper.GetFormString("LastModificationTime");
            var searchList = SanctionService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<SanctionEntity> postData)
        {
            postData.RequestEntity.CreatorUserCode = LoginUserInfo.UserCode;
            postData.RequestEntity.CreatorUserName = LoginUserInfo.UserName;
            postData.RequestEntity.CreateTime = DateTime.Now;
            var addResult = SanctionService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<SanctionEntity>()
               {
                   success = true,
                   result = postData.RequestEntity
               };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit(AjaxRequest<SanctionEntity> postData)
        {
            var updateResult = SanctionService.GetInstance().Update(postData.RequestEntity);
            var result = new AjaxResponse<SanctionEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = SanctionService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<SanctionEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}





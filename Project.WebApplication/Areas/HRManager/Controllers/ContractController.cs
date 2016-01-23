

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using NHibernate.Mapping.ByCode.Impl;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.ToolKit;
using Project.Infrastructure.FrameworkCore.ToolKit.JsonHandler;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;
using Project.Model.HRManager;
using Project.Mvc.Controllers.Results;
using Project.Mvc.Models;
using Project.Service.HRManager;
using Project.WebApplication.Controllers;

namespace Project.WebApplication.Areas.HRManager.Controllers
{
    public class ContractController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = ContractService.GetInstance().GetModelByPk(pkId);
                ViewBag.BindEntity = JsonHelper.JsonSerializer(entity);
            }

            if (RequestHelper.GetInt("ParentId") > 0)
            {
                var entity = ContractService.GetInstance().GetModelByPk(RequestHelper.GetInt("ParentId"));
                entity.BeginDate = null;
                entity.EndDate = null;
                entity.ContractContent = null;
                entity.ContractNo = null;
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
            var where = new ContractEntity();
            //where.PkId = RequestHelper.GetFormString("PkId");
            //where.EmployeeCode = RequestHelper.GetFormString("EmployeeCode");
            //where.DepartmentCode = RequestHelper.GetFormString("DepartmentCode");
            //where.DepartmentName = RequestHelper.GetFormString("DepartmentName");
            //where.BeginDate = RequestHelper.GetFormString("BeginDate");
            //where.EndDate = RequestHelper.GetFormString("EndDate");
            //where.Remark = RequestHelper.GetFormString("Remark");
            //where.CreatorUserCode = RequestHelper.GetFormString("CreatorUserCode");
            //where.CreateTime = RequestHelper.GetFormString("CreateTime");
            //where.LastModifierUserCode = RequestHelper.GetFormString("LastModifierUserCode");
            //where.LastModificationTime = RequestHelper.GetFormString("LastModificationTime");
            //where.IsDelete = RequestHelper.GetFormString("IsDelete");
            //where.State = RequestHelper.GetFormString("State");
            //where.IsActive = RequestHelper.GetFormString("IsActive");
            //where.ContractNo = RequestHelper.GetFormString("ContractNo");
            //where.FirstParty = RequestHelper.GetFormString("FirstParty");
            //where.SecondParty = RequestHelper.GetFormString("SecondParty");
            //where.ContractContent = RequestHelper.GetFormString("ContractContent");
            //where.IdentityCardNo = RequestHelper.GetFormString("IdentityCardNo");
            var searchList = ContractService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver(new string[] { "Remark", "ContractContent" }));
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<ContractEntity> postData)
        {
            postData.RequestEntity.ContractContent = Base64Helper.DecodeBase64(postData.RequestEntity.ContractContent);
            postData.RequestEntity.IsActive = 1;
            var addResult = ContractService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<ContractEntity>()
               {
                   success = true,
                   result = postData.RequestEntity
               };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }



        [HttpPost]
        public AbpJsonResult Edit(AjaxRequest<ContractEntity> postData)
        {
            postData.RequestEntity.ContractContent = Base64Helper.DecodeBase64(postData.RequestEntity.ContractContent);
            var newInfo = postData.RequestEntity;
            var orgInfo = ContractService.GetInstance().GetModelByPk(postData.RequestEntity.PkId);
            var mergInfo = Mapper.Map(newInfo, orgInfo);

            var updateResult = ContractService.GetInstance().Update(mergInfo);
            var result = new AjaxResponse<ContractEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = ContractService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<ContractEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}





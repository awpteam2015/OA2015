

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
using Project.Infrastructure.FrameworkCore.WebMvc.Controllers.Results;
using Project.Infrastructure.FrameworkCore.WebMvc.Models;
using Project.Model.HRManager;
using Project.Service.HRManager;
using Project.Service.PermissionManager;
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
            else
            {
                ViewBag.BindEntity = JsonHelper.JsonSerializer(new ContractEntity(){State =1,IsActive = 1});
            }

            if (RequestHelper.GetInt("ParentId") > 0)
            {
                var entity = ContractService.GetInstance().GetModelByPk(RequestHelper.GetInt("ParentId"));
                if (RequestHelper.GetInt("State")!=4)
                {
                    entity.BeginDate = null;
                    entity.EndDate = null;
                    entity.ContractContent = null;
                    entity.ContractNo = null;
                }
               
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
  
            where.EmployeeCode = RequestHelper.GetFormString("EmployeeCode");
            where.DepartmentCode = string.Join(",", (DepartmentService.GetInstance().GetChiledArr(where.DepartmentCode, LoginUserInfo.UserDepartmentList.ToList(), LoginUserInfo.IsAdmin)));
            //RequestHelper.GetFormString("DepartmentCode");
            where.BeginDate = RequestHelper.GetDateTime("BeginDate");
            where.EndDate = RequestHelper.GetDateTime("EndDate");
            where.State = RequestHelper.GetInt("State");
            where.IsActive = RequestHelper.GetInt("IsActive");
            where.ContractNo = RequestHelper.GetFormString("ContractNo");
            where.FirstParty = RequestHelper.GetFormString("FirstParty");
            where.SecondParty = RequestHelper.GetFormString("SecondParty");
            where.ContractContent = RequestHelper.GetFormString("ContractContent");
            where.IdentityCardNo = RequestHelper.GetFormString("IdentityCardNo");
            var searchList = ContractService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver(new string[]{},new string[] { "Remark", "ContractContent" }));
        }


        [HttpPost]
        public AbpJsonResult<string> Add(AjaxRequest<ContractEntity> postData)
        {
            postData.RequestEntity.ContractContent = Base64Helper.DecodeBase64(postData.RequestEntity.ContractContent);
            postData.RequestEntity.IsActive = 1;
            var addResult = ContractService.GetInstance().Add(postData.RequestEntity);
            return new AbpJsonResult<string>(addResult);
        }


        [HttpPost]
        public AbpJsonResult<string> Edit(AjaxRequest<ContractEntity> postData)
        {
            postData.RequestEntity.ContractContent = Base64Helper.DecodeBase64(postData.RequestEntity.ContractContent);
            var newInfo = postData.RequestEntity;
            var orgInfo = ContractService.GetInstance().GetModelByPk(postData.RequestEntity.PkId);
            var mergInfo = Mapper.Map(newInfo, orgInfo);
            var updateResult = ContractService.GetInstance().Update(mergInfo);
            return new AbpJsonResult<string>(updateResult);
        }

        [HttpPost]
        public AbpJsonResult<string> Delete(int pkid)
        {
            var deleteResult = ContractService.GetInstance().DeleteByPkId(pkid);
            return new AbpJsonResult<string>(deleteResult);
        }

    }
}





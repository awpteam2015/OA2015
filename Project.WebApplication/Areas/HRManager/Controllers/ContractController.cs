

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Aspose.Cells;
using AutoMapper;
using NHibernate.Mapping.ByCode.Impl;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.ToolKit;
using Project.Infrastructure.FrameworkCore.ToolKit.JsonHandler;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;
using Project.Infrastructure.FrameworkCore.WebMvc.Controllers.Results;
using Project.Infrastructure.FrameworkCore.WebMvc.Models;
using Project.Model.HRManager;
using Project.Model.Other;
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
                ViewBag.BindEntity = JsonHelper.JsonSerializer(new ContractEntity() { State = 1, IsActive = 1 });
            }

            if (RequestHelper.GetInt("ParentId") > 0)
            {
                var entity = ContractService.GetInstance().GetModelByPk(RequestHelper.GetInt("ParentId"));
                if (RequestHelper.GetInt("State") != 4)
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

        public ActionResult Upload()
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
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver(new string[] { }, new string[] { "Remark", "ContractContent" }));
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



        [HttpPost]
        public AbpJsonResult Upload(AjaxRequest<UploadEntity> postData)
        {
            var path = Server.MapPath(postData.RequestEntity.FileUrl + "/" + postData.RequestEntity.FileName);

            Workbook workbook = new Workbook(path);
            Worksheet sheet = workbook.Worksheets[0];
            Cells cells = sheet.Cells;

            var resultMessage = "";

            for (int i = 1; i < cells.MaxDataRow + 1; i++)
            {
                var row = new ContractEntity();
                row.EmployeeCode = cells[i, 0].StringValue.Trim();
                var employeeInfo = EmployeeInfoService.GetInstance().GetEmployeeNameByCode2(row.EmployeeCode);
                row.DepartmentCode = employeeInfo.DepartmentCode;
                row.DepartmentName = employeeInfo.DepartmentName;
                row.IdentityCardNo = employeeInfo.CertNo;

                row.IsActive = int.Parse(cells[i, 4].StringValue.Trim().Substring(0, 1));
                row.State = int.Parse(cells[i, 3].StringValue.Trim().Substring(0, 1));
                row.BeginDate = DateTime.Parse(cells[i, 1].StringValue.Trim());
                row.EndDate = DateTime.Parse(cells[i, 2].StringValue.Trim());
                row.ContractNo = cells[i, 5].StringValue.Trim();
                row.FirstParty =  cells[i, 6].StringValue.Trim();
                row.SecondParty = employeeInfo.EmployeeName;
                row.ContractContent = "批量上传";
                var rowResult = ContractService.GetInstance().Add(row);
                if (!rowResult.Item1)
                {
                    resultMessage += row.EmployeeCode + ",";
                }
            }


            var result = new AjaxResponse<string>()
            {
                success = string.IsNullOrWhiteSpace(resultMessage),

                error = string.IsNullOrWhiteSpace(resultMessage) ? null : new ErrorInfo() { message = "员工号为："+resultMessage+"存在重复的合同编号！" }
            };
            return new AbpJsonResult(result, new NHibernateContractResolver());

        }

    }


}





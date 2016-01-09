using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.ToolKit;
using Project.Infrastructure.FrameworkCore.ToolKit.JsonHandler;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;
using Project.Model.PermissionManager;
using Project.Mvc.Controllers.Results;
using Project.Mvc.Models;
using Project.Service.PermissionManager;
using Project.WebApplication.Controllers;

namespace Project.WebApplication.Areas.PermissionManager.Controllers
{
    public class UserInfoController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            var roleList = RoleService.GetInstance().GetList(new RoleEntity());
            var departmentList = DepartmentService.GetInstance().GetList(new DepartmentEntity());
            if (pkId > 0)
            {
                var entity = UserInfoService.GetInstance().GetModel(pkId);

                ViewBag.BindEntity = JsonHelper.JsonSerializer(entity, new NHibernateContractResolver());

                roleList.Where(p => entity.UserRoleList.Any(x => x.RoleId == p.PkId)).ForEach(p =>
                {
                    p.Attr_UserRolePkId = entity.UserRoleList.SingleOrDefault(x => x.RoleId == p.PkId).PkId;
                    p.Attr_IsCheck = true;
                });

                departmentList.Where(p => entity.UserDepartmentList.Any(x => x.DepartmentCode == p.DepartmentCode)).ForEach(p =>
                {
                    p.Attr_UserDepartmentPkId = entity.UserDepartmentList.SingleOrDefault(x => x.DepartmentCode == p.DepartmentCode).PkId;
                    p.Attr_IsCheck = true;
                });
            }

            ViewBag.RoleList = JsonHelper.JsonSerializer(new DataGridResponse()
            {
                total = roleList.Count,
                rows = roleList
            }, new NHibernateContractResolver());

            ViewBag.DepartmentList = JsonHelper.JsonSerializer(new DataGridTreeResponse<DepartmentEntity>(departmentList.Count, departmentList), new NHibernateContractResolver());

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
            var where = new UserInfoEntity();
            where.UserCode = RequestHelper.GetString("UserCode");
            where.UserName = RequestHelper.GetString("UserName");
            where.Mobile = RequestHelper.GetString("Mobile");
            where.IsActive = RequestHelper.GetInt("IsActive");

            var searchList = UserInfoService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        public ActionResult UserFunctionDetailList()
        {
            return View();
        }

        //public AbpJsonResult GetUserFunctionDetailList()
        //{
        //    var pIndex = this.Request["page"].ConvertTo<int>();
        //    var pSize = this.Request["rows"].ConvertTo<int>();
        //    var where = new UserFunctionDetailEntity();
        //    //where.PkId = RequestHelper.GetFormString("PkId");
        //    //where.UserCode = RequestHelper.GetFormString("UserCode");
        //    //where.FunctionId = RequestHelper.GetFormString("FunctionId");
        //    //where.FunctionDetailId = RequestHelper.GetFormString("FunctionDetailId");
        //    var searchList = UserFunctionDetailService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

        //    var dataGridEntity = new DataGridResponse()
        //    {
        //        total = searchList.Item2,
        //        rows = searchList.Item1
        //    };
        //    return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        //}


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<UserInfoEntity> postData)
        {
            postData.RequestEntity.UserDepartmentList.ForEach(p => p.UserCode = postData.RequestEntity.UserCode);
            postData.RequestEntity.UserRoleList.ForEach(p => p.UserCode = postData.RequestEntity.UserCode);
            postData.RequestEntity.CreationTime = DateTime.Now;
            postData.RequestEntity.CreatorUserCode = "";
            postData.RequestEntity.Password = Encrypt.MD5Encrypt(postData.RequestEntity.Password);

            var addResult = UserInfoService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<UserInfoEntity>()
               {
                   success = addResult.Item1,
                   result = postData.RequestEntity,
                   error = addResult.Item1 ? null : new ErrorInfo(addResult.Item2) 
               };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit(AjaxRequest<UserInfoEntity> postData)
        {
            postData.RequestEntity.UserDepartmentList.ForEach(p => p.UserCode = postData.RequestEntity.UserCode);
            postData.RequestEntity.UserRoleList.ForEach(p => p.UserCode = postData.RequestEntity.UserCode);
            postData.RequestEntity.LastModificationTime = DateTime.Now;
            postData.RequestEntity.LastModifierUserCode = "";
            var updateResult = UserInfoService.GetInstance().Update(postData.RequestEntity);
            var result = new AjaxResponse<UserInfoEntity>()
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
            var deleteResult = UserInfoService.GetInstance().Delete(pkid);
            var result = new AjaxResponse<UserInfoEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult SetRowFunction()
        {
            var userCode = RequestHelper.GetString("UserCode");
            var functionPkId = RequestHelper.GetInt("FunctionPkId");
            var functionDetailPkId = RequestHelper.GetInt("FunctionDetailPkId");
            var isCheck = RequestHelper.GetInt("IsCheck") == 1;
            var addResult = UserInfoService.GetInstance().SetRowFunction(userCode, functionPkId, functionDetailPkId, isCheck);
            var result = new AjaxResponse<UserInfoEntity>()
            {
                success = addResult,
                result = null
            };
            return new AbpJsonResult(result, null);
        }
    }
}
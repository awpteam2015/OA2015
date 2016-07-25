

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.ToolKit;
using Project.Infrastructure.FrameworkCore.ToolKit.JsonHandler;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;
using Project.Infrastructure.FrameworkCore.WebMvc.Controllers.Results;
using Project.Infrastructure.FrameworkCore.WebMvc.Models;
using Project.Model.PermissionManager;
using Project.Service.PermissionManager;
using Project.WebApplication.Controllers;

namespace Project.WebApplication.Areas.PermissionManager.Controllers
{
    public class FunctionController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = FunctionService.GetInstance().GetModelByPk(pkId);


                ViewBag.BindEntity = JsonHelper.JsonSerializer(entity, new NHibernateContractResolver());

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
            var where = new FunctionEntity();
            //where.PkId = RequestHelper.GetFormString("PkId");
            //where.FunctionnName = RequestHelper.GetFormString("FunctionnName");
            where.ModuleId = RequestHelper.GetInt("ModuleId");
            //where.FunctionUrl = RequestHelper.GetFormString("FunctionUrl");
            //where.Area = RequestHelper.GetFormString("Area");
            //where.Controller = RequestHelper.GetFormString("Controller");
            //where.Action = RequestHelper.GetFormString("Action");
            //where.IsDisplayOnMenu = RequestHelper.GetFormString("IsDisplayOnMenu");
            //where.RankId = RequestHelper.GetFormString("RankId");
            //where.Remark = RequestHelper.GetFormString("Remark");
            var searchList = FunctionService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver(new[] { "ModuleEntity" }));
        }

        public AbpJsonResult GetListAll()
        {
            var checkList = new List<int>();
            var userCode = RequestHelper.GetString("UserCode");
            var roleId = RequestHelper.GetInt("RoleId");
            if (!string.IsNullOrWhiteSpace(userCode))
            {
                checkList = UserInfoService.GetInstance().GetFunctionDetailList_Checked(userCode);
            }

            if (roleId > 0)
            {
                checkList = RoleService.GetInstance().GetFunctionDetailList_Checked(roleId);
            }

            var where = new FunctionEntity();
            where.ModuleId = RequestHelper.GetInt("ModuleId");
            var searchList = FunctionService.GetInstance().GetList(where);
            searchList.ForEach(p => p.FunctionDetailList.ForEach(x =>
            {
                x.Attr_IsCheck = checkList.Any(y => y == x.PkId);
            }));

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Count,
                rows = searchList
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver(new[] { "FunctionDetailList" }));
        }


        //public AbpJsonResult GetListAll()
        //{
        //    var userCode = RequestHelper.GetString("UserCode");
        //    if (!string.IsNullOrWhiteSpace(userCode))
        //    {
        //        var userInfo = UserInfoService.GetInstance().GetUserInfo(userCode);
        //        userInfo.UserRoleList.ForEach(p =>
        //        {


        //        });
        //    }

        //    var checkList = RoleService.GetInstance().GetRoleFunctionDetailList(new RoleFunctionDetailEntity() { RoleId = RequestHelper.GetInt("RoleId") }).ToList();





        //    var where = new FunctionEntity();
        //    //where.PkId = RequestHelper.GetFormString("PkId");
        //    //where.FunctionnName = RequestHelper.GetFormString("FunctionnName");
        //    where.ModuleId = RequestHelper.GetInt("ModuleId");
        //    //where.FunctionUrl = RequestHelper.GetFormString("FunctionUrl");
        //    //where.Area = RequestHelper.GetFormString("Area");
        //    //where.Controller = RequestHelper.GetFormString("Controller");
        //    //where.Action = RequestHelper.GetFormString("Action");
        //    //where.IsDisplayOnMenu = RequestHelper.GetFormString("IsDisplayOnMenu");
        //    //where.RankId = RequestHelper.GetFormString("RankId");
        //    //where.Remark = RequestHelper.GetFormString("Remark");
        //    var searchList = FunctionService.GetInstance().GetList(where);

        //    searchList.ForEach(p =>
        //    {
        //        p.FunctionDetailList.ForEach(x =>
        //        {
        //            x.Attr_IsCheck = checkList.Any(y => y.FunctionDetailId == x.PkId);
        //        });
        //    });

        //    var dataGridEntity = new DataGridResponse()
        //    {
        //        total = searchList.Count,
        //        rows = searchList
        //    };
        //    return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver(new[] { "FunctionDetailList" }));
        //}


        public AbpJsonResult GetFunctionDetailList()
        {

            var where = new FunctionDetailEntity();
            where.FunctionId = RequestHelper.GetInt("FunctionId");

            var searchList = where.FunctionId == 0 ? new List<FunctionDetailEntity>() : FunctionService.GetInstance().GetFunctionDetailList(where);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Count,
                rows = searchList
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }

        //public AbpJsonResult GetRoleFunctionDetailList()
        //{
        //    var pIndex = this.Request["page"].ConvertTo<int>();
        //    var pSize = this.Request["rows"].ConvertTo<int>();
        //    var where = new FunctionDetailEntity();
        //    //where.PkId = RequestHelper.GetFormString("PkId");
        //    //where.FunctionDetailName = RequestHelper.GetFormString("FunctionDetailName");
        //    //where.FunctionDetailCode = RequestHelper.GetFormString("FunctionDetailCode");
        //    //where.FunctionId = RequestHelper.GetFormString("FunctionId");
        //    //where.Area = RequestHelper.GetFormString("Area");
        //    //where.Controller = RequestHelper.GetFormString("Controller");
        //    //where.Action = RequestHelper.GetFormString("Action");
        //    //where.CreatorUserCode = RequestHelper.GetFormString("CreatorUserCode");
        //    //where.CreationTime = RequestHelper.GetFormString("CreationTime");
        //    //where.LastModifierUserCode = RequestHelper.GetFormString("LastModifierUserCode");
        //    //where.LastModificationTime = RequestHelper.GetFormString("LastModificationTime");
        //    var searchList = FunctionService.GetInstance().GetFunctionDetailList(where, (pIndex - 1) * pSize, pSize);

        //    var dataGridEntity = new DataGridResponse()
        //    {
        //        total = searchList.Item2,
        //        rows = searchList.Item1
        //    };
        //    return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        //}



        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<FunctionEntity> postData)
        {
            var url = postData.RequestEntity.FunctionUrl;
            var urlRoute = url.Split('/');

            if (!postData.RequestEntity.FunctionDetailList.Any() && urlRoute.Count()>=4)
            {
                postData.RequestEntity.FunctionDetailList.Add(new FunctionDetailEntity()
                {
                    Area = urlRoute[1],
                    Controller = urlRoute[2],
                    Action = urlRoute[3],
                    CreationTime = DateTime.Now,
                    CreatorUserCode = LoginUserInfo.UserCode,
                    FunctionDetailCode = "btn_View",
                    FunctionDetailName = "浏览"
                });
            }

            var addResult = FunctionService.GetInstance().Add(postData.RequestEntity);

            var result = new AjaxResponse<FunctionEntity>()
               {
                   success = true,
                   result = postData.RequestEntity
               };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit(AjaxRequest<FunctionEntity> postData)
        {
            var updateResult = FunctionService.GetInstance().Update(postData.RequestEntity);
            var result = new AjaxResponse<FunctionEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = FunctionService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<FunctionEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}





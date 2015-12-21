using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.ToolKit;
using Project.Infrastructure.FrameworkCore.ToolKit.JsonHandler;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;
using Project.Model.PermissionManager;
using Project.Mvc.Controllers.Results;
using Project.Mvc.Models;
using Project.Service.PermissionManager;

namespace Project.WebApplication.Areas.PermissionManager.Controllers
{
    public class UserInfoController : Controller
    {
        // GET: PermissionManager/UserInfo
        //public ActionResult Hd()
        //{
        //    return View();
        //}

        public ActionResult Hd(int PkId = 0)
        {
            if (PkId > 0)
            {
                ViewBag.BindEntity = "{\"UserCode\":\"1111\",\"List\":[{\"List\":\"111\"},{\"List\":\"222\"}]}";
            }

            return View();
        }

        //public AbpJsonResult Add()
        //{


        //    return new AbpJsonResult(new AjaxResponse<UserInfoEntity>(), new NHibernateContractResolver());
        //}

        //public AbpJsonResult Update()
        //{

        //    return new AbpJsonResult(new AjaxResponse<UserInfoEntity>(), new NHibernateContractResolver());
        //}


        public ActionResult List()
        {
            return View();
        }


        public AbpJsonResult GetList()
        {
            var pIndex = this.Request["page"].ConvertTo<int>();
            var pSize = this.Request["rows"].ConvertTo<int>();
            var where = new UserInfoEntity();
            var searchList = UserInfoService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<UserInfoEntity> postData)
        {
            var addResult = UserInfoService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<UserInfoEntity>()
               {
                   success = true,
                   result = postData.RequestEntity
               };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit( AjaxRequest<UserInfoEntity> postData)
        {
            var updateResult = UserInfoService.GetInstance().Update(postData.RequestEntity);
            var result = new AjaxResponse<UserInfoEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}
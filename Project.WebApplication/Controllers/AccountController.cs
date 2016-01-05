using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using System.Web.UI;
using Newtonsoft.Json;
using Project.Infrastructure.FrameworkCore.ToolKit;
using Project.Model.PermissionManager;
using Project.Mvc.Authorization;
using Project.Mvc.Controllers.Results;
using Project.Mvc.Models;
using Project.Mvc.Views;
using Project.Service.PermissionManager;
using Project.Service.PermissionManager.DTO;


namespace Project.WebApplication.Controllers
{
    public class AccountController : BaseController
    {

         //[PermissionAuthorize]
        // GET: Account
        public ActionResult Index()
        {  // ISAPIRuntime
            //  PageHandlerFactory
            //   ApplicationManager
            //        applicat
            //UrlRoutingModule
            //   MvcHandler



            ViewBag.ModuleList = UserInfoService.GetInstance().GetMenuDTOList(LoginUserInfo.UserCode,LoginUserInfo.PermissionCodeList);



            return View();
        }



        public JsonResult GetData()
        {
            //var t = new JsonResult();
            //t.Data = new { success = true, Message = "成功！", unAuthorizedRequest = false, targetUrl="http://www.baidu.com" };
            //return t;

            return new AbpJsonResult
            {
                Data = new AjaxResponse<object>() { success = true, result = "{'name':'1111'}", }
            };
        }

       


        public ActionResult Default()
        {
            return View();
        }


    }

}
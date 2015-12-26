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
using Project.Mvc.Authorization;
using Project.Mvc.Controllers.Results;
using Project.Mvc.Models;
using Project.Mvc.Views;


namespace Project.WebApplication.Controllers
{
    public class AccountController : Controller
    {

       // [PermissionAuthorize]
        // GET: Account
        public ActionResult Index()
        {  // ISAPIRuntime
         //  PageHandlerFactory
         //   ApplicationManager
        //        applicat
       //UrlRoutingModule
            return View();
        }


            [HttpGet]
        public ActionResult Login()
        {
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

        [HttpPost]
        public JsonResult UserLogin(string userCode, string password)
        {
            var loginUserInfo = new LoginUserInfo();
            loginUserInfo.PermissionCodeList = new List<string>() { "111", "222" };
            loginUserInfo.UserCode = "UserCode";
            loginUserInfo.UserName = "UserName";

            var ticket = new FormsAuthenticationTicket(
            1 /*version*/,
            Guid.NewGuid().ToString(),
            DateTime.Now,
             DateTime.Now.AddMinutes(20),
            true,//持久性
           JsonConvert.SerializeObject(loginUserInfo),
            FormsAuthentication.FormsCookiePath);
            var encryptedTicket = FormsAuthentication.Encrypt(ticket);


            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            cookie.Expires = DateTime.Now.AddMinutes(20);
            cookie.HttpOnly = true;
            Response.Cookies.Add(cookie);

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
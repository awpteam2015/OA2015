using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using Project.Infrastructure.FrameworkCore.Logging;
using Project.Infrastructure.FrameworkCore.ToolKit;
using Project.Infrastructure.FrameworkCore.WebMvc.Controllers.Results;
using Project.Infrastructure.FrameworkCore.WebMvc.Models;
using Project.Service.PermissionManager;

namespace Project.WebApplication.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index3()
        {

            return View();
        }

        public ActionResult Index2()
        {

            return View();
        }

        [HttpPost]
        public JsonResult UserLogin(string userCode, string password)
        {
            LoggerHelper.Info("登陆前：");
            var userInfo = UserInfoService.GetInstance().Login(userCode, password);
            if (!userInfo.Item1)
            {
                return new AbpJsonResult
                {
                    Data = new AjaxResponse<object>() { success = false, error = new ErrorInfo(userInfo.Item2) }
                };
            }

            var ticket = new FormsAuthenticationTicket(
            1 /*version*/,
            Guid.NewGuid().ToString(),
            DateTime.Now,
            DateTime.Now.AddMinutes(300),
            true,//持久性
            JsonConvert.SerializeObject(userInfo.Item3),
            FormsAuthentication.FormsCookiePath);
            var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            cookie.Expires = DateTime.Now.AddMinutes(300);
            cookie.HttpOnly = true;
            Response.Cookies.Add(cookie);

            LoggerHelper.Info("登陆结束：");
            return new AbpJsonResult
            {
                Data = new AjaxResponse<object>() { success = true }
            };
        }


        [HttpPost]
        public JsonResult UserLogoff(string userCode, string password)
        {
            FormsAuthentication.SignOut();
            CookieHelper.Del(FormsAuthentication.FormsCookieName); ;
            return new AbpJsonResult
            {
                Data = new AjaxResponse<object>() { success = true }
            };
        }

    }
}
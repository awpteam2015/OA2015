using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Newtonsoft.Json;
using Project.Infrastructure.FrameworkCore.ToolKit;
using Project.Mvc.Controllers.Results;
using Project.Mvc.Models;
using Project.Service.PermissionManager;

namespace Project.WebApplication.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public JsonResult UserLogin(string userCode, string password)
        {
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
            DateTime.Now.AddMinutes(30),
            true,//持久性
            JsonConvert.SerializeObject(userInfo.Item3),
            FormsAuthentication.FormsCookiePath);
            var encryptedTicket = FormsAuthentication.Encrypt(ticket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            cookie.Expires = DateTime.Now.AddMinutes(30);
            cookie.HttpOnly = true;
            Response.Cookies.Add(cookie);
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
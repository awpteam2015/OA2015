using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Model.PermissionManager;
using Project.Mvc.Models;

namespace Project.WebApplication.Areas.PermissionManager.Controllers
{
    public class UserInfoController : Controller
    {
        // GET: PermissionManager/UserInfo
        //public ActionResult Hd()
        //{
        //    return View();
        //}

        public ActionResult Hd(int PkId=0)
        {
            if (PkId>0)
            {
                ViewBag.BindEntity = "{\"UserCode\":\"1111\",\"List\":[{\"List\":\"111\"},{\"List\":\"222\"}]}";
            }
            
            return View();
        }

        public ActionResult List()
        {
            return View();
        }

        [HttpPost]
        public string Hd(AjaxRequest<UserInfoEntity> userInfoEntity)
        {

            return "";
        }
    }
}
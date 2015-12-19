using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.WebApplication.Areas.PermissionManager.Controllers
{
    public class PermissionController : Controller
    {
        // GET: PermissionManager/Permission
        public ActionResult Index()
        {
            return View();
        }
    }
}
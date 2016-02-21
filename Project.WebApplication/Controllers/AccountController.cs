using System.Web.Mvc;
using Project.Config;
using Project.Infrastructure.FrameworkCore.WebMvc.Controllers.Results;
using Project.Infrastructure.FrameworkCore.WebMvc.Models;
using Project.Service.PermissionManager;
using Project.Service.SystemSetManager;


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

            ViewBag.User = LoginUserInfo;
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
            ViewBag.Des = IndexPageSetService.GetInstance().GetModelByPk(SiteConfig.GetConfig().IndexPagePkId).Des;
            return View();
        }


    }

}
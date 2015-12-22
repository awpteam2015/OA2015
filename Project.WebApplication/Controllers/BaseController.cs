using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Project.WebApplication.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var javascriptBuilder = new StringBuilder();
                //notPagePopedoms.ForEach(p => javascriptBuilder.AppendFormat("$('#{0}').next().remove('.datagrid-btn-separator');$('#{0}').remove();", p.RightTagId));
                ViewBag.PermissionScript = javascriptBuilder.ToString();
            }

            base.OnResultExecuting(filterContext);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            //if (filterContext == null) return;

            //var exception = filterContext.Exception ?? new Exception("不存在进一步错误信息");

            //string message;
            ////如果没有记录日常，就记录日志
            //if (exception is EipException)
            //{
            //    message = exception.Message;
            //}
            //else
            //{
            //    //记录日志
            //    message = new ExceptionLogRecordsService().Records(exception);
            //    exception = new EipException(message, exception);
            //}

            //if (!filterContext.HttpContext.IsCustomErrorEnabled)
            //{
            //    base.OnException(filterContext);
            //    return;
            //}

            //filterContext.ExceptionHandled = true;

            //if (Request.IsAjaxRequest())
            //{
            //    filterContext.Result = Content(message);
            //}
            //else
            //{
            //    var controllerName = (string)filterContext.RouteData.Values["controller"];
            //    var actionName = (string)filterContext.RouteData.Values["action"];
            //    var model = new HandleErrorInfo(exception, controllerName, actionName);
            //    filterContext.Result = new ViewResult
            //    {
            //        ViewName = "InternalServer",
            //        MasterName = "",
            //        ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
            //        TempData = filterContext.Controller.TempData
            //    };
            //}

            //filterContext.HttpContext.Response.Clear();
            //filterContext.HttpContext.Response.Clear();
            //filterContext.HttpContext.Response.StatusCode = 500;
            //filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }
}
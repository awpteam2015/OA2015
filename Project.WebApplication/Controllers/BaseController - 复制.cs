//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Web;
//using System.Web.Mvc;
//using System.Web.Mvc.Filters;

//namespace Project.WebApplication.Controllers
//{
//    public class BaseController : Controller
//    {

//        /// <summary>
//        /// 在调用操作方法前调用。
//        /// </summary>
//        /// <param name="filterContext">有关当前请求和操作的信息。</param>
//        protected override void OnActionExecuting(ActionExecutingContext filterContext)
//        {
//            if (!HttpContext.User.Identity.IsAuthenticated || CurrentUser == null)
//            {
//                filterContext.Result = new ContentResult { Content = @"<script>window.top.location='/Login/Index'</script>" };
//                base.OnActionExecuting(filterContext);
//                return;
//            }

//            if (filterContext == null)
//            {
//                throw new ArgumentNullException("filterContext");
//            }

//            var controllerName = filterContext.RouteData.Values["controller"].ToString();
//            var actionName = filterContext.RouteData.Values["action"].ToString();

//            object[] moduleFunction = filterContext.ActionDescriptor.GetCustomAttributes(typeof(ModuleFunctionAttribute), true);
//            if (moduleFunction.Length > 0)
//            {
//                notPagePopedoms = userService.GetNotPagePopedoms(CurrentUser, controllerName, actionName);
//            }

//            object[] functionRights = filterContext.ActionDescriptor.GetCustomAttributes(typeof(FunctionRightAttribute), true);
//            if (functionRights.Length == 0)
//            {
//                base.OnActionExecuting(filterContext);
//                return;
//            }

//            if (functionRights.Length > 0)
//            {
//                var functionRight = (FunctionRightAttribute)functionRights[0];
//                if (functionRight.Associated != null)
//                {
//                    actionName = functionRight.Associated;
//                }
//            }
//            if (actionName != "Audit")
//            {
//                var pagePopedom = userService.GetPagePopedom(CurrentUser, controllerName, actionName);
//                if (!pagePopedom && moduleFunction.Length > 0)
//                {
//                    filterContext.Result = new ContentResult { Content = @"<script>window.top.location='/Login/Index'</script>" };
//                }
//                else if (!pagePopedom && moduleFunction.Length == 0)
//                {
//                    filterContext.Result = new ContentResult { Content = @"没有操作权限" };
//                }
//            }


//            ViewBag.ShowInfo += "OnActionExecuting<br/>";
//            base.OnActionExecuting(filterContext);
//        }

//        /// <summary>
//        /// 在调用操作方法后调用。
//        /// </summary>
//        /// <param name="filterContext">有关当前请求和操作的信息。</param>
//        protected override void OnActionExecuted(ActionExecutedContext filterContext)
//        {
//            ViewBag.ShowInfo += "OnActionExecuted<br/>";
//            base.OnActionExecuted(filterContext);
//        }

//        /// <summary>
//        /// 在进行授权时调用。
//        /// </summary>
//        /// <param name="filterContext">有关当前请求和操作的信息。</param>
//        protected override void OnAuthentication(AuthenticationContext filterContext)
//        {
//            //身份验证
//            ViewBag.ShowInfo += "OnAuthentication<br/>";
//            base.OnAuthentication(filterContext);
//        }

//        /// <summary>
//        /// 在进行授权质询时调用。
//        /// </summary>
//        /// <param name="filterContext">有关当前请求和操作的信息。</param>
//        protected override void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
//        {
//            ViewBag.ShowInfo += "OnAuthenticationChallenge<br/>";
//            base.OnAuthenticationChallenge(filterContext);
//        }

//        /// <summary>
//        /// 在进行授权时调用。
//        /// </summary>
//        /// <param name="filterContext">有关当前请求和操作的信息。</param>
//        protected override void OnAuthorization(AuthorizationContext filterContext)
//        {
//            //授权
//            ViewBag.ShowInfo += "OnAuthorization<br/>";
//            base.OnAuthorization(filterContext);
//        }

//        /// <summary>
//        /// 当操作中发生未经处理的异常时调用。
//        /// </summary>
//        /// <param name="filterContext">有关当前请求和操作的信息。</param>
//        //protected override void OnException(ExceptionContext filterContext)
//        //{
//        //    ViewBag.ShowInfo += "OnException<br/>";
//        //    base.OnException(filterContext);
//        //}

//        /// <summary>
//        /// 在执行由操作方法返回的操作结果后调用。
//        /// </summary>
//        /// <param name="filterContext">有关当前请求和操作结果的信息。</param>
//        protected override void OnResultExecuted(ResultExecutedContext filterContext)
//        {
//            ViewBag.ShowInfo += "OnResultExecuted<br/>";
//            base.OnResultExecuted(filterContext);
//        }

//        /// <summary>
//        /// 在执行由操作方法返回的操作结果前调用。
//        /// </summary>
//        /// <param name="filterContext">有关当前请求和操作结果的信息。</param>
//        protected override void OnResultExecuting(ResultExecutingContext filterContext)
//        {
//            ViewBag.ShowInfo += "OnResultExecuting<br/>";
//            base.OnResultExecuting(filterContext);
//        }

//        // GET: Base
//        //protected override void OnResultExecuting(ResultExecutingContext filterContext)
//        //{
//        //    if (HttpContext.User.Identity.IsAuthenticated)
//        //    {
//        //        var javascriptBuilder = new StringBuilder();
//        //        //notPagePopedoms.ForEach(p => javascriptBuilder.AppendFormat("$('#{0}').next().remove('.datagrid-btn-separator');$('#{0}').remove();", p.RightTagId));
//        //        ViewBag.ShowInfo = javascriptBuilder.ToString();
//        //    }

//        //    base.OnResultExecuting(filterContext);
//        //}

//        protected override void OnException(ExceptionContext filterContext)
//        {
//            //if (filterContext == null) return;

//            //var exception = filterContext.Exception ?? new Exception("不存在进一步错误信息");

//            //string message;
//            ////如果没有记录日常，就记录日志
//            //if (exception is EipException)
//            //{
//            //    message = exception.Message;
//            //}
//            //else
//            //{
//            //    //记录日志
//            //    message = new ExceptionLogRecordsService().Records(exception);
//            //    exception = new EipException(message, exception);
//            //}

//            //if (!filterContext.HttpContext.IsCustomErrorEnabled)
//            //{
//            //    base.OnException(filterContext);
//            //    return;
//            //}

//            //filterContext.ExceptionHandled = true;

//            //if (Request.IsAjaxRequest())
//            //{
//            //    filterContext.Result = Content(message);
//            //}
//            //else
//            //{
//            //    var controllerName = (string)filterContext.RouteData.Values["controller"];
//            //    var actionName = (string)filterContext.RouteData.Values["action"];
//            //    var model = new HandleErrorInfo(exception, controllerName, actionName);
//            //    filterContext.Result = new ViewResult
//            //    {
//            //        ViewName = "InternalServer",
//            //        MasterName = "",
//            //        ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
//            //        TempData = filterContext.Controller.TempData
//            //    };
//            //}

//            //filterContext.HttpContext.Response.Clear();
//            //filterContext.HttpContext.Response.Clear();
//            //filterContext.HttpContext.Response.StatusCode = 500;
//            //filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
//        }
//    }
//}
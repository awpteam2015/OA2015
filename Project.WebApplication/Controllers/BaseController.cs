using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Security;
using Newtonsoft.Json;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.Logging;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;
using Project.Model.PermissionManager;
using Project.Mvc.Controllers.Results;
using Project.Mvc.Models;
using Project.Service.PermissionManager;
using Project.Service.PermissionManager.DTO;

namespace Project.WebApplication.Controllers
{
    public class BaseController : Controller
    {

        /// <summary>
        /// 用户信息
        /// </summary>
        public LoginUserInfoDTO LoginUserInfo { get; set; }

        private IList<PermissionFunctionDetailDTO> _notPermissionFunctionDetailList = new List<PermissionFunctionDetailDTO>();


        /// <summary>
        /// 在调用操作方法前调用。
        /// </summary>
        /// <param name="filterContext">有关当前请求和操作的信息。</param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            var areaName = "";
            if (filterContext.RouteData.DataTokens.ContainsKey("area"))
            {
                areaName = filterContext.RouteData.DataTokens["area"].ToString();
            }
            var controllerName = filterContext.RouteData.Values["controller"].ToString();
            var actionName = filterContext.RouteData.Values["action"].ToString();

            var userData = ((FormsIdentity)User.Identity).Ticket.UserData;
            LoginUserInfo = JsonConvert.DeserializeObject<LoginUserInfoDTO>(userData);
            if (PermissionService.GetInstance().IsAdmin(LoginUserInfo.UserCode))
            {
                base.OnActionExecuting(filterContext);
                return;
            }


            //判读是否同一个ip
            //if (loginUserInfo.ClientIp != Request.UserHostAddress)
            //{
            //    throw new ArgumentNullException("IP改变了");
            //}

            var allPermissionFunction = PermissionService.GetInstance().GetAllPermissionFunction();

            if (allPermissionFunction.Any(p => p.Area == areaName && p.Controller == controllerName && p.Action == actionName))
            {
                var userPermissionFunction = (from hasList in LoginUserInfo.PermissionCodeList
                                              from allList in allPermissionFunction
                                              where hasList == allList.PkId
                                              select new { hasList, allList }).ToList();
                var functionDetailEntity = userPermissionFunction.SingleOrDefault(p =>
                    p.allList.Area == areaName && p.allList.Controller == controllerName &&
                    p.allList.Action == actionName);


                if (functionDetailEntity == null)
                {
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        filterContext.Result = new AbpJsonResult(new AjaxResponse<object>()
                        {
                            success = false,
                            error = new ErrorInfo("没有权限")
                        });
                    }
                    else
                    {
                        filterContext.Result = new ContentResult { Content = @"<script>window.top.location='/Login/Index'</script>" };
                    }
                }
                else
                {
                    _notPermissionFunctionDetailList = allPermissionFunction.Where(
                           p =>
                               p.FunctionId == functionDetailEntity.allList.FunctionId &&
                               LoginUserInfo.PermissionCodeList.All(x => x != p.PkId)).ToList();
                }

            }

            ViewBag.ShowInfo += "OnActionExecuting<br/>";
            base.OnActionExecuting(filterContext);
        }

        /// <summary>
        /// 在调用操作方法后调用。
        /// </summary>
        /// <param name="filterContext">有关当前请求和操作的信息。</param>
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ViewBag.ShowInfo += "OnActionExecuted<br/>";
            base.OnActionExecuted(filterContext);
        }

        /// <summary>
        /// 在进行授权时调用。
        /// </summary>
        /// <param name="filterContext">有关当前请求和操作的信息。</param>
        protected override void OnAuthentication(AuthenticationContext filterContext)
        {
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new ContentResult { Content = @"<script>window.top.location='/Login/Index'</script>" };
                base.OnAuthentication(filterContext);
                return;
            }

            //身份验证
            ViewBag.ShowInfo += "OnAuthentication<br/>";
            base.OnAuthentication(filterContext);
        }

        /// <summary>
        /// 在进行授权质询时调用。
        /// </summary>
        /// <param name="filterContext">有关当前请求和操作的信息。</param>
        protected override void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            ViewBag.ShowInfo += "OnAuthenticationChallenge<br/>";
            base.OnAuthenticationChallenge(filterContext);
        }

        /// <summary>
        /// 在进行授权时调用。
        /// </summary>
        /// <param name="filterContext">有关当前请求和操作的信息。</param>
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            //授权
            ViewBag.ShowInfo += "OnAuthorization<br/>";
            base.OnAuthorization(filterContext);
        }

        /// <summary>
        /// 当操作中发生未经处理的异常时调用。
        /// </summary>
        /// <param name="filterContext">有关当前请求和操作的信息。</param>
        //protected override void OnException(ExceptionContext filterContext)
        //{
        //    ViewBag.ShowInfo += "OnException<br/>";
        //    base.OnException(filterContext);
        //}

        /// <summary>
        /// 在执行由操作方法返回的操作结果后调用。
        /// </summary>
        /// <param name="filterContext">有关当前请求和操作结果的信息。</param>
        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            ViewBag.ShowInfo += "OnResultExecuted<br/>";
            base.OnResultExecuted(filterContext);
        }

        /// <summary>
        /// 在执行由操作方法返回的操作结果前调用。
        /// </summary>
        /// <param name="filterContext">有关当前请求和操作结果的信息。</param>
        //protected override void OnResultExecuting(ResultExecutingContext filterContext)
        //{
        //    ViewBag.ShowInfo += "OnResultExecuting<br/>";
        //    base.OnResultExecuting(filterContext);
        //}

        // GET: Base
        protected override void OnResultExecuting(ResultExecutingContext filterContext)
        {

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                var javascriptBuilder = new StringBuilder();
                _notPermissionFunctionDetailList.Where(p => !string.IsNullOrEmpty(p.FunctionDetailCode)).ForEach(p =>
                    javascriptBuilder.AppendFormat("$('#{0}').remove();", p.FunctionDetailCode));
                ViewBag.PermissionScript = javascriptBuilder.ToString();
            }

            base.OnResultExecuting(filterContext);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null) return;

            var exception = filterContext.Exception ?? new Exception("不存在进一步错误信息");
            LoggerHelper.Error(LogType.ErrorLogger, exception.Message);


            if (Request.IsAjaxRequest())
            {
                filterContext.Result = new AbpJsonResult
                {
                    Data = new AjaxResponse<object>() { success = false, error = new ErrorInfo(exception.ToString()) }
                };
            }
            else
            {
                var controllerName = (string)filterContext.RouteData.Values["controller"];
                var actionName = (string)filterContext.RouteData.Values["action"];
                var model = new HandleErrorInfo(exception, controllerName, actionName);
                filterContext.Result = new ViewResult
                {
                    ViewName = "InternalServer",
                    MasterName = "",
                    ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                    TempData = filterContext.Controller.TempData
                };
            }
            base.OnException(filterContext);
           // return;
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
           // filterContext.HttpContext.Response.Clear();
            //filterContext.HttpContext.Response.StatusCode = 500;
            //filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }
}
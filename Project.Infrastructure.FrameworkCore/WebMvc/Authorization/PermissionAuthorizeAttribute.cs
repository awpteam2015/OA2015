using System;
using System.Web;
using System.Web.Mvc;
using Project.Infrastructure.FrameworkCore.WebMvc.Models;

namespace Project.Infrastructure.FrameworkCore.WebMvc.Authorization
{
    /// <summary>
    /// 授权认证
    /// </summary>
    public class PermissionAuthorizeAttribute : AuthorizeAttribute
    {
        public PermissionFunctionInfo PermissionFunctionInfo { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("HttpContext");
            }
            if (!httpContext.User.Identity.IsAuthenticated)
            {
                return false;
            }

            //var userData = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket.UserData;
            //var loginUserInfo = JsonConvert.DeserializeObject<LoginUserInfo>(userData);

            ////判读是否同一个ip
            //if (loginUserInfo.ClientIp != HttpContext.Current.Request.UserHostAddress)
            //{
            //}


           
            //var allPermissionFunction = PermissionService.GetInstance().GetAllPermissionFunction();


            //var list = from hasList in loginUserInfo.PermissionCodeList
            //           from allList in allPermissionFunction
            //           where hasList == allList.FunctionCode
            //           select new { hasList, allList };


            return true;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //PermissionFunctionInfo = new PermissionFunctionInfo();
            //if (filterContext.RouteData.DataTokens.ContainsKey("area"))
            //{
            //    PermissionFunctionInfo.Area = filterContext.RouteData.DataTokens["area"].ToString();
            //}

            //PermissionFunctionInfo.Controller = filterContext.RouteData.Values["controller"].ToString();
            //PermissionFunctionInfo.Action = filterContext.RouteData.Values["action"].ToString();
            base.OnAuthorization(filterContext);
        }


        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
            //if (filterContext.HttpContext.Request.IsAjaxRequest())
            //{
            //    var returnUrl = string.Empty;
            //    var referrerUrl = filterContext.HttpContext.Request.UrlReferrer;
            //    if (referrerUrl != null)
            //    {
            //        returnUrl = HttpUtility.UrlEncode(referrerUrl.PathAndQuery);
            //    }
            //    var json = JsonConvert.SerializeObject(new
            //    {
            //        status = "notauthorized",
            //        msg = "登录超时,请重新登录。",
            //        loginUrl = "/account/login?ReturnUrl=" + returnUrl
            //    });
            //    filterContext.Result = new ContentResult
            //    {
            //        Content = json
            //    };
            //}

        }
    }
}

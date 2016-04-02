using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Xml.Linq;
using Newtonsoft.Json;
using Project.Infrastructure.FrameworkCore.Logging;
using Project.Infrastructure.FrameworkCore.ToolKit;
using Project.Infrastructure.FrameworkCore.WebMvc.Controllers.Results;
using Project.Infrastructure.FrameworkCore.WebMvc.Models;
using Project.Model.PermissionManager;
using Project.Service.PermissionManager;

namespace Project.WebApplication.Controllers
{
   

    public class DepatmentTemp
    {
        /// <summary>
        /// 机构代码
        /// </summary>
        public string JGDM { get; set; }


        /// <summary>
        /// 医疗机构编码
        /// </summary>
        public string JGBM { get; set; }

        /// <summary>
        /// 机构名称
        /// </summary>
        public string JGMC { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string KSMC { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        public string KSDM { get; set; }

        /// <summary>
        /// 科室父级编码
        /// </summary>
        public string KSFJBM { get; set; }

        /// <summary>
        ///  机构/科室状态代码
        /// </summary>
        public string TYPE { get; set; }
    }
    public class LoginController : Controller
    {
        public static DateTime UpdateTime=DateTime.Now.AddDays(-1);

       // public static int dd = 1;

        // GET: Login
        public ActionResult Index3()
        {

            return View();
        }

        public ActionResult Index2()
        {











        //    dd++;
        //    ViewBag.dd = dd;
            var Third_DepartmentWebServiceUrl =
                System.Configuration.ConfigurationManager.AppSettings["Third_DepartmentWebServiceUrl"];
            if (Third_DepartmentWebServiceUrl != "" && DateTime.Now.Subtract(UpdateTime).Days >= 1)
            {
                UpdateTime=DateTime.Now;

                string result = HttpHelper.Helper.GetResponseString(System.Configuration.ConfigurationManager.AppSettings["Third_DepartmentWebServiceUrl"], "Get", new Dictionary<string, string> { }, Encoding.Default, Encoding.UTF8, 10000);

                XDocument doc = XDocument.Parse(result);

                //var path = Server.MapPath("/Config/XMLFile1.xml");

                //XDocument doc = XDocument.Load(path);
                var text = from t in doc.Descendants("dept")                    //定位到节点 
                           select new DepatmentTemp()
                           {
                               JGDM = t.Element("jgdm").Value,
                               JGBM = t.Element("jgbm").Value,
                               JGMC = t.Element("jgmc").Value,
                               KSMC = t.Element("ksmc").Value,
                               KSDM = t.Element("ksdm").Value,
                               KSFJBM = t.Element("ksfjbm").Value,
                               TYPE = t.Element("type").Value,
                           };
                var allList = text.ToList();

                var departmentSecondList = new List<DepartmentEntity>();

                var departmentThirdList = new List<DepartmentEntity>();

                var secondlist1 = allList.Where(p => p.JGBM == "330110");

                foreach (var a in secondlist1)
                {
                    departmentSecondList.Add(new DepartmentEntity()
                    {
                        DepartmentCode = a.KSDM,
                        DepartmentName = a.KSMC,
                        ParentDepartmentCode = "330110"
                    });
                }

                var secondlist2 = allList.Where(p => p.JGBM != "330110");
                foreach (var a in secondlist2)
                {
                    departmentSecondList.Add(new DepartmentEntity()
                    {
                        DepartmentCode = a.JGDM,
                        DepartmentName = a.JGMC,
                        ParentDepartmentCode = a.JGDM== "330110"?"0": "330110"
                    });

                    departmentThirdList.Add(new DepartmentEntity()
                    {
                        DepartmentCode = a.KSDM,
                        DepartmentName = a.KSMC,
                        ParentDepartmentCode = a.JGDM
                    });
                }

                departmentSecondList.ForEach(p =>
                {
                    DepartmentService.GetInstance().Add(p);
                });

                departmentThirdList.ForEach(p =>
                {
                    DepartmentService.GetInstance().Add(p);
                });



            }





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
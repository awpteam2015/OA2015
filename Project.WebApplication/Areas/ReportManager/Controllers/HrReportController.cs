using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.WebApplication.Areas.ReportManager.Controllers
{
    public class HrReportController : Controller
    {
        // GET: ReportManager/HrReport
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AttendanceReport()
        {
            return View();
        }
    }
}
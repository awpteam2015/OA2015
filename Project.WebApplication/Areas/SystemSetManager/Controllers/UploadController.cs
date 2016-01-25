using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Infrastructure.FrameworkCore.ToolKit;
using Project.Infrastructure.FrameworkCore.ToolKit.JsonHandler;
using Project.WebApplication.Controllers;

namespace Project.WebApplication.Areas.SystemSetManager.Controllers
{
    public class UploadController : Controller
    {
        // GET: SystemSetManager/Upload
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ContentResult UploadImage()
        {
            var filePath = Path.Combine(ConfigurationManager.AppSettings["UploadFile"], RequestHelper.GetFormString("path"));
            var file = Request.Files["Filedata"];
            if (file == null)
            {
                return Content(JsonHelper.ReturnMsg(false, "参数错误：文件不存在!"));
            }
            var serverPath = Server.MapPath(filePath);
            if (Directory.Exists(serverPath) == false)
            {
                Directory.CreateDirectory(serverPath);
            }

            var filename = string.Format("{0}-{1}-{2}", DateTime.Now.ToString("yyyyMMddHHmmss"), Guid.NewGuid(),file.FileName);
            file.SaveAs(serverPath + "/" + filename);

            return Content(JsonHelper.ReturnMsg(true, "", 
                new
            {
                fileFullPath = Path.Combine(filePath, filename), 
                fileName = filename, 
                fileUrl = filePath,
                orgfileName = file.FileName
            }));
        }

    }
}
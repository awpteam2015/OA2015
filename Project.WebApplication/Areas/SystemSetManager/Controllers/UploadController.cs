using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project.Infrastructure.FrameworkCore.ToolKit.JsonHandler;

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
            var imagePath = Path.Combine(ConfigurationManager.AppSettings["UploadFile"], "Image");

            var file = Request.Files["Filedata"];
            if (file == null)
            {
                return Content(JsonHelper.ReturnMsg(false, "参数错误：文件不存在!"));
            }
            var serverPath = Server.MapPath(imagePath);
            if (Directory.Exists(serverPath) == false)
            {
                Directory.CreateDirectory(serverPath);
            }

            var filename = string.Format("{0}-{1}-{2}", "", Guid.NewGuid(), file.FileName);
            file.SaveAs(serverPath + "/" + filename);

            return Content(JsonHelper.ReturnMsg(true, "", new { imagePath = Path.Combine(imagePath, filename), fileName = filename, dataPath = Path.Combine("Image", filename) }));
        }

    }
}
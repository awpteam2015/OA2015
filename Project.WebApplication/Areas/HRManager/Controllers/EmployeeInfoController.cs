

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Aspose.Cells;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.ToolKit.JsonHandler;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;
using Project.Infrastructure.FrameworkCore.ToolKit.StringHandler;
using Project.Model.HRManager;
using Project.Service.HRManager;
using Project.WebApplication.Controllers;
using Project.Infrastructure.FrameworkCore.ToolKit;
using Project.Infrastructure.FrameworkCore.ToolKit.Extensions;
using Project.Infrastructure.FrameworkCore.WebMvc.Controllers.Results;
using Project.Infrastructure.FrameworkCore.WebMvc.Models;
using Project.Service.PermissionManager;
using Project.Service.HRManager.Validate;


namespace Project.WebApplication.Areas.HRManager.Controllers
{
    public class EmployeeInfoController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = EmployeeInfoService.GetInstance().GetModelByPk(pkId);
                ViewBag.BindEntity = JsonHelper.JsonSerializer(entity);
            }
            else
            {
                var maxCode = ((TypeParse.StrToInt(EmployeeInfoService.GetInstance().GetMaxEmployeeCode(), 0) + 1) + "").PadLeft(8, '0');
                ViewBag.BindEntity = JsonHelper.JsonSerializer(new EmployeeInfoEntity()
                {
                    EmployeeCode = maxCode,
                    Duties = "0",
                    EmployeeType = "0",
                    Sex = 0,
                    WorkingYears = 1,
                    WorkState = "1",
                    State = 1
                });
            }
            return View();
        }

        public ActionResult List()
        {
            return View();
        }

        public AbpJsonResult GetList()
        {
            var pIndex = this.Request["page"].ConvertTo<int>();
            var pSize = this.Request["rows"].ConvertTo<int>();
            var where = new EmployeeInfoEntity();
            //where.PkId = RequestHelper.GetFormString("PkId");
            where.EmployeeCode = RequestHelper.GetFormString("EmployeeCode");
            where.EmployeeName = RequestHelper.GetFormString("EmployeeName");
            where.DepartmentCode = RequestHelper.GetFormString("DepartmentCode");
            where.DepartmentCode = string.Join(",", (DepartmentService.GetInstance().GetChiledArr(where.DepartmentCode)));
            where.JobName = RequestHelper.GetFormString("JobName");
            //where.PayCode = RequestHelper.GetFormString("PayCode");
            //where.Sex = RequestHelper.GetFormString("Sex");
            //where.CertNo = RequestHelper.GetFormString("CertNo");
            //where.Birthday = RequestHelper.GetFormString("Birthday");
            //where.TechnicalTitle = RequestHelper.GetFormString("TechnicalTitle");
            //where.Duties = RequestHelper.GetFormString("Duties");
            where.WorkState = RequestHelper.GetFormString("WorkState");
            where.EmployeeType = RequestHelper.GetFormString("EmployeeType");
            //where.HomeAddress = RequestHelper.GetFormString("HomeAddress");
            //where.MobileNO = RequestHelper.GetFormString("MobileNO");
            //where.ImageUrl = RequestHelper.GetFormString("ImageUrl");
            //where.Sort = RequestHelper.GetFormString("Sort");
            //where.State = RequestHelper.GetFormString("State");
            //where.Remark = RequestHelper.GetFormString("Remark");
            //where.CreatorUserCode = RequestHelper.GetFormString("CreatorUserCode");
            //where.CreatorUserName = RequestHelper.GetFormString("CreatorUserName");
            //where.CreateTime = RequestHelper.GetFormString("CreateTime");
            //where.LastModificationTime = RequestHelper.GetFormString("LastModificationTime");
            var searchList = EmployeeInfoService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver(new string[] { "result" }));
        }

        public AbpJsonResult GetAllList()
        {
            var where = new EmployeeInfoEntity();
            where.DepartmentCode = RequestHelper.QueryString["DepartmentCode"];
            where.DepartmentCode = string.Join(",", (DepartmentService.GetInstance().GetChiledArr(where.DepartmentCode)));
            //where.DepartmentCode = RequestHelper.GetFormString("DepartmentCode");
            var searchList = EmployeeInfoService.GetInstance().GetList(where, true);


            return new AbpJsonResult(searchList, new NHibernateContractResolver());
        }

        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<EmployeeInfoEntity> postData)
        {
            //var where = new EmployeeInfoEntity();
            //where.EmployeeCode = postData.RequestEntity.EmployeeCode;
            //EmployeeInfoService.GetInstance().GetList(where);
            postData.RequestEntity.CreatorUserCode = LoginUserInfo.UserCode;
            postData.RequestEntity.CreatorUserName = LoginUserInfo.UserName;
            postData.RequestEntity.CreationTime = DateTime.Now;
            if (!string.IsNullOrEmpty(postData.RequestEntity.EmployeeName))
            {
                postData.RequestEntity.PayCode = Chinese.GetFirstPinYin(postData.RequestEntity.EmployeeName).ToUpper();
            }
            var addResult = EmployeeInfoService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<EmployeeInfoEntity>()
            {
                success = addResult.Item1,
                result = postData.RequestEntity,
                error = addResult.Item1 ? null : new ErrorInfo(addResult.Item2)
            };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit(AjaxRequest<EmployeeInfoEntity> postData)
        {
            postData.RequestEntity.LastModificationTime = DateTime.Now;
            if (!string.IsNullOrEmpty(postData.RequestEntity.EmployeeName))
            {
                postData.RequestEntity.PayCode = Chinese.GetFirstPinYin(postData.RequestEntity.EmployeeName).ToUpper();
            }
            var updateResult = EmployeeInfoService.GetInstance().Update(postData.RequestEntity);
            var result = new AjaxResponse<EmployeeInfoEntity>()
            {
                success = updateResult.Item1,
                result = postData.RequestEntity,
                error = updateResult.Item1 ? null : new ErrorInfo(updateResult.Item2)
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = EmployeeInfoService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<EmployeeInfoEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }


        public AbpJsonResult GetEmployeeInfo(string employeeCode)
        {

            var list = EmployeeInfoService.GetInstance().GetList(new EmployeeInfoEntity() { EmployeeCode = employeeCode });

            if (list.Any())
            {
                var result = new AjaxResponse<EmployeeInfoEntity>()
                {
                    success = true,
                    result = list.FirstOrDefault()
                };
                return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
            }
            else
            {
                return new AbpJsonResult(new AjaxResponse<string>() { success = false, error = new ErrorInfo() { message = "请输入正确的员工号！" } });
            }


        }

        public ActionResult Up(int pkId = 0)
        {
            //var entity = EmployeeInfoService.GetInstance().GetModelByPk(pkId);
            //ViewBag.BindEntity = JsonHelper.JsonSerializer(entity);
            return View();
        }

        [HttpPost]
        public AbpJsonResult Upload(AjaxRequest<EmployeeInfoEntity> postData)
        {
            var path = Server.MapPath(postData.RequestEntity.FileUrl + "/" + postData.RequestEntity.FileName);

            Workbook workbook = new Workbook(path);
            Worksheet sheet = workbook.Worksheets[0];
            Cells cells = sheet.Cells;
            int sucessNum = 0, failNum = 0;
            for (int i = 1; i < cells.MaxDataRow + 1; i++)
            {
                //var row = new AttendanceEntity();
                //postData.RequestEntity.DepartmentCode = cells[i, 0].StringValue.Trim();
                //postData.RequestEntity.DepartmentName = DepartmentService.GetInstance().GetModelByDepartmentCode(postData.RequestEntity.DepartmentCode).DepartmentName;
                postData.RequestEntity.EmployeeCode = cells[i, 2].StringValue.Trim();
                postData.RequestEntity.EmployeeName = cells[i, 1].StringValue.Trim();
                postData.RequestEntity.Sex = cells[i, 3].IntValue;
                postData.RequestEntity.CertNo = cells[i, 4].StringValue.Trim();
                postData.RequestEntity.Birthday = cells[i, 5].DateTimeValue;
                postData.RequestEntity.WorkState = cells[i, 6].StringValue.Trim();
                postData.RequestEntity.WorkStateName = DictionaryService.GetInstance().GetModelByKeyCode("ZZZT", postData.RequestEntity.WorkState).KeyName;
                postData.RequestEntity.EmployeeType = cells[i, 7].StringValue.Trim();
                postData.RequestEntity.EmployeeTypeName = DictionaryService.GetInstance().GetModelByKeyCode("YGLY", postData.RequestEntity.EmployeeType).KeyName;
                postData.RequestEntity.Duties = cells[i, 8].StringValue;
                postData.RequestEntity.DutiesName = DictionaryService.GetInstance().GetModelByKeyCode("DWZW", postData.RequestEntity.Duties).KeyName; ;
                postData.RequestEntity.WorkingYears = cells[i, 9].IntValue;
                postData.RequestEntity.MobileNO = cells[i, 10].StringValue.Trim();
                postData.RequestEntity.HomeAddress = cells[i, 11].StringValue.Trim();
                postData.RequestEntity.State = 1;
                Tuple<bool, string> addResult;
                var oldentity = EmployeeInfoValidate.GetInstance().GetModelByCertNo(postData.RequestEntity.CertNo);
                if (oldentity != null && oldentity.PkId > 0)
                {
                    postData.RequestEntity.PkId = oldentity.PkId;
                    addResult = EmployeeInfoService.GetInstance().Update(postData.RequestEntity);
                }
                else
                    addResult = EmployeeInfoService.GetInstance().Add(postData.RequestEntity);
                if (addResult.Item1)
                    sucessNum++;
                else
                    failNum++;
            }

            var result = new AjaxResponse<EmployeeInfoEntity>()
            {
                success = true,
                error = new ErrorInfo(string.Format("成功条数：{0},失败条数：{1}", sucessNum, failNum))
            };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }

        public AbpJsonResult ExportExcel()
        {
            var filepath = "/UploadFile/Temp/";
            var rootpath = this.Request.MapPath("/");
            var fileName = DateTime.Now.ToString("yyyyMMHHddmmssfff") + ".xls";

            var where = new EmployeeInfoEntity();
            //where.DepartmentCode = RequestHelper.GetFormString("DepartmentCode");
            var searchList = EmployeeInfoService.GetInstance().GetList(where, false);


            var bl = AsposeCellsHelper.ExportToExcel<IList<EmployeeInfoEntity>>(searchList, "EmployeeInfo", $"{rootpath}/TemplateFile/EmployeeExport.xlsx", $"{rootpath}{filepath}{fileName}", new Dictionary<string, object>());



            var result = new AjaxResponse<EmployeeInfoEntity>()
            {
                success = bl,
                targeturl = filepath + fileName
            };
            return new AbpJsonResult(result, new NHibernateContractResolver());

        }


        public AbpJsonResult ExportWord(int employeeId)
        {
            var entity = EmployeeInfoService.GetInstance().GetModelByPk(employeeId);
            var workStr = string.Empty;
            string studyStr = string.Empty;
            string technicalStr = string.Empty;
            string yearStr = string.Empty;
            foreach (var workItem in entity.WorkList)
            {
                workStr += string.Format(@"{0}至{1};{2};{3};<br/>",
                    workItem.BeginDate.HasValue ? workItem.BeginDate.Value.ToString("yyyy年MM月") : "",
                    workItem.EndDate.HasValue ? workItem.EndDate.Value.ToString("yyyy年MM月") : "",
                    workItem.WorkCompany,
                    workItem.Duties);

            }
            foreach (var learningItem in entity.LearningList)
            {
                studyStr += string.Format(@"{0}至{1};{2};{3};<br/>",
                    learningItem.BeginDate.HasValue ? learningItem.BeginDate.Value.ToString("yyyy年MM月") : "",
                    learningItem.EndDate.HasValue ? learningItem.EndDate.Value.ToString("yyyy年MM月") : "",
                    learningItem.School,
                    learningItem.Education);

            }
            foreach (TechnicalEntity technicalEntity in entity.TechnicalList)
            {
                technicalStr += string.Format(@"取得时间：{0};{1};证书编号：{2};<br/>",
                      technicalEntity.GetDate.HasValue ? technicalEntity.GetDate.Value.ToString("yyyy年") : "",
                      technicalEntity.Title,
                      technicalEntity.CerNo);

            }
            foreach (var yearAssessmentEntity in entity.YearAssessmentList)
            {
                yearStr += string.Format(@"{0}年度考核;{1};<br/>",
                      yearAssessmentEntity.KHYear,
                      yearAssessmentEntity.KHComment);

            }
            var filepath = "/UploadFile/Temp/";
            var rootpath = this.Request.MapPath("/");
            var fileName = entity.EmployeeName + "_人员档案" + ".doc";
            Dictionary<string, string> dict = new Dictionary<string, string>();
            dict.Add("EmployeeCode", entity.EmployeeCode);
            dict.Add("EmployeeName", entity.EmployeeName);
            dict.Add("SexName", entity.Sex.HasValue ? (entity.Sex.Value == 1 ? "男" : "女") : "未知");
            dict.Add("CertNo", entity.CertNo);
            dict.Add("Birthday", entity.Birthday.HasValue ? entity.Birthday.Value.ToString("yyyy-MM-dd") : "");
            dict.Add("StartWork", entity.StartWork.HasValue ? entity.StartWork.Value.ToString("yyyy-MM-dd") : "");
            dict.Add("DepartmentName", entity.DepartmentName);
            dict.Add("DutiesName", entity.DutiesName);
            dict.Add("EmployeeTypeName", entity.EmployeeTypeName);
            dict.Add("PostLevelName", entity.PostLevelName);
            dict.Add("PostPropertyName", entity.PostPropertyName);
            dict.Add("MobileNO", string.IsNullOrEmpty(entity.MobileNO) ? "" : entity.MobileNO);
            dict.Add("HomeAddress", string.IsNullOrEmpty(entity.HomeAddress) ? "" : entity.HomeAddress);
            dict.Add("JoinCommy", entity.JoinCommy.HasValue ? entity.JoinCommy.Value.ToString("yyyy-MM-dd") : "");
            dict.Add("WorkList", workStr);
            dict.Add("StudyList", studyStr);
            dict.Add("ProfessionList", technicalStr);
            dict.Add("YearList", yearStr);
            var imagePath = rootpath + entity.FileUrl + "/" + entity.FileName;

            if (string.IsNullOrEmpty(entity.FileName))
            {
                imagePath = rootpath + "/Content/images/NoImage.jpg";
            }
            if (System.IO.File.Exists(imagePath) && AsposeWordsHelper.IsImage(imagePath))
            {
                //存在
            }
            else
            {
                imagePath = rootpath + "/Content/images/NoImage.jpg";
            }
            dict.Add("IMGPhoto", imagePath.Length > 0 ? imagePath : "");

            AsposeWordsHelper.SaveWordByTemplate($"{rootpath}/TemplateFile/EmployeeTemplate.doc", dict, $"{rootpath}{filepath}{fileName}", 78, 140);
            var result = new AjaxResponse<EmployeeInfoEntity>()
            {
                success = true,
                targeturl = filepath + fileName
            };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        public ActionResult Look(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = EmployeeInfoService.GetInstance().GetModelByPk(pkId);
                ViewBag.BindEntity = JsonHelper.JsonSerializer(entity);
            }
            else
            {
                // var maxCode = ((TypeParse.StrToInt(EmployeeInfoService.GetInstance().GetMaxEmployeeCode(), 0) + 1) + "").PadLeft(8, '0');
                ViewBag.BindEntity = JsonHelper.JsonSerializer(new EmployeeInfoEntity()
                {
                    EmployeeCode = "",
                    Duties = "0",
                    EmployeeType = "0",
                    Sex = 0,
                    WorkingYears = 1,
                    WorkState = "1",
                    State = 1
                });
            }
            return View();
        }

    }
}





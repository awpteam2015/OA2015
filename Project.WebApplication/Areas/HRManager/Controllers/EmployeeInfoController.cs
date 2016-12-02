

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using Aspose.Cells;
using AutoMapper;
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
                    Duties = "1",
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
            where.DepartmentCode = string.Join(",", (DepartmentService.GetInstance().GetChiledArr(where.DepartmentCode, LoginUserInfo.UserDepartmentList.ToList(), LoginUserInfo.IsAdmin)));
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
            where.DepartmentCode = string.Join(",", (DepartmentService.GetInstance().GetChiledArr(where.DepartmentCode, LoginUserInfo.UserDepartmentList.ToList(), LoginUserInfo.IsAdmin)));
            //where.DepartmentCode = RequestHelper.GetFormString("DepartmentCode");
            var searchList = EmployeeInfoService.GetInstance().GetList(where, true);


            return new AbpJsonResult(searchList, new NHibernateContractResolver());
        }

        public AbpJsonResult GetAllList2()
        {
            var where = new EmployeeInfoEntity();
            var searchList = EmployeeInfoService.GetInstance().GetList(where, false);
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

            postData.RequestEntity.LastModifierUserCode = LoginUserInfo.UserCode;
            postData.RequestEntity.LastModifierUserName = LoginUserInfo.UserName;
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

        public AbpJsonResult ExportExcel()
        {
            var filepath = "/UploadFile/Temp/";
            var rootpath = this.Request.MapPath("/");
            var fileName = DateTime.Now.ToString("yyyyMMHHddmmssfff") + ".xls";

            var where = new EmployeeInfoEntity();
            where.DepartmentCode = string.Join(",", (DepartmentService.GetInstance().GetChiledArr(where.DepartmentCode, LoginUserInfo.UserDepartmentList.ToList(), LoginUserInfo.IsAdmin)));

            //where.DepartmentCode = RequestHelper.GetFormString("DepartmentCode");
            var searchList = EmployeeInfoService.GetInstance().GetList(where, false);
            searchList.ForEach(p => p.SexName = (p.Sex == 0 ? "女" : "男"));
            var bl = AsposeCellsHelper.ExportToExcel<IList<EmployeeInfoEntity>>(searchList, "EmployeeInfo", $"{rootpath}/TemplateFile/EmployeeExport.xlsx", $"{rootpath}{filepath}{fileName}", new Dictionary<string, object>());
            //var bl = true;

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

        [HttpPost]
        public AbpJsonResult Upload(AjaxRequest<EmployeeInfoEntity> postData)
        {
            var path = Server.MapPath(postData.RequestEntity.FileUrl + "/" + postData.RequestEntity.FileName);

            Workbook workbook = new Workbook(path);

            var tipInfo = string.Empty;
            var ret = false;
            var tuple = new Tuple<bool, string>(true, "");
            try
            {

                for (int j = 0; j < workbook.Worksheets.Count; j++)
                {
                    var sheet = workbook.Worksheets[j];
                    switch (j)
                    {
                        case 0:
                            tuple = ImportExcelBaseInfo(sheet, postData.RequestEntity);//导入基本信息
                            tipInfo += tuple.Item2;
                            break;
                        case 1:
                            tuple = ImportExcelWorkInfo(sheet, postData.RequestEntity);//导入工作经历
                            tipInfo += tuple.Item2;
                            break;
                        case 2:
                            tuple = ImportExcelLearnInfo(sheet, postData.RequestEntity);//学习经历
                            tipInfo += tuple.Item2;
                            break;
                        case 3:
                            tuple = ImportExcelContinEducationInfo(sheet, postData.RequestEntity);//继续教育学分
                            tipInfo += tuple.Item2;
                            break;
                        case 4:
                            tuple = ImportExcelTechnicalInfo(sheet, postData.RequestEntity);//职称记录
                            tipInfo += tuple.Item2;
                            break;
                        case 5:
                            tuple = ImportExcelProfessionInfo(sheet, postData.RequestEntity);//专业技术
                            tipInfo += tuple.Item2;
                            break;
                        case 6:
                            tuple = ImportExcelYearAssessmentInfo(sheet, postData.RequestEntity);//年度考核
                            tipInfo += tuple.Item2;
                            break;
                        case 7:
                            tuple = ImportExcelWageInfo(sheet, postData.RequestEntity);//人事工资
                            tipInfo += tuple.Item2;
                            break;
                            
                    }

                }

                ret = true;

            }
            catch (Exception)
            {
                ret = false;
            }

            var result = new AjaxResponse<EmployeeInfoEntity>()
            {
                success = ret,
                error = new ErrorInfo() { message = tipInfo }
            };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }

        /// <summary>
        /// 基本信息经历
        /// </summary>
        /// <param name="baseSheet"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private Tuple<bool, string> ImportExcelBaseInfo(Worksheet baseSheet, EmployeeInfoEntity model)
        {
            int sucessNum = 0, failNum = 0;
            try
            {
                Cells cells = baseSheet.Cells;

                var newModel = new EmployeeInfoEntity();
                for (int i = 1; i < cells.MaxDataRow + 1; i++)
                {
                    //var row = new AttendanceEntity();
                    //postData.RequestEntity.DepartmentCode = cells[i, 0].StringValue.Trim();
                    //postData.RequestEntity.DepartmentName = DepartmentService.GetInstance().GetModelByDepartmentCode(postData.RequestEntity.DepartmentCode).DepartmentName;
                    var insertModel = new EmployeeInfoEntity() { };
                    // insertModel = Mapper.Map(newModel, model);
                    insertModel.DepartmentCode = model.DepartmentCode;
                    insertModel.DepartmentName = model.DepartmentName;
                    insertModel.EmployeeCode = cells[i, 1].StringValue.Trim();
                    insertModel.EmployeeName = cells[i, 0].StringValue.Trim();
                    insertModel.JobName = cells[i, 2].StringValue.Trim();
                    insertModel.Sex = cells[i, 3].IntValue;
                    insertModel.CertNo = cells[i, 4].StringValue.Trim();
                    if (cells[i, 5].StringValue.Length > 0)
                        insertModel.Birthday = cells[i, 5].StringValue.ToDateTime();
                    if (cells[i, 6].StringValue.Length > 0)
                        insertModel.StartWork = cells[i, 6].StringValue.ToDateTime();
                    if (cells[i, 7].StringValue.Length > 0)
                        insertModel.IntoCompanyTime = cells[i, 7].StringValue.ToDateTime();
                    insertModel.Duties = cells[i, 8].StringValue;
                    if (insertModel.Duties.Trim().Length > 0)
                    {
                        var temp = DictionaryService.GetInstance().GetModelByKeyCode("DWZW", insertModel.Duties);
                        if (temp != null)
                            insertModel.DutiesName = temp.KeyName;

                    };
                    insertModel.PostProperty = cells[i, 9].StringValue.Trim();
                    insertModel.PostPropertyName = DictionaryService.GetInstance().GetModelByKeyCode("GWXZ", insertModel.PostProperty).KeyName;
                    if (cells[i, 10].StringValue.Length > 0)
                        insertModel.JoinCommy = cells[i, 10].StringValue.ToDateTime(); ;
                    insertModel.WorkState = cells[i, 11].StringValue.Trim();
                    insertModel.WorkStateName = DictionaryService.GetInstance().GetModelByKeyCode("ZZZT", insertModel.WorkState).KeyName;
                    insertModel.EmployeeType = cells[i, 12].StringValue.Trim();
                    insertModel.EmployeeTypeName = DictionaryService.GetInstance().GetModelByKeyCode("YGLY", insertModel.EmployeeType).KeyName;
                    insertModel.PostLevel = cells[i, 13].StringValue.Trim();
                    insertModel.PostLevelName = DictionaryService.GetInstance().GetModelByKeyCode("GWDJ", insertModel.PostLevel).KeyName;

                    insertModel.PoliticsName = cells[i, 14].StringValue.Trim();
                    insertModel.MobileNO = cells[i, 15].StringValue.Trim();
                    insertModel.HomeAddress = cells[i, 16].StringValue.Trim();
                    insertModel.State = 1;
                    insertModel.LastModifierUserCode = LoginUserInfo.UserCode;
                    insertModel.LastModifierUserName = LoginUserInfo.UserName;
                    Tuple<bool, string> addResult;
                    var oldentity = EmployeeInfoValidate.GetInstance().GetModelByCertNo(insertModel.CertNo);
                    if (oldentity != null && oldentity.PkId > 0)
                    {
                        insertModel.PkId = oldentity.PkId;
                        addResult = EmployeeInfoService.GetInstance().Update(insertModel);
                    }
                    else
                    {
                        insertModel.CreatorUserName = LoginUserInfo.UserName;
                        addResult = EmployeeInfoService.GetInstance().Add(insertModel);
                    }
                    if (addResult.Item1)
                        sucessNum++;
                    else
                        failNum++;
                }
                return new Tuple<bool, string>(true, " 基本信息成功：" + sucessNum + "失败：" + failNum);
            }
            catch (Exception ex)
            {
                return new Tuple<bool, string>(false, string.Format(" 基本信息成功：{0},失败：{1}过程出错：{2}", sucessNum, failNum, ex.Message));
            }

        }

        /// <summary>
        /// 工作经历
        /// </summary>
        /// <param name="baseSheet"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private Tuple<bool, string> ImportExcelWorkInfo(Worksheet baseSheet, EmployeeInfoEntity model)
        {
            int sucessNum = 0, failNum = 0;
            try
            {
                Cells cells = baseSheet.Cells;
                for (int i = 1; i < cells.MaxDataRow + 1; i++)
                {
                    model.EmployeeCode = cells[i, 0].StringValue.Trim();
                    var tempmodel = EmployeeInfoService.GetInstance().GetEmployeeNameByCode2(model.EmployeeCode);
                    if (tempmodel == null)
                        continue;

                    var workModel = new WorkExperienceEntity();
                    workModel.WorkCompany = cells[i, 1].StringValue.Trim();
                    workModel.Duties = cells[i, 2].StringValue.Trim();
                    if (cells[i, 3].StringValue.Length > 0)
                        workModel.BeginDate = DateTime.Parse(cells[i, 3].StringValue);
                    if (cells[i, 4].StringValue.Length > 0)
                        workModel.EndDate = DateTime.Parse(cells[i, 4].StringValue);
                    workModel.WorkContent = cells[i, 5].StringValue.Trim();
                    workModel.EmployeeID = tempmodel.PkId;
                    if (WorkExperienceService.GetInstance().Add(workModel) > 0)
                        sucessNum++;
                    else
                        failNum++;
                }
                return new Tuple<bool, string>(true, " 工作经历成功：" + sucessNum + "失败：" + failNum);
            }
            catch (Exception ex)
            {
                return new Tuple<bool, string>(false, string.Format(" 工作经历成功：{0},失败：{1}过程出错：{2}", sucessNum, failNum, ex.Message));
            }

        }

        /// <summary>
        /// 学习经历
        /// </summary>
        /// <param name="baseSheet"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private Tuple<bool, string> ImportExcelLearnInfo(Worksheet baseSheet, EmployeeInfoEntity model)
        {
            int sucessNum = 0, failNum = 0;
            try
            {
                Cells cells = baseSheet.Cells;
                for (int i = 1; i < cells.MaxDataRow + 1; i++)
                {
                    model.EmployeeCode = cells[i, 0].StringValue.Trim();
                    var tempmodel = EmployeeInfoService.GetInstance().GetEmployeeNameByCode2(model.EmployeeCode);
                    if (tempmodel == null)
                        continue;

                    var learnModel = new LearningExperiencesEntity();
                    learnModel.School = cells[i, 1].StringValue.Trim();
                    learnModel.ProfessionCode = cells[i, 2].StringValue.Trim();
                    learnModel.Degree = cells[i, 3].StringValue.Trim();
                    learnModel.Education = cells[i, 4].StringValue.Trim();
                    learnModel.SchoolYear = cells[i, 5].StringValue.Trim();
                    learnModel.CertNumber = cells[i, 6].StringValue.Trim();
                    if (cells[i, 7].StringValue.Length > 0)
                        learnModel.BeginDate = cells[i, 7].StringValue.ToDateTime();// cells[i, 7].DateTimeValue;
                    if (cells[i, 8].StringValue.Length > 0)
                        learnModel.EndDate = cells[i, 8].StringValue.ToDateTime();
                    learnModel.Remark = cells[i, 9].StringValue.Trim();
                    learnModel.EmployeeID = tempmodel.PkId;
                    if (LearningExperiencesService.GetInstance().Add(learnModel) > 0)
                        sucessNum++;
                    else
                        failNum++;
                }
                return new Tuple<bool, string>(true, " 学习经历成功：" + sucessNum + "失败：" + failNum);
            }
            catch (Exception ex)
            {
                return new Tuple<bool, string>(false, string.Format(" 学习经历成功：{0},失败：{1}过程出错：{2}", sucessNum, failNum, ex.Message));
            }

        }

        /// <summary>
        /// 继续教育学分
        /// </summary>
        /// <param name="baseSheet"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private Tuple<bool, string> ImportExcelContinEducationInfo(Worksheet baseSheet, EmployeeInfoEntity model)
        {
            int sucessNum = 0, failNum = 0;
            try
            {
                Cells cells = baseSheet.Cells;
                for (int i = 1; i < cells.MaxDataRow + 1; i++)
                {
                    model.EmployeeCode = cells[i, 0].StringValue.Trim();
                    var tempmodel = EmployeeInfoService.GetInstance().GetEmployeeNameByCode2(model.EmployeeCode);
                    if (tempmodel == null)
                        continue;

                    var continEducationEntity = new ContinEducationEntity();
                    continEducationEntity.CreditType = cells[i, 1].StringValue.Trim();
                    continEducationEntity.Score = cells[i, 2].StringValue.Trim();
                    if (cells[i, 3].StringValue.Length > 0)
                        continEducationEntity.GetTime = cells[i, 3].StringValue.ToDateTime();
                    continEducationEntity.EmployeeID = tempmodel.PkId;
                    if (ContinEducationService.GetInstance().Add(continEducationEntity) > 0)
                        sucessNum++;
                    else
                        failNum++;
                }
                return new Tuple<bool, string>(true, " 继续教育学分成功：" + sucessNum + "失败：" + failNum);
            }
            catch (Exception ex)
            {
                return new Tuple<bool, string>(false, string.Format(" 继续教育学分成功：{0},失败：{1}过程出错：{2}", sucessNum, failNum, ex.Message));
            }
        }

        /// <summary>
        /// 职称记录
        /// </summary>
        /// <param name="baseSheet"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private Tuple<bool, string> ImportExcelTechnicalInfo(Worksheet baseSheet, EmployeeInfoEntity model)
        {
            int sucessNum = 0, failNum = 0;
            try
            {
                Cells cells = baseSheet.Cells;
                for (int i = 1; i < cells.MaxDataRow + 1; i++)
                {
                    model.EmployeeCode = cells[i, 0].StringValue.Trim();
                    var tempmodel = EmployeeInfoService.GetInstance().GetEmployeeNameByCode2(model.EmployeeCode);
                    if (tempmodel == null)
                        continue;

                    var technicalEntity = new TechnicalEntity();
                    technicalEntity.Title = cells[i, 1].StringValue.Trim();
                    technicalEntity.LevNum = cells[i, 2].StringValue.Trim();
                    if (cells[i, 3].StringValue.Length > 0)
                        technicalEntity.GetDate = cells[i, 3].StringValue.ToDateTime();
                    technicalEntity.CerNo = cells[i, 4].StringValue.Trim();
                    technicalEntity.EmployeeID = tempmodel.PkId;
                    //关闭聘用时间等等
                    //if (cells[i, 5].StringValue.Length > 0)
                    //    technicalEntity.EmployDate = cells[i, 5].StringValue.ToDateTime();
                    //if (cells[i, 6].StringValue.Length > 0)
                    //    technicalEntity.EmployEndDate = cells[i, 6].StringValue.ToDateTime();
                    if (TechnicalService.GetInstance().Add(technicalEntity) > 0)
                        sucessNum++;
                    else
                        failNum++;
                }
                return new Tuple<bool, string>(true, " 职称记录成功：" + sucessNum + "失败：" + failNum);
            }
            catch (Exception ex)
            {
                return new Tuple<bool, string>(false, string.Format(" 职称记录成功：{0},失败：{1}过程出错：{2}", sucessNum, failNum, ex.Message));
            }

        }

        /// <summary>
        /// 专业技术
        /// </summary>
        /// <param name="baseSheet"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private Tuple<bool, string> ImportExcelProfessionInfo(Worksheet baseSheet, EmployeeInfoEntity model)
        {
            int sucessNum = 0, failNum = 0;
            try
            {
                Cells cells = baseSheet.Cells;
                for (int i = 1; i < cells.MaxDataRow + 1; i++)
                {
                    model.EmployeeCode = cells[i, 0].StringValue.Trim();
                    var tempmodel = EmployeeInfoService.GetInstance().GetEmployeeNameByCode2(model.EmployeeCode);
                    if (tempmodel == null)
                        continue;

                    var professionEntity = new ProfessionEntity();
                    professionEntity.Title = cells[i, 1].StringValue.Trim();
                    professionEntity.TypeName = cells[i, 2].StringValue.Trim();
                    professionEntity.RangeName = cells[i, 3].StringValue.Trim();
                    if (cells[i, 4].StringValue.Length > 0)
                        professionEntity.GetDate = cells[i, 4].StringValue.ToDateTime();
                    professionEntity.CerNo = cells[i, 5].StringValue.Trim();
                    if (cells[i, 6].StringValue.Length > 0)
                        professionEntity.EmployDate = cells[i, 6].StringValue.ToDateTime();
                    if (cells[i, 7].StringValue.Length > 0)
                        professionEntity.EmployEndDate = cells[i, 7].StringValue.ToDateTime();
                    professionEntity.EmployeeID = tempmodel.PkId;
                    if (ProfessionService.GetInstance().Add(professionEntity) > 0)
                        sucessNum++;
                    else
                        failNum++;
                }
                return new Tuple<bool, string>(true, " 专业技术成功：" + sucessNum + "失败：" + failNum);
            }
            catch (Exception ex)
            {
                return new Tuple<bool, string>(false, string.Format(" 专业技术成功：{0},失败：{1}过程出错：{2}", sucessNum, failNum, ex.Message));
            }
        }

        /// <summary>
        /// 年度考核
        /// </summary>
        /// <param name="baseSheet"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private Tuple<bool, string> ImportExcelYearAssessmentInfo(Worksheet baseSheet, EmployeeInfoEntity model)
        {
            int sucessNum = 0, failNum = 0;
            try
            {
                Cells cells = baseSheet.Cells;
                for (int i = 1; i < cells.MaxDataRow + 1; i++)
                {
                    model.EmployeeCode = cells[i, 0].StringValue.Trim();
                    var tmpmodel = EmployeeInfoService.GetInstance().GetEmployeeNameByCode2(model.EmployeeCode);
                    if (tmpmodel == null)
                        continue;

                    var yearAssessmentEntity = new YearAssessmentEntity();
                    yearAssessmentEntity.KHYear = cells[i, 1].StringValue.Trim();
                    yearAssessmentEntity.KHComment = cells[i, 2].StringValue.Trim();
                    yearAssessmentEntity.EmployeeID = tmpmodel.PkId;
                    if (YearAssessmentService.GetInstance().Add(yearAssessmentEntity) > 0)
                        sucessNum++;
                    else
                        failNum++;
                }
                return new Tuple<bool, string>(true, " 年度考核成功：" + sucessNum + "失败：" + failNum);
            }
            catch (Exception ex)
            {
                return new Tuple<bool, string>(false, string.Format(" 年度考核成功：{0},失败：{1}过程出错：{2}", sucessNum, failNum, ex.Message));
            }
        }


        /// <summary>
        /// 人事工资
        /// </summary>
        /// <param name="baseSheet"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        private Tuple<bool, string> ImportExcelWageInfo(Worksheet baseSheet, EmployeeInfoEntity model)
        {
            int sucessNum = 0, failNum = 0;
            try
            {
                Cells cells = baseSheet.Cells;
                for (int i = 1; i < cells.MaxDataRow + 1; i++)
                {
                    model.EmployeeCode = cells[i, 0].StringValue.Trim();
                    var tmpmodel = EmployeeInfoService.GetInstance().GetEmployeeNameByCode2(model.EmployeeCode);
                    if (tmpmodel == null)
                        continue;

                    var ygWageEntity = new YGWageEntity();
                    ygWageEntity.GWGZ = cells[i, 1].StringValue.Trim();
                    ygWageEntity.XZGZ = cells[i, 2].StringValue.Trim();
                    ygWageEntity.EmployeeID = tmpmodel.PkId;
                    if (YGWageService.GetInstance().Add(ygWageEntity) > 0)
                        sucessNum++;
                    else
                        failNum++;
                }
                return new Tuple<bool, string>(true, " 人事工资成功：" + sucessNum + "失败：" + failNum);
            }
            catch (Exception ex)
            {
                return new Tuple<bool, string>(false, string.Format(" 人事工资成功：{0},失败：{1}过程出错：{2}", sucessNum, failNum, ex.Message));
            }
        }
    }
}





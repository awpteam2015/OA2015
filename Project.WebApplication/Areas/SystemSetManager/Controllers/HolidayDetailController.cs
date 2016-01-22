using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.ToolKit.JsonHandler;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;
using Project.Model.SystemSetManager;
using Project.Mvc.Controllers.Results;
using Project.Mvc.Models;
using Project.Service.SystemSetManager;
using Project.WebApplication.Controllers;
using Project.Infrastructure.FrameworkCore.ToolKit;

namespace Project.WebApplication.Areas.SystemSetManager.Controllers
{
    public class HolidayDetailController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = HolidayDetailService.GetInstance().GetModelByPk(pkId);
                ViewBag.BindEntity = JsonHelper.JsonSerializer(entity);
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
            var where = new HolidayDetailEntity();
            //where.PkId = RequestHelper.GetFormString("PkId");
            where.HolidayName = RequestHelper.GetFormString("HolidayName");
            where.HolidayDateType = RequestHelper.GetFormInt("HolidayDateType", -1);
            where.HolidayDate = RequestHelper.GetFormDateTime("HolidayDate");
            where.HolidayDateEnd = RequestHelper.GetFormDateTime("HolidayDateEnd");

            //where.Remark = RequestHelper.GetFormString("Remark");
            //where.CreatorUserCode = RequestHelper.GetFormString("CreatorUserCode");
            //where.CreatorUserName = RequestHelper.GetFormString("CreatorUserName");
            //where.CreateTime = RequestHelper.GetFormString("CreateTime");
            //where.LastModificationTime = RequestHelper.GetFormString("LastModificationTime");
            var searchList = HolidayDetailService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<HolidayDetailEntity> postData)
        {
            var result = new AjaxResponse<HolidayDetailEntity>()
            {
                success = true,
                result = postData.RequestEntity
            };
            if (postData.RequestEntity.HolidayDate > postData.RequestEntity.HolidayDateEnd)
            {
                result.success = false;
                result.error = new ErrorInfo() { message = "开始时间要小于结束时间" };
                return new AbpJsonResult(result, new NHibernateContractResolver());
            }
            ///是否查询一次本次设置日期已经存在日期
            var where = new HolidayDetailEntity();
            where.HolidayDate = postData.RequestEntity.HolidayDate;
            where.HolidayDateEnd = postData.RequestEntity.HolidayDateEnd;
            var exitList = HolidayDetailService.GetInstance().GetList(where);
            if (exitList.Count > 0)
            {
                result.success = false;
                result.error = new ErrorInfo() { message = "时间段内已经有节假日" };
                return new AbpJsonResult(result, new NHibernateContractResolver());
            }
            postData.RequestEntity.CreateTime = DateTime.Now;
            postData.RequestEntity.CreatorUserCode = LoginUserInfo.UserCode;
            postData.RequestEntity.CreatorUserName = LoginUserInfo.UserName;
            // postData.RequestEntity.Remark = Base64Helper.DecodeBase64(postData.RequestEntity.Remark);
            var addResult = HolidayDetailService.GetInstance().Add(postData.RequestEntity);

            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit(AjaxRequest<HolidayDetailEntity> postData)
        {
            postData.RequestEntity.LastModificationTime = DateTime.Now;
            var updateResult = HolidayDetailService.GetInstance().Update(postData.RequestEntity);
            var result = new AjaxResponse<HolidayDetailEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = HolidayDetailService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<HolidayDetailEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}





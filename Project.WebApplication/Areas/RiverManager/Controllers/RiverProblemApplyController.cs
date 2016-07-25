

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.ToolKit;
using Project.Infrastructure.FrameworkCore.ToolKit.JsonHandler;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;
using Project.Infrastructure.FrameworkCore.WebMvc.Controllers.Results;
using Project.Infrastructure.FrameworkCore.WebMvc.Models;
using Project.Model.RiverManager;
using Project.Service.RiverManager;
using Project.WebApplication.Controllers;

namespace Project.WebApplication.Areas.RiverManager.Controllers
{
    public class RiverProblemApplyController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = RiverProblemApplyService.GetInstance().GetModelByPk(pkId);
                ViewBag.BindEntity = JsonHelper.JsonSerializer(entity);
            }
            return View();
        }


        public ActionResult List()
        {
            return View();
        }

        /// <summary>
        /// 问题督办列表 对已督办的问题进行不同颜色或图标进行标识。
        /// </summary>
        /// <returns></returns>
        public ActionResult DbList()
        {
            return View();
        }


        /// <summary>
        /// 问题曝光
        /// </summary>
        /// <returns></returns>
        public ActionResult BgList()
        {
            return View();
        }

        /// <summary>
        /// 问题删除曝光
        /// </summary>
        /// <returns></returns>
        public ActionResult DeleteList()
        {
            return View();
        }



        public AbpJsonResult GetList()
        {
            var pIndex = this.Request["page"].ConvertTo<int>();
            var pSize = this.Request["rows"].ConvertTo<int>();
            var where = new RiverProblemApplyEntity();
            //where.PkId = RequestHelper.GetFormString("PkId");
            where.Title = RequestHelper.GetFormString("Title");
            //where.Des = RequestHelper.GetFormString("Des");
            //where.ProblemType = RequestHelper.GetFormString("ProblemType");
            //where.PicUrl = RequestHelper.GetFormString("PicUrl");
            where.DepartmentCode = RequestHelper.GetFormString("DepartmentCode");
            //where.RiverId = RequestHelper.GetFormString("RiverId");
            where.RiverName = RequestHelper.GetFormString("RiverName");
            where.UserCode = RequestHelper.GetFormString("UserCode");
            where.UserName = RequestHelper.GetFormString("UserName");
            //where.Coords = RequestHelper.GetFormString("Coords");
            where.State = RequestHelper.GetFormInt("State", 0);

            where.IsUrgent = RequestHelper.GetFormInt("IsUrgent", 2);
            where.IsSendMessage = RequestHelper.GetFormInt("IsSendMessage", 2);
            where.IsExposure = RequestHelper.GetFormInt("IsExposure", 2);

            //where.DepartmentRemark = RequestHelper.GetFormString("DepartmentRemark");
            //where.DepartmentOpTime = RequestHelper.GetFormString("DepartmentOpTime");
            //where.TopDepartmentRemark = RequestHelper.GetFormString("TopDepartmentRemark");
            //where.TopDepartmentOpTime = RequestHelper.GetFormString("TopDepartmentOpTime");
            //where.FinishOpTime = RequestHelper.GetFormString("FinishOpTime");
            //where.FinishRemark = RequestHelper.GetFormString("FinishRemark");
            //where.ReturnRemark = RequestHelper.GetFormString("ReturnRemark");
            //where.ReturnOpTime = RequestHelper.GetFormString("ReturnOpTime");
            //where.IsExposure = RequestHelper.GetFormString("IsExposure");
            //where.ExposureLever = RequestHelper.GetFormString("ExposureLever");
            //where.IsSendMessage = RequestHelper.GetFormString("IsSendMessage");
            //where.IsActive = RequestHelper.GetFormString("IsActive");
            //where.CreatorUserName = RequestHelper.GetFormString("CreatorUserName");
            //where.CreatorUserCode = RequestHelper.GetFormString("CreatorUserCode");
            //where.CreationTime = RequestHelper.GetFormString("CreationTime");
            //where.LastModifierUserName = RequestHelper.GetFormString("LastModifierUserName");
            //where.LastModifierUserCode = RequestHelper.GetFormString("LastModifierUserCode");
            //where.LastModificationTime = RequestHelper.GetFormString("LastModificationTime");
            //where.Remark = RequestHelper.GetFormString("Remark");
            //where.DeleteRemark = RequestHelper.GetFormString("DeleteRemark");
            //where.IsDeleted = RequestHelper.GetFormString("IsDeleted");
            //where.DeleteUserName = RequestHelper.GetFormString("DeleteUserName");
            //where.DeleteUserCode = RequestHelper.GetFormString("DeleteUserCode");
            //where.DeleteTime = RequestHelper.GetFormString("DeleteTime");
            var searchList = RiverProblemApplyService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<RiverProblemApplyEntity> postData)
        {
            postData.RequestEntity.State = 1;
            postData.RequestEntity.Des = Base64Helper.DecodeBase64(postData.RequestEntity.Des);

            var addResult = RiverProblemApplyService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<RiverProblemApplyEntity>()
            {
                success = true,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit(AjaxRequest<RiverProblemApplyEntity> postData)
        {
            if (!string.IsNullOrWhiteSpace(postData.RequestEntity.UrgentRemark))
            {
                postData.RequestEntity.IsUrgent = 1;
            }

            postData.RequestEntity.Des = Base64Helper.DecodeBase64(postData.RequestEntity.Des);
            var newInfo = postData.RequestEntity;
            var orgInfo = RiverProblemApplyService.GetInstance().GetModelByPk(postData.RequestEntity.PkId);
            var mergInfo = Mapper.Map(newInfo, orgInfo);
            var updateResult = RiverProblemApplyService.GetInstance().Update(mergInfo);

            var result = new AjaxResponse<RiverProblemApplyEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = RiverProblemApplyService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<RiverProblemApplyEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}





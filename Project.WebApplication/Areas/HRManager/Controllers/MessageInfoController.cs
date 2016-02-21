

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.ToolKit.JsonHandler;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;
using Project.Model.HRManager;
using Project.Infrastructure.FrameworkCore.WebMvc.Controllers.Results;
using Project.Infrastructure.FrameworkCore.WebMvc.Models;
using Project.Service.HRManager;
using Project.WebApplication.Controllers;
using AutoMapper;
using Project.Infrastructure.FrameworkCore.ToolKit;

namespace Project.WebApplication.Areas.HRManager.Controllers
{
    public class MessageInfoController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = MessageInfoService.GetInstance().GetModelByPk(pkId);
                //修改浏览记录
                if (entity.ReadUser == null || entity.ReadUser.Contains(LoginUserInfo.UserCode + ","))
                {
                    if (entity.ReadUser == null)
                        entity.ReadUser = LoginUserInfo.UserCode + ",";
                    else
                        entity.ReadUser += LoginUserInfo.UserCode + ",";
                    MessageInfoService.GetInstance().Update(entity);
                }
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
            var where = new MessageInfoEntity();
            //where.PkId = RequestHelper.GetFormString("PkId");
            //where.MesTitle = RequestHelper.GetFormString("MesTitle");
            //where.MesContent = RequestHelper.GetFormString("MesContent");
            //where.ReceiveUserCode = RequestHelper.GetFormString("ReceiveUserCode");
            //where.IsAll = RequestHelper.GetFormString("IsAll");
            //where.CreatorUserCode = RequestHelper.GetFormString("CreatorUserCode");
            //where.CreatorUserName = RequestHelper.GetFormString("CreatorUserName");
            where.CreationTime = RequestHelper.GetFormDateTime("CreationTime");
            where.CreationTimeEnd = RequestHelper.GetFormDateTime("CreationTimeEnd");
            //where.LastModificationTime = RequestHelper.GetFormString("LastModificationTime");
            //where.LastModifierUserCode = RequestHelper.GetFormString("LastModifierUserCode");
            //where.DeleterUserCode = RequestHelper.GetFormString("DeleterUserCode");
            //where.DeletionTime = RequestHelper.GetFormString("DeletionTime");
            var searchList = MessageInfoService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<MessageInfoEntity> postData)
        {
            var addResult = MessageInfoService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<MessageInfoEntity>()
            {
                success = true,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit(AjaxRequest<MessageInfoEntity> postData)
        {
            var newInfo = postData.RequestEntity;
            var orgInfo = MessageInfoService.GetInstance().GetModelByPk(postData.RequestEntity.PkId);
            var mergInfo = Mapper.Map(newInfo, orgInfo);
            var updateResult = MessageInfoService.GetInstance().Update(mergInfo);

            var result = new AjaxResponse<MessageInfoEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = MessageInfoService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<MessageInfoEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}





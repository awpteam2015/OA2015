

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.ToolKit.JsonHandler;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;
using Project.Model.HRManager;
using Project.Mvc.Controllers.Results;
using Project.Mvc.Models;
using Project.Service.HRManager;
using Project.WebApplication.Controllers;

namespace Project.WebApplication.Areas.HRManager.Controllers
{
    public class GroupController : BaseController
    {

        public ActionResult Hd(int pkId = 0)
        {
            if (pkId > 0)
            {
                var entity = GroupService.GetInstance().GetModelByPk(pkId);
                ViewBag.BindEntity = JsonHelper.JsonSerializer(entity);
            }
            else
            {
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
            var where = new GroupEntity();
            //where.PkId = RequestHelper.GetFormString("PkId");
            //where.GroupCode = RequestHelper.GetFormString("GroupCode");
            //where.GroupName = RequestHelper.GetFormString("GroupName");
            //where.Sort = RequestHelper.GetFormString("Sort");
            //where.Remark = RequestHelper.GetFormString("Remark");
            //where.CreatorUserCode = RequestHelper.GetFormString("CreatorUserCode");
            //where.CreatorUserName = RequestHelper.GetFormString("CreatorUserName");
            //where.CreateTime = RequestHelper.GetFormString("CreateTime");
            //where.LastModificationTime = RequestHelper.GetFormString("LastModificationTime");
            //where.IsDeleted = RequestHelper.GetFormString("IsDeleted");
            var searchList = GroupService.GetInstance().Search(where, (pIndex - 1) * pSize, pSize);

            var dataGridEntity = new DataGridResponse()
            {
                total = searchList.Item2,
                rows = searchList.Item1
            };
            return new AbpJsonResult(dataGridEntity, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Add(AjaxRequest<GroupEntity> postData)
        {
            postData.RequestEntity.CreatorUserCode = LoginUserInfo.UserCode;
            postData.RequestEntity.CreatorUserName = LoginUserInfo.UserName;
            postData.RequestEntity.CreateTime = DateTime.Now;
            var addResult = GroupService.GetInstance().Add(postData.RequestEntity);
            var result = new AjaxResponse<GroupEntity>()
            {
                success = addResult.Item1,
                result = postData.RequestEntity,
                error = addResult.Item1 ? null : new ErrorInfo(addResult.Item2)
            };
            return new AbpJsonResult(result, new NHibernateContractResolver());
        }


        [HttpPost]
        public AbpJsonResult Edit(AjaxRequest<GroupEntity> postData)
        {
            postData.RequestEntity.LastModificationTime = DateTime.Now;
            var updateResult = GroupService.GetInstance().Update(postData.RequestEntity);
            var result = new AjaxResponse<GroupEntity>()
            {
                success = updateResult,
                result = postData.RequestEntity
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }

        [HttpPost]
        public AbpJsonResult Delete(int pkid)
        {
            var deleteResult = GroupService.GetInstance().DeleteByPkId(pkid);
            var result = new AjaxResponse<GroupEntity>()
            {
                success = deleteResult
            };
            return new AbpJsonResult(result, new NHibernateContractResolver(new string[] { "result" }));
        }
    }
}





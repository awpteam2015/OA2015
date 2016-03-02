
/***************************************************************************
*       功能：     HRGroup业务处理层
*       作者：     ROY
*       日期：     2016-01-09
*       描述：     用于管理组
* *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.HRManager;
using Project.Repository.HRManager;
using System;
using Project.Service.HRManager.Validate;

namespace Project.Service.HRManager
{
    public class GroupService
    {

        #region 构造函数
        private readonly GroupRepository _groupRepository;
        private static readonly GroupService Instance = new GroupService();

        public GroupService()
        {
            this._groupRepository = new GroupRepository();
        }

        public static GroupService GetInstance()
        {
            return Instance;
        }
        #endregion


        #region 基础方法
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Tuple<bool, string> Add(GroupEntity entity)
        {

            var validateResult = GroupValidate.GetInstance().IsHasSameGroupCode(entity.GroupCode);
            if (!validateResult.Item1)
            {
                return validateResult;
            }
            var addResult = _groupRepository.Save(entity);
            if (addResult > 0)
            {
                return new Tuple<bool, string>(true, "");
            }
            else
            {
                return new Tuple<bool, string>(false, "");
            }
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
            try
            {
                var entity = _groupRepository.GetById(pkId);
                _groupRepository.Delete(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        public bool Delete(GroupEntity entity)
        {
            try
            {
                _groupRepository.Delete(entity);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public Tuple<bool, string> Update(GroupEntity entity)
        {
            try
            {
                var validateResult = GroupValidate.GetInstance().IsHasSameGroupCode(entity.GroupCode);
                if (!validateResult.Item1)
                {
                    return validateResult;
                }
                _groupRepository.Update(entity);
                return new Tuple<bool, string>(true, "");
            }
            catch
            {
                return new Tuple<bool, string>(false, "");
            }
        }


        /// <summary>
        /// 通过主键获取实体
        /// </summary>
        /// <param name="pkId">主键</param>
        /// <returns></returns>
        public GroupEntity GetModelByPk(System.Int32 pkId)
        {
            return _groupRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【用于管理组】和总【用于管理组】数</returns>
        public System.Tuple<IList<GroupEntity>, int> Search(GroupEntity where, int skipResults, int maxResults)
        {
            var expr = PredicateBuilder.True<GroupEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            if (!string.IsNullOrEmpty(where.GroupCode))
                expr = expr.And(p => p.GroupCode == where.GroupCode);
            if (!string.IsNullOrEmpty(where.GroupName))
                expr = expr.And(p => p.GroupName == where.GroupName);
            // if (!string.IsNullOrEmpty(where.Sort))
            //  expr = expr.And(p => p.Sort == where.Sort);
            // if (!string.IsNullOrEmpty(where.Remark))
            //  expr = expr.And(p => p.Remark == where.Remark);
            // if (!string.IsNullOrEmpty(where.CreatorUserCode))
            //  expr = expr.And(p => p.CreatorUserCode == where.CreatorUserCode);
            // if (!string.IsNullOrEmpty(where.CreatorUserName))
            //  expr = expr.And(p => p.CreatorUserName == where.CreatorUserName);
            // if (!string.IsNullOrEmpty(where.CreateTime))
            //  expr = expr.And(p => p.CreateTime == where.CreateTime);
            // if (!string.IsNullOrEmpty(where.LastModificationTime))
            //  expr = expr.And(p => p.LastModificationTime == where.LastModificationTime);
            // if (!string.IsNullOrEmpty(where.IsDeleted))
            //  expr = expr.And(p => p.IsDeleted == where.IsDeleted);
            #endregion
            var list = _groupRepository.Query().Where(expr).OrderBy(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _groupRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<GroupEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<GroupEntity> GetList(GroupEntity where)
        {
            var expr = PredicateBuilder.True<GroupEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            if (!string.IsNullOrEmpty(where.GroupCode))
                expr = expr.And(p => p.GroupCode == where.GroupCode);
            // if (!string.IsNullOrEmpty(where.GroupName))
            //  expr = expr.And(p => p.GroupName == where.GroupName);
            // if (!string.IsNullOrEmpty(where.Sort))
            //  expr = expr.And(p => p.Sort == where.Sort);
            // if (!string.IsNullOrEmpty(where.Remark))
            //  expr = expr.And(p => p.Remark == where.Remark);
            // if (!string.IsNullOrEmpty(where.CreatorUserCode))
            //  expr = expr.And(p => p.CreatorUserCode == where.CreatorUserCode);
            // if (!string.IsNullOrEmpty(where.CreatorUserName))
            //  expr = expr.And(p => p.CreatorUserName == where.CreatorUserName);
            // if (!string.IsNullOrEmpty(where.CreateTime))
            //  expr = expr.And(p => p.CreateTime == where.CreateTime);
            // if (!string.IsNullOrEmpty(where.LastModificationTime))
            //  expr = expr.And(p => p.LastModificationTime == where.LastModificationTime);
            // if (!string.IsNullOrEmpty(where.IsDeleted))
            //  expr = expr.And(p => p.IsDeleted == where.IsDeleted);
            #endregion
            var list = _groupRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法

        #endregion
    }
}





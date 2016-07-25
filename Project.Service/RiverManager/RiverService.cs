
/***************************************************************************
*       功能：     RMRiver业务处理层
*       作者：     李伟伟
*       日期：     2016/7/23
*       描述：     河道信息
* *************************************************************************/

using System;
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.RiverManager;
using Project.Repository.RiverManager;

namespace Project.Service.RiverManager
{
    public class RiverService
    {

        #region 构造函数
        private readonly RiverRepository _riverRepository;
        private static readonly RiverService Instance = new RiverService();

        public RiverService()
        {
            this._riverRepository = new RiverRepository();
        }

        public static RiverService GetInstance()
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
        public System.Int32 Add(RiverEntity entity)
        {
            return _riverRepository.Save(entity);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
            try
            {
                var entity = _riverRepository.GetById(pkId);
                _riverRepository.Delete(entity);
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
        public bool Delete(RiverEntity entity)
        {
            try
            {
                _riverRepository.Delete(entity);
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
        public bool Update(RiverEntity entity)
        {
            try
            {
                _riverRepository.Update(entity);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }


        /// <summary>
        /// 通过主键获取实体
        /// </summary>
        /// <param name="pkId">主键</param>
        /// <returns></returns>
        public RiverEntity GetModelByPk(System.Int32 pkId)
        {
            return _riverRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【河道信息】和总【河道信息】数</returns>
        public System.Tuple<IList<RiverEntity>, int> Search(RiverEntity where, int skipResults, int maxResults)
        {
            var expr = PredicateBuilder.True<RiverEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            // if (!string.IsNullOrEmpty(where.RiverName))
            //  expr = expr.And(p => p.RiverName == where.RiverName);
            // if (!string.IsNullOrEmpty(where.RiverRank))
            //  expr = expr.And(p => p.RiverRank == where.RiverRank);
            // if (!string.IsNullOrEmpty(where.RiverArea))
            //  expr = expr.And(p => p.RiverArea == where.RiverArea);
            // if (!string.IsNullOrEmpty(where.RiverLength))
            //  expr = expr.And(p => p.RiverLength == where.RiverLength);
            // if (!string.IsNullOrEmpty(where.RiverCrossArea))
            //  expr = expr.And(p => p.RiverCrossArea == where.RiverCrossArea);
            // if (!string.IsNullOrEmpty(where.Coords))
            //  expr = expr.And(p => p.Coords == where.Coords);
            // if (!string.IsNullOrEmpty(where.IsActive))
            //  expr = expr.And(p => p.IsActive == where.IsActive);
            // if (!string.IsNullOrEmpty(where.CreatorUserCode))
            //  expr = expr.And(p => p.CreatorUserCode == where.CreatorUserCode);
            // if (!string.IsNullOrEmpty(where.CreationTime))
            //  expr = expr.And(p => p.CreationTime == where.CreationTime);
            // if (!string.IsNullOrEmpty(where.LastModifierUserCode))
            //  expr = expr.And(p => p.LastModifierUserCode == where.LastModifierUserCode);
            // if (!string.IsNullOrEmpty(where.LastModificationTime))
            //  expr = expr.And(p => p.LastModificationTime == where.LastModificationTime);
            // if (!string.IsNullOrEmpty(where.Remark))
            //  expr = expr.And(p => p.Remark == where.Remark);
            #endregion
            var list = _riverRepository.Query().Where(expr).OrderBy(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _riverRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<RiverEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<RiverEntity> GetList(RiverEntity where)
        {
            var expr = PredicateBuilder.True<RiverEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            if (!string.IsNullOrEmpty(where.RiverName))
                expr = expr.And(p => p.RiverName == where.RiverName);
            if (!string.IsNullOrEmpty(where.RiverRank))
                expr = expr.And(p => p.RiverRank == where.RiverRank);
            // if (!string.IsNullOrEmpty(where.RiverArea))
            //  expr = expr.And(p => p.RiverArea == where.RiverArea);
            // if (!string.IsNullOrEmpty(where.RiverLength))
            //  expr = expr.And(p => p.RiverLength == where.RiverLength);
            // if (!string.IsNullOrEmpty(where.RiverCrossArea))
            //  expr = expr.And(p => p.RiverCrossArea == where.RiverCrossArea);
            // if (!string.IsNullOrEmpty(where.Coords))
            //  expr = expr.And(p => p.Coords == where.Coords);
            // if (!string.IsNullOrEmpty(where.IsActive))
            //  expr = expr.And(p => p.IsActive == where.IsActive);
            // if (!string.IsNullOrEmpty(where.CreatorUserCode))
            //  expr = expr.And(p => p.CreatorUserCode == where.CreatorUserCode);
            // if (!string.IsNullOrEmpty(where.CreationTime))
            //  expr = expr.And(p => p.CreationTime == where.CreationTime);
            // if (!string.IsNullOrEmpty(where.LastModifierUserCode))
            //  expr = expr.And(p => p.LastModifierUserCode == where.LastModifierUserCode);
            // if (!string.IsNullOrEmpty(where.LastModificationTime))
            //  expr = expr.And(p => p.LastModificationTime == where.LastModificationTime);
            // if (!string.IsNullOrEmpty(where.Remark))
            //  expr = expr.And(p => p.Remark == where.Remark);
            #endregion
            var list = _riverRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法

        #endregion
    }
}





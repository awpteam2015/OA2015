
 /***************************************************************************
 *       功能：     HRSanction业务处理层
 *       作者：     ROY
 *       日期：     2016-01-09
 *       描述：     职工奖罚及部门等奖罚
 * *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.HRManager;
using Project.Repository.HRManager;

namespace Project.Service.HRManager
{
    public class SanctionService
    {
       
       #region 构造函数
        private readonly SanctionRepository  _sanctionRepository;
            private static readonly SanctionService Instance = new SanctionService();

        public SanctionService()
        {
           this._sanctionRepository =new SanctionRepository();
        }
        
         public static  SanctionService GetInstance()
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
        public System.Int32 Add(SanctionEntity entity)
        {
            return _sanctionRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _sanctionRepository.GetById(pkId);
            _sanctionRepository.Delete(entity);
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
        public bool Delete(SanctionEntity entity)
        {
         try
            {
            _sanctionRepository.Delete(entity);
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
        public bool Update(SanctionEntity entity)
        {
          try
            {
            _sanctionRepository.Update(entity);
         return true;
        }
        catch
        {
         return false;
        }
        }


        /// <summary>
        /// 通过主键获取实体
        /// </summary>
        /// <param name="pkId">主键</param>
        /// <returns></returns>
        public SanctionEntity GetModelByPk(System.Int32 pkId)
        {
            return _sanctionRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【职工奖罚及部门等奖罚】和总【职工奖罚及部门等奖罚】数</returns>
        public System.Tuple<IList<SanctionEntity>, int> Search(SanctionEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<SanctionEntity>();
                  #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.SanctionType))
              //  expr = expr.And(p => p.SanctionType == where.SanctionType);
              // if (!string.IsNullOrEmpty(where.SanctionObj))
              //  expr = expr.And(p => p.SanctionObj == where.SanctionObj);
              // if (!string.IsNullOrEmpty(where.SanctionTitle))
              //  expr = expr.And(p => p.SanctionTitle == where.SanctionTitle);
              // if (!string.IsNullOrEmpty(where.SanctionMoney))
              //  expr = expr.And(p => p.SanctionMoney == where.SanctionMoney);
              // if (!string.IsNullOrEmpty(where.SanctionDate))
              //  expr = expr.And(p => p.SanctionDate == where.SanctionDate);
              // if (!string.IsNullOrEmpty(where.Remark))
              //  expr = expr.And(p => p.Remark == where.Remark);
              // if (!string.IsNullOrEmpty(where.CreatorUserCode))
              //  expr = expr.And(p => p.CreatorUserCode == where.CreatorUserCode);
              // if (!string.IsNullOrEmpty(where.CreatorUserName))
              //  expr = expr.And(p => p.CreatorUserName == where.CreatorUserName);
              // if (!string.IsNullOrEmpty(where.CreateTime))
              //  expr = expr.And(p => p.CreateTime == where.CreateTime);
 #endregion
            var list = _sanctionRepository.Query().Where(expr).OrderBy(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _sanctionRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<SanctionEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<SanctionEntity> GetList(SanctionEntity where)
        {
               var expr = PredicateBuilder.True<SanctionEntity>();
             #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.SanctionType))
              //  expr = expr.And(p => p.SanctionType == where.SanctionType);
              // if (!string.IsNullOrEmpty(where.SanctionObj))
              //  expr = expr.And(p => p.SanctionObj == where.SanctionObj);
              // if (!string.IsNullOrEmpty(where.SanctionTitle))
              //  expr = expr.And(p => p.SanctionTitle == where.SanctionTitle);
              // if (!string.IsNullOrEmpty(where.SanctionMoney))
              //  expr = expr.And(p => p.SanctionMoney == where.SanctionMoney);
              // if (!string.IsNullOrEmpty(where.SanctionDate))
              //  expr = expr.And(p => p.SanctionDate == where.SanctionDate);
              // if (!string.IsNullOrEmpty(where.Remark))
              //  expr = expr.And(p => p.Remark == where.Remark);
              // if (!string.IsNullOrEmpty(where.CreatorUserCode))
              //  expr = expr.And(p => p.CreatorUserCode == where.CreatorUserCode);
              // if (!string.IsNullOrEmpty(where.CreatorUserName))
              //  expr = expr.And(p => p.CreatorUserName == where.CreatorUserName);
              // if (!string.IsNullOrEmpty(where.CreateTime))
              //  expr = expr.And(p => p.CreateTime == where.CreateTime);
 #endregion
            var list = _sanctionRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 


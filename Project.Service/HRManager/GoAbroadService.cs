
 /***************************************************************************
 *       功能：     HRGoAbroad业务处理层
 *       作者：     ROY
 *       日期：     2016-01-09
 *       描述：     记录人员出国情况
 * *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.HRManager;
using Project.Repository.HRManager;

namespace Project.Service.HRManager
{
    public class GoAbroadService
    {
       
       #region 构造函数
        private readonly GoAbroadRepository  _goAbroadRepository;
            private static readonly GoAbroadService Instance = new GoAbroadService();

        public GoAbroadService()
        {
           this._goAbroadRepository =new GoAbroadRepository();
        }
        
         public static  GoAbroadService GetInstance()
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
        public System.Int32 Add(GoAbroadEntity entity)
        {
            return _goAbroadRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _goAbroadRepository.GetById(pkId);
            _goAbroadRepository.Delete(entity);
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
        public bool Delete(GoAbroadEntity entity)
        {
         try
            {
            _goAbroadRepository.Delete(entity);
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
        public bool Update(GoAbroadEntity entity)
        {
          try
            {
            _goAbroadRepository.Update(entity);
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
        public GoAbroadEntity GetModelByPk(System.Int32 pkId)
        {
            return _goAbroadRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【记录人员出国情况】和总【记录人员出国情况】数</returns>
        public System.Tuple<IList<GoAbroadEntity>, int> Search(GoAbroadEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<GoAbroadEntity>();
                  #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.EmployeeCode))
              //  expr = expr.And(p => p.EmployeeCode == where.EmployeeCode);
              // if (!string.IsNullOrEmpty(where.DepartmentCode))
              //  expr = expr.And(p => p.DepartmentCode == where.DepartmentCode);
              // if (!string.IsNullOrEmpty(where.Country))
              //  expr = expr.And(p => p.Country == where.Country);
              // if (!string.IsNullOrEmpty(where.BeginDate))
              //  expr = expr.And(p => p.BeginDate == where.BeginDate);
              // if (!string.IsNullOrEmpty(where.EndDate))
              //  expr = expr.And(p => p.EndDate == where.EndDate);
              // if (!string.IsNullOrEmpty(where.DaySum))
              //  expr = expr.And(p => p.DaySum == where.DaySum);
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
 #endregion
            var list = _goAbroadRepository.Query().Where(expr).OrderBy(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _goAbroadRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<GoAbroadEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<GoAbroadEntity> GetList(GoAbroadEntity where)
        {
               var expr = PredicateBuilder.True<GoAbroadEntity>();
             #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.EmployeeCode))
              //  expr = expr.And(p => p.EmployeeCode == where.EmployeeCode);
              // if (!string.IsNullOrEmpty(where.DepartmentCode))
              //  expr = expr.And(p => p.DepartmentCode == where.DepartmentCode);
              // if (!string.IsNullOrEmpty(where.Country))
              //  expr = expr.And(p => p.Country == where.Country);
              // if (!string.IsNullOrEmpty(where.BeginDate))
              //  expr = expr.And(p => p.BeginDate == where.BeginDate);
              // if (!string.IsNullOrEmpty(where.EndDate))
              //  expr = expr.And(p => p.EndDate == where.EndDate);
              // if (!string.IsNullOrEmpty(where.DaySum))
              //  expr = expr.And(p => p.DaySum == where.DaySum);
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
 #endregion
            var list = _goAbroadRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 


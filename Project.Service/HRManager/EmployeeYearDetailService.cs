
 /***************************************************************************
 *       功能：     HREmployeeYearDetail业务处理层
 *       作者：     Roy
 *       日期：     2016-01-22
 *       描述：     年休登记
 * *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.HRManager;
using Project.Repository.HRManager;

namespace Project.Service.HRManager
{
    public class EmployeeYearDetailService
    {
       
       #region 构造函数
        private readonly EmployeeYearDetailRepository  _employeeYearDetailRepository;
            private static readonly EmployeeYearDetailService Instance = new EmployeeYearDetailService();

        public EmployeeYearDetailService()
        {
           this._employeeYearDetailRepository =new EmployeeYearDetailRepository();
        }
        
         public static  EmployeeYearDetailService GetInstance()
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
        public System.Int32 Add(EmployeeYearDetailEntity entity)
        {
            return _employeeYearDetailRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _employeeYearDetailRepository.GetById(pkId);
            _employeeYearDetailRepository.Delete(entity);
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
        public bool Delete(EmployeeYearDetailEntity entity)
        {
         try
            {
            _employeeYearDetailRepository.Delete(entity);
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
        public bool Update(EmployeeYearDetailEntity entity)
        {
          try
            {
            _employeeYearDetailRepository.Update(entity);
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
        public EmployeeYearDetailEntity GetModelByPk(System.Int32 pkId)
        {
            return _employeeYearDetailRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【年休登记】和总【年休登记】数</returns>
        public System.Tuple<IList<EmployeeYearDetailEntity>, int> Search(EmployeeYearDetailEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<EmployeeYearDetailEntity>();
                  #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.DepartmentCode))
              //  expr = expr.And(p => p.DepartmentCode == where.DepartmentCode);
              // if (!string.IsNullOrEmpty(where.EmployeeCode))
              //  expr = expr.And(p => p.EmployeeCode == where.EmployeeCode);
              // if (!string.IsNullOrEmpty(where.BeginDate))
              //  expr = expr.And(p => p.BeginDate == where.BeginDate);
              // if (!string.IsNullOrEmpty(where.EndDate))
              //  expr = expr.And(p => p.EndDate == where.EndDate);
              // if (!string.IsNullOrEmpty(where.UseCount))
              //  expr = expr.And(p => p.UseCount == where.UseCount);
              // if (!string.IsNullOrEmpty(where.BeforeUseCount))
              //  expr = expr.And(p => p.BeforeUseCount == where.BeforeUseCount);
              // if (!string.IsNullOrEmpty(where.LeftCount))
              //  expr = expr.And(p => p.LeftCount == where.LeftCount);
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
            var list = _employeeYearDetailRepository.Query().Where(expr).OrderBy(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _employeeYearDetailRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<EmployeeYearDetailEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<EmployeeYearDetailEntity> GetList(EmployeeYearDetailEntity where)
        {
               var expr = PredicateBuilder.True<EmployeeYearDetailEntity>();
             #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.DepartmentCode))
              //  expr = expr.And(p => p.DepartmentCode == where.DepartmentCode);
              // if (!string.IsNullOrEmpty(where.EmployeeCode))
              //  expr = expr.And(p => p.EmployeeCode == where.EmployeeCode);
              // if (!string.IsNullOrEmpty(where.BeginDate))
              //  expr = expr.And(p => p.BeginDate == where.BeginDate);
              // if (!string.IsNullOrEmpty(where.EndDate))
              //  expr = expr.And(p => p.EndDate == where.EndDate);
              // if (!string.IsNullOrEmpty(where.UseCount))
              //  expr = expr.And(p => p.UseCount == where.UseCount);
              // if (!string.IsNullOrEmpty(where.BeforeUseCount))
              //  expr = expr.And(p => p.BeforeUseCount == where.BeforeUseCount);
              // if (!string.IsNullOrEmpty(where.LeftCount))
              //  expr = expr.And(p => p.LeftCount == where.LeftCount);
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
            var list = _employeeYearDetailRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 


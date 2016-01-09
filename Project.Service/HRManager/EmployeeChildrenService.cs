
 /***************************************************************************
 *       功能：     HREmployeeChildren业务处理层
 *       作者：     ROY
 *       日期：     2016-01-09
 *       描述：     职工子女登记
 * *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.HRManager;
using Project.Repository.HRManager;

namespace Project.Service.HRManager
{
    public class EmployeeChildrenService
    {
       
       #region 构造函数
        private readonly EmployeeChildrenRepository  _employeeChildrenRepository;
            private static readonly EmployeeChildrenService Instance = new EmployeeChildrenService();

        public EmployeeChildrenService()
        {
           this._employeeChildrenRepository =new EmployeeChildrenRepository();
        }
        
         public static  EmployeeChildrenService GetInstance()
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
        public System.Int32 Add(EmployeeChildrenEntity entity)
        {
            return _employeeChildrenRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _employeeChildrenRepository.GetById(pkId);
            _employeeChildrenRepository.Delete(entity);
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
        public bool Delete(EmployeeChildrenEntity entity)
        {
         try
            {
            _employeeChildrenRepository.Delete(entity);
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
        public bool Update(EmployeeChildrenEntity entity)
        {
          try
            {
            _employeeChildrenRepository.Update(entity);
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
        public EmployeeChildrenEntity GetModelByPk(System.Int32 pkId)
        {
            return _employeeChildrenRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【职工子女登记】和总【职工子女登记】数</returns>
        public System.Tuple<IList<EmployeeChildrenEntity>, int> Search(EmployeeChildrenEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<EmployeeChildrenEntity>();
                  #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.EmployeeCode))
              //  expr = expr.And(p => p.EmployeeCode == where.EmployeeCode);
              // if (!string.IsNullOrEmpty(where.ChildrenName))
              //  expr = expr.And(p => p.ChildrenName == where.ChildrenName);
              // if (!string.IsNullOrEmpty(where.Sex))
              //  expr = expr.And(p => p.Sex == where.Sex);
              // if (!string.IsNullOrEmpty(where.Relation))
              //  expr = expr.And(p => p.Relation == where.Relation);
              // if (!string.IsNullOrEmpty(where.Certificate))
              //  expr = expr.And(p => p.Certificate == where.Certificate);
              // if (!string.IsNullOrEmpty(where.JoinDate))
              //  expr = expr.And(p => p.JoinDate == where.JoinDate);
              // if (!string.IsNullOrEmpty(where.Hospital))
              //  expr = expr.And(p => p.Hospital == where.Hospital);
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
            var list = _employeeChildrenRepository.Query().Where(expr).OrderBy(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _employeeChildrenRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<EmployeeChildrenEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<EmployeeChildrenEntity> GetList(EmployeeChildrenEntity where)
        {
               var expr = PredicateBuilder.True<EmployeeChildrenEntity>();
             #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.EmployeeCode))
              //  expr = expr.And(p => p.EmployeeCode == where.EmployeeCode);
              // if (!string.IsNullOrEmpty(where.ChildrenName))
              //  expr = expr.And(p => p.ChildrenName == where.ChildrenName);
              // if (!string.IsNullOrEmpty(where.Sex))
              //  expr = expr.And(p => p.Sex == where.Sex);
              // if (!string.IsNullOrEmpty(where.Relation))
              //  expr = expr.And(p => p.Relation == where.Relation);
              // if (!string.IsNullOrEmpty(where.Certificate))
              //  expr = expr.And(p => p.Certificate == where.Certificate);
              // if (!string.IsNullOrEmpty(where.JoinDate))
              //  expr = expr.And(p => p.JoinDate == where.JoinDate);
              // if (!string.IsNullOrEmpty(where.Hospital))
              //  expr = expr.And(p => p.Hospital == where.Hospital);
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
            var list = _employeeChildrenRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 


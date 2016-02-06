
 /***************************************************************************
 *       功能：     HRProfession业务处理层
 *       作者：     Roy
 *       日期：     2016-02-02
 *       描述：     职业资格
 * *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.HRManager;
using Project.Repository.HRManager;

namespace Project.Service.HRManager
{
    public class ProfessionService
    {
       
       #region 构造函数
        private readonly ProfessionRepository  _professionRepository;
            private static readonly ProfessionService Instance = new ProfessionService();

        public ProfessionService()
        {
           this._professionRepository =new ProfessionRepository();
        }
        
         public static  ProfessionService GetInstance()
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
        public System.Int32 Add(ProfessionEntity entity)
        {
            return _professionRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _professionRepository.GetById(pkId);
            _professionRepository.Delete(entity);
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
        public bool Delete(ProfessionEntity entity)
        {
         try
            {
            _professionRepository.Delete(entity);
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
        public bool Update(ProfessionEntity entity)
        {
          try
            {
            _professionRepository.Update(entity);
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
        public ProfessionEntity GetModelByPk(System.Int32 pkId)
        {
            return _professionRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【职业资格】和总【职业资格】数</returns>
        public System.Tuple<IList<ProfessionEntity>, int> Search(ProfessionEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<ProfessionEntity>();
                  #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.EmployeeID))
              //  expr = expr.And(p => p.EmployeeID == where.EmployeeID);
              // if (!string.IsNullOrEmpty(where.EmployeeCode))
              //  expr = expr.And(p => p.EmployeeCode == where.EmployeeCode);
              // if (!string.IsNullOrEmpty(where.DepartmentCode))
              //  expr = expr.And(p => p.DepartmentCode == where.DepartmentCode);
              // if (!string.IsNullOrEmpty(where.Title))
              //  expr = expr.And(p => p.Title == where.Title);
              // if (!string.IsNullOrEmpty(where.TypeName))
              //  expr = expr.And(p => p.TypeName == where.TypeName);
              // if (!string.IsNullOrEmpty(where.RangeName))
              //  expr = expr.And(p => p.RangeName == where.RangeName);
              // if (!string.IsNullOrEmpty(where.GetDate))
              //  expr = expr.And(p => p.GetDate == where.GetDate);
              // if (!string.IsNullOrEmpty(where.CerNo))
              //  expr = expr.And(p => p.CerNo == where.CerNo);
              // if (!string.IsNullOrEmpty(where.CreatorUserCode))
              //  expr = expr.And(p => p.CreatorUserCode == where.CreatorUserCode);
              // if (!string.IsNullOrEmpty(where.CreatorUserName))
              //  expr = expr.And(p => p.CreatorUserName == where.CreatorUserName);
              // if (!string.IsNullOrEmpty(where.CreationTime))
              //  expr = expr.And(p => p.CreationTime == where.CreationTime);
              // if (!string.IsNullOrEmpty(where.LastModificationTime))
              //  expr = expr.And(p => p.LastModificationTime == where.LastModificationTime);
              // if (!string.IsNullOrEmpty(where.LastModifierUserCode))
              //  expr = expr.And(p => p.LastModifierUserCode == where.LastModifierUserCode);
 #endregion
            var list = _professionRepository.Query().Where(expr).OrderBy(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _professionRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<ProfessionEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<ProfessionEntity> GetList(ProfessionEntity where)
        {
               var expr = PredicateBuilder.True<ProfessionEntity>();
             #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.EmployeeID))
              //  expr = expr.And(p => p.EmployeeID == where.EmployeeID);
              // if (!string.IsNullOrEmpty(where.EmployeeCode))
              //  expr = expr.And(p => p.EmployeeCode == where.EmployeeCode);
              // if (!string.IsNullOrEmpty(where.DepartmentCode))
              //  expr = expr.And(p => p.DepartmentCode == where.DepartmentCode);
              // if (!string.IsNullOrEmpty(where.Title))
              //  expr = expr.And(p => p.Title == where.Title);
              // if (!string.IsNullOrEmpty(where.TypeName))
              //  expr = expr.And(p => p.TypeName == where.TypeName);
              // if (!string.IsNullOrEmpty(where.RangeName))
              //  expr = expr.And(p => p.RangeName == where.RangeName);
              // if (!string.IsNullOrEmpty(where.GetDate))
              //  expr = expr.And(p => p.GetDate == where.GetDate);
              // if (!string.IsNullOrEmpty(where.CerNo))
              //  expr = expr.And(p => p.CerNo == where.CerNo);
              // if (!string.IsNullOrEmpty(where.CreatorUserCode))
              //  expr = expr.And(p => p.CreatorUserCode == where.CreatorUserCode);
              // if (!string.IsNullOrEmpty(where.CreatorUserName))
              //  expr = expr.And(p => p.CreatorUserName == where.CreatorUserName);
              // if (!string.IsNullOrEmpty(where.CreationTime))
              //  expr = expr.And(p => p.CreationTime == where.CreationTime);
              // if (!string.IsNullOrEmpty(where.LastModificationTime))
              //  expr = expr.And(p => p.LastModificationTime == where.LastModificationTime);
              // if (!string.IsNullOrEmpty(where.LastModifierUserCode))
              //  expr = expr.And(p => p.LastModifierUserCode == where.LastModifierUserCode);
 #endregion
            var list = _professionRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 



 /***************************************************************************
 *       功能：     PMRoleDepartment业务处理层
 *       作者：     Roy
 *       日期：     2016/3/27
 *       描述：     
 * *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.PermissionManager;
using Project.Repository.PermissionManager;

namespace Project.Service.PermissionManager
{
    public class RoleDepartmentService
    {
       
       #region 构造函数
        private readonly RoleDepartmentRepository  _roleDepartmentRepository;
            private static readonly RoleDepartmentService Instance = new RoleDepartmentService();

        public RoleDepartmentService()
        {
           this._roleDepartmentRepository =new RoleDepartmentRepository();
        }
        
         public static  RoleDepartmentService GetInstance()
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
        public System.Int32 Add(RoleDepartmentEntity entity)
        {
            return _roleDepartmentRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _roleDepartmentRepository.GetById(pkId);
            _roleDepartmentRepository.Delete(entity);
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
        public bool Delete(RoleDepartmentEntity entity)
        {
         try
            {
            _roleDepartmentRepository.Delete(entity);
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
        public bool Update(RoleDepartmentEntity entity)
        {
          try
            {
            _roleDepartmentRepository.Update(entity);
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
        public RoleDepartmentEntity GetModelByPk(System.Int32 pkId)
        {
            return _roleDepartmentRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【】和总【】数</returns>
        public System.Tuple<IList<RoleDepartmentEntity>, int> Search(RoleDepartmentEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<RoleDepartmentEntity>();
                  #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.RoleId))
              //  expr = expr.And(p => p.RoleId == where.RoleId);
              // if (!string.IsNullOrEmpty(where.DepartId))
              //  expr = expr.And(p => p.DepartId == where.DepartId);
 #endregion
            var list = _roleDepartmentRepository.Query().Where(expr).OrderBy(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _roleDepartmentRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<RoleDepartmentEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<RoleDepartmentEntity> GetList(RoleDepartmentEntity where)
        {
               var expr = PredicateBuilder.True<RoleDepartmentEntity>();
             #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.RoleId))
              //  expr = expr.And(p => p.RoleId == where.RoleId);
              // if (!string.IsNullOrEmpty(where.DepartId))
              //  expr = expr.And(p => p.DepartId == where.DepartId);
 #endregion
            var list = _roleDepartmentRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 


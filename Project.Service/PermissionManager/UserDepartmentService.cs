
 /***************************************************************************
 *       功能：     PMUserDepartment业务处理层
 *       作者：     李伟伟
 *       日期：     2015/12/23
 *       描述：     用户所属部门
 * *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.PermissionManager;
using Project.Repository.PermissionManager;

namespace Project.Service.PermissionManager
{
    public class UserDepartmentService
    {
       
       #region 构造函数
        private readonly UserDepartmentRepository  _userDepartmentRepository;
            private static readonly UserDepartmentService Instance = new UserDepartmentService();

        public UserDepartmentService()
        {
           this._userDepartmentRepository =new UserDepartmentRepository();
        }
        
         public static  UserDepartmentService GetInstance()
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
        public System.Int32 Add(UserDepartmentEntity entity)
        {
            return _userDepartmentRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _userDepartmentRepository.GetById(pkId);
            _userDepartmentRepository.Delete(entity);
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
        public bool Delete(UserDepartmentEntity entity)
        {
         try
            {
            _userDepartmentRepository.Delete(entity);
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
        public bool Update(UserDepartmentEntity entity)
        {
          try
            {
            _userDepartmentRepository.Update(entity);
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
        public UserDepartmentEntity GetModelByPk(System.Int32 pkId)
        {
            return _userDepartmentRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【用户所属部门】和总【用户所属部门】数</returns>
        public System.Tuple<IList<UserDepartmentEntity>, int> Search(UserDepartmentEntity entity, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<UserDepartmentEntity>();
                  #region
              // if (!string.IsNullOrEmpty(entity.PkId))
              //  expr = expr.And(p => p.PkId == entity.PkId);
              // if (!string.IsNullOrEmpty(entity.UserCode))
              //  expr = expr.And(p => p.UserCode == entity.UserCode);
              // if (!string.IsNullOrEmpty(entity.DepartmentCode))
              //  expr = expr.And(p => p.DepartmentCode == entity.DepartmentCode);
 #endregion
            var list = _userDepartmentRepository.Query().Where(expr).OrderBy(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _userDepartmentRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<UserDepartmentEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<UserDepartmentEntity> GetList(UserDepartmentEntity entity)
        {
               var expr = PredicateBuilder.True<UserDepartmentEntity>();
             #region
              // if (!string.IsNullOrEmpty(entity.PkId))
              //  expr = expr.And(p => p.PkId == entity.PkId);
              // if (!string.IsNullOrEmpty(entity.UserCode))
              //  expr = expr.And(p => p.UserCode == entity.UserCode);
              // if (!string.IsNullOrEmpty(entity.DepartmentCode))
              //  expr = expr.And(p => p.DepartmentCode == entity.DepartmentCode);
 #endregion
            var list = _userDepartmentRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 


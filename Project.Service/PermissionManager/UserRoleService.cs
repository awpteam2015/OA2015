
 /***************************************************************************
 *       功能：     PMUserRole业务处理层
 *       作者：     李伟伟
 *       日期：     2015/12/23
 *       描述：     EC系统用户和角色对应关系表
 * *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.PermissionManager;
using Project.Repository.PermissionManager;

namespace Project.Service.PermissionManager
{
    public class UserRoleService
    {
       
       #region 构造函数
        private readonly UserRoleRepository  _userRoleRepository;
            private static readonly UserRoleService Instance = new UserRoleService();

        public UserRoleService()
        {
           this._userRoleRepository =new UserRoleRepository();
        }
        
         public static  UserRoleService GetInstance()
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
        public System.Int32 Add(UserRoleEntity entity)
        {
            return _userRoleRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _userRoleRepository.GetById(pkId);
            _userRoleRepository.Delete(entity);
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
        public bool Delete(UserRoleEntity entity)
        {
         try
            {
            _userRoleRepository.Delete(entity);
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
        public bool Update(UserRoleEntity entity)
        {
          try
            {
            _userRoleRepository.Update(entity);
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
        public UserRoleEntity GetModelByPk(System.Int32 pkId)
        {
            return _userRoleRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【EC系统用户和角色对应关系表】和总【EC系统用户和角色对应关系表】数</returns>
        public System.Tuple<IList<UserRoleEntity>, int> Search(UserRoleEntity entity, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<UserRoleEntity>();
                  #region
              // if (!string.IsNullOrEmpty(entity.PkId))
              //  expr = expr.And(p => p.PkId == entity.PkId);
              // if (!string.IsNullOrEmpty(entity.UserCode))
              //  expr = expr.And(p => p.UserCode == entity.UserCode);
              // if (!string.IsNullOrEmpty(entity.RoleId))
              //  expr = expr.And(p => p.RoleId == entity.RoleId);
 #endregion
            var list = _userRoleRepository.Query().Where(expr).OrderBy(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _userRoleRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<UserRoleEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<UserRoleEntity> GetList(UserRoleEntity entity)
        {
               var expr = PredicateBuilder.True<UserRoleEntity>();
             #region
              // if (!string.IsNullOrEmpty(entity.PkId))
              //  expr = expr.And(p => p.PkId == entity.PkId);
              // if (!string.IsNullOrEmpty(entity.UserCode))
              //  expr = expr.And(p => p.UserCode == entity.UserCode);
              // if (!string.IsNullOrEmpty(entity.RoleId))
              //  expr = expr.And(p => p.RoleId == entity.RoleId);
 #endregion
            var list = _userRoleRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 


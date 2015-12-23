
 /***************************************************************************
 *       功能：     PMUserFunctionDetail业务处理层
 *       作者：     李伟伟
 *       日期：     2015/12/23
 *       描述：     用户对应的权限
 * *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.PermissionManager;
using Project.Repository.PermissionManager;

namespace Project.Service.PermissionManager
{
    public class UserFunctionDetailService
    {
       
       #region 构造函数
        private readonly UserFunctionDetailRepository  _userFunctionDetailRepository;
            private static readonly UserFunctionDetailService Instance = new UserFunctionDetailService();

        public UserFunctionDetailService()
        {
           this._userFunctionDetailRepository =new UserFunctionDetailRepository();
        }
        
         public static  UserFunctionDetailService GetInstance()
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
        public System.Int32 Add(UserFunctionDetailEntity entity)
        {
            return _userFunctionDetailRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _userFunctionDetailRepository.GetById(pkId);
            _userFunctionDetailRepository.Delete(entity);
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
        public bool Delete(UserFunctionDetailEntity entity)
        {
         try
            {
            _userFunctionDetailRepository.Delete(entity);
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
        public bool Update(UserFunctionDetailEntity entity)
        {
          try
            {
            _userFunctionDetailRepository.Update(entity);
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
        public UserFunctionDetailEntity GetModelByPk(System.Int32 pkId)
        {
            return _userFunctionDetailRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【用户对应的权限】和总【用户对应的权限】数</returns>
        public System.Tuple<IList<UserFunctionDetailEntity>, int> Search(UserFunctionDetailEntity entity, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<UserFunctionDetailEntity>();
                  #region
              // if (!string.IsNullOrEmpty(entity.PkId))
              //  expr = expr.And(p => p.PkId == entity.PkId);
              // if (!string.IsNullOrEmpty(entity.UserCode))
              //  expr = expr.And(p => p.UserCode == entity.UserCode);
              // if (!string.IsNullOrEmpty(entity.FunctionId))
              //  expr = expr.And(p => p.FunctionId == entity.FunctionId);
              // if (!string.IsNullOrEmpty(entity.FunctionDetailId))
              //  expr = expr.And(p => p.FunctionDetailId == entity.FunctionDetailId);
 #endregion
            var list = _userFunctionDetailRepository.Query().Where(expr).OrderBy(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _userFunctionDetailRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<UserFunctionDetailEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<UserFunctionDetailEntity> GetList(UserFunctionDetailEntity entity)
        {
               var expr = PredicateBuilder.True<UserFunctionDetailEntity>();
             #region
              // if (!string.IsNullOrEmpty(entity.PkId))
              //  expr = expr.And(p => p.PkId == entity.PkId);
              // if (!string.IsNullOrEmpty(entity.UserCode))
              //  expr = expr.And(p => p.UserCode == entity.UserCode);
              // if (!string.IsNullOrEmpty(entity.FunctionId))
              //  expr = expr.And(p => p.FunctionId == entity.FunctionId);
              // if (!string.IsNullOrEmpty(entity.FunctionDetailId))
              //  expr = expr.And(p => p.FunctionDetailId == entity.FunctionDetailId);
 #endregion
            var list = _userFunctionDetailRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 


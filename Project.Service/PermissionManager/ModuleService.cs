
 /***************************************************************************
 *       功能：     PMModule业务处理层
 *       作者：     李伟伟
 *       日期：     2015/12/23
 *       描述：     权限模块
 * *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.PermissionManager;
using Project.Repository.PermissionManager;

namespace Project.Service.PermissionManager
{
    public class ModuleService
    {
       
       #region 构造函数
        private readonly ModuleRepository  _moduleRepository;
            private static readonly ModuleService Instance = new ModuleService();

        public ModuleService()
        {
           this._moduleRepository =new ModuleRepository();
        }
        
         public static  ModuleService GetInstance()
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
        public System.Int32 Add(ModuleEntity entity)
        {
            return _moduleRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _moduleRepository.GetById(pkId);
            _moduleRepository.Delete(entity);
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
        public bool Delete(ModuleEntity entity)
        {
         try
            {
            _moduleRepository.Delete(entity);
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
        public bool Update(ModuleEntity entity)
        {
          try
            {
            _moduleRepository.Update(entity);
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
        public ModuleEntity GetModelByPk(System.Int32 pkId)
        {
            return _moduleRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【权限模块】和总【权限模块】数</returns>
        public System.Tuple<IList<ModuleEntity>, int> Search(ModuleEntity entity, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<ModuleEntity>();
                  #region
              // if (!string.IsNullOrEmpty(entity.PkId))
              //  expr = expr.And(p => p.PkId == entity.PkId);
                if (!string.IsNullOrEmpty(entity.ModuleName))
                    expr = expr.And(p => p.ModuleName == entity.ModuleName);
              // if (!string.IsNullOrEmpty(entity.ParentId))
              //  expr = expr.And(p => p.ParentId == entity.ParentId);
              // if (!string.IsNullOrEmpty(entity.ModuleLevel))
              //  expr = expr.And(p => p.ModuleLevel == entity.ModuleLevel);
              // if (!string.IsNullOrEmpty(entity.RankId))
              //  expr = expr.And(p => p.RankId == entity.RankId);
              // if (!string.IsNullOrEmpty(entity.Remark))
              //  expr = expr.And(p => p.Remark == entity.Remark);
 #endregion
            var list = _moduleRepository.Query().Where(expr).OrderBy(p => p.RankId).ThenBy(p=>p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _moduleRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<ModuleEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<ModuleEntity> GetList(ModuleEntity entity)
        {
               var expr = PredicateBuilder.True<ModuleEntity>();
             #region
              // if (!string.IsNullOrEmpty(entity.PkId))
              //  expr = expr.And(p => p.PkId == entity.PkId);
              // if (!string.IsNullOrEmpty(entity.ModuleName))
              //  expr = expr.And(p => p.ModuleName == entity.ModuleName);
              // if (!string.IsNullOrEmpty(entity.ParentId))
              //  expr = expr.And(p => p.ParentId == entity.ParentId);
              // if (!string.IsNullOrEmpty(entity.ModuleLevel))
              //  expr = expr.And(p => p.ModuleLevel == entity.ModuleLevel);
              // if (!string.IsNullOrEmpty(entity.RankId))
              //  expr = expr.And(p => p.RankId == entity.RankId);
              // if (!string.IsNullOrEmpty(entity.Remark))
              //  expr = expr.And(p => p.Remark == entity.Remark);
 #endregion
            var list = _moduleRepository.Query().Where(expr).OrderBy(p => p.RankId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 


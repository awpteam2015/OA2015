
 /***************************************************************************
 *       功能：     PMFunctionDetail业务处理层
 *       作者：     李伟伟
 *       日期：     2015/12/23
 *       描述：     模块功能点
 * *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.PermissionManager;
using Project.Repository.PermissionManager;

namespace Project.Service.PermissionManager
{
    public class FunctionDetailService
    {
       
       #region 构造函数
        private readonly FunctionDetailRepository  _functionDetailRepository;
            private static readonly FunctionDetailService Instance = new FunctionDetailService();

        public FunctionDetailService()
        {
           this._functionDetailRepository =new FunctionDetailRepository();
        }
        
         public static  FunctionDetailService GetInstance()
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
        public System.Int32 Add(FunctionDetailEntity entity)
        {
            return _functionDetailRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _functionDetailRepository.GetById(pkId);
            _functionDetailRepository.Delete(entity);
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
        public bool Delete(FunctionDetailEntity entity)
        {
         try
            {
            _functionDetailRepository.Delete(entity);
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
        public bool Update(FunctionDetailEntity entity)
        {
          try
            {
            _functionDetailRepository.Update(entity);
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
        public FunctionDetailEntity GetModelByPk(System.Int32 pkId)
        {
            return _functionDetailRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【模块功能点】和总【模块功能点】数</returns>
        public System.Tuple<IList<FunctionDetailEntity>, int> Search(FunctionDetailEntity entity, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<FunctionDetailEntity>();
                  #region
              // if (!string.IsNullOrEmpty(entity.PkId))
              //  expr = expr.And(p => p.PkId == entity.PkId);
              // if (!string.IsNullOrEmpty(entity.FunctionDetailName))
              //  expr = expr.And(p => p.FunctionDetailName == entity.FunctionDetailName);
              // if (!string.IsNullOrEmpty(entity.FunctionDetailCode))
              //  expr = expr.And(p => p.FunctionDetailCode == entity.FunctionDetailCode);
              // if (!string.IsNullOrEmpty(entity.FunctionId))
              //  expr = expr.And(p => p.FunctionId == entity.FunctionId);
              // if (!string.IsNullOrEmpty(entity.Area))
              //  expr = expr.And(p => p.Area == entity.Area);
              // if (!string.IsNullOrEmpty(entity.Controller))
              //  expr = expr.And(p => p.Controller == entity.Controller);
              // if (!string.IsNullOrEmpty(entity.Action))
              //  expr = expr.And(p => p.Action == entity.Action);
              // if (!string.IsNullOrEmpty(entity.CreatorUserCode))
              //  expr = expr.And(p => p.CreatorUserCode == entity.CreatorUserCode);
              // if (!string.IsNullOrEmpty(entity.CreationTime))
              //  expr = expr.And(p => p.CreationTime == entity.CreationTime);
              // if (!string.IsNullOrEmpty(entity.LastModifierUserCode))
              //  expr = expr.And(p => p.LastModifierUserCode == entity.LastModifierUserCode);
              // if (!string.IsNullOrEmpty(entity.LastModificationTime))
              //  expr = expr.And(p => p.LastModificationTime == entity.LastModificationTime);
 #endregion
            var list = _functionDetailRepository.Query().Where(expr).OrderBy(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _functionDetailRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<FunctionDetailEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<FunctionDetailEntity> GetList(FunctionDetailEntity entity)
        {
               var expr = PredicateBuilder.True<FunctionDetailEntity>();
             #region
              // if (!string.IsNullOrEmpty(entity.PkId))
              //  expr = expr.And(p => p.PkId == entity.PkId);
              // if (!string.IsNullOrEmpty(entity.FunctionDetailName))
              //  expr = expr.And(p => p.FunctionDetailName == entity.FunctionDetailName);
              // if (!string.IsNullOrEmpty(entity.FunctionDetailCode))
              //  expr = expr.And(p => p.FunctionDetailCode == entity.FunctionDetailCode);
               if (entity.FunctionId>0)
                   expr = expr.And(p => p.FunctionId == entity.FunctionId);
              // if (!string.IsNullOrEmpty(entity.Area))
              //  expr = expr.And(p => p.Area == entity.Area);
              // if (!string.IsNullOrEmpty(entity.Controller))
              //  expr = expr.And(p => p.Controller == entity.Controller);
              // if (!string.IsNullOrEmpty(entity.Action))
              //  expr = expr.And(p => p.Action == entity.Action);
              // if (!string.IsNullOrEmpty(entity.CreatorUserCode))
              //  expr = expr.And(p => p.CreatorUserCode == entity.CreatorUserCode);
              // if (!string.IsNullOrEmpty(entity.CreationTime))
              //  expr = expr.And(p => p.CreationTime == entity.CreationTime);
              // if (!string.IsNullOrEmpty(entity.LastModifierUserCode))
              //  expr = expr.And(p => p.LastModifierUserCode == entity.LastModifierUserCode);
              // if (!string.IsNullOrEmpty(entity.LastModificationTime))
              //  expr = expr.And(p => p.LastModificationTime == entity.LastModificationTime);
 #endregion
            var list = _functionDetailRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 


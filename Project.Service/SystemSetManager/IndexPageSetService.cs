
 /***************************************************************************
 *       功能：     SMIndexPageSet业务处理层
 *       作者：     Roy
 *       日期：     2016-02-21
 *       描述：     
 * *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.SystemSetManager;
using Project.Repository.SystemSetManager;

namespace Project.Service.SystemSetManager
{
    public class IndexPageSetService
    {
       
       #region 构造函数
        private readonly IndexPageSetRepository  _indexPageSetRepository;
            private static readonly IndexPageSetService Instance = new IndexPageSetService();

        public IndexPageSetService()
        {
           this._indexPageSetRepository =new IndexPageSetRepository();
        }
        
         public static  IndexPageSetService GetInstance()
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
        public System.Int32 Add(IndexPageSetEntity entity)
        {
            return _indexPageSetRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _indexPageSetRepository.GetById(pkId);
            _indexPageSetRepository.Delete(entity);
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
        public bool Delete(IndexPageSetEntity entity)
        {
         try
            {
            _indexPageSetRepository.Delete(entity);
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
        public bool Update(IndexPageSetEntity entity)
        {
          try
            {
            _indexPageSetRepository.Merge(entity);
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
        public IndexPageSetEntity GetModelByPk(System.Int32 pkId)
        {
            return _indexPageSetRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【】和总【】数</returns>
        public System.Tuple<IList<IndexPageSetEntity>, int> Search(IndexPageSetEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<IndexPageSetEntity>();
                  #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.Des))
              //  expr = expr.And(p => p.Des == where.Des);
 #endregion
            var list = _indexPageSetRepository.Query().Where(expr).OrderBy(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _indexPageSetRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<IndexPageSetEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<IndexPageSetEntity> GetList(IndexPageSetEntity where)
        {
               var expr = PredicateBuilder.True<IndexPageSetEntity>();
             #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.Des))
              //  expr = expr.And(p => p.Des == where.Des);
 #endregion
            var list = _indexPageSetRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 


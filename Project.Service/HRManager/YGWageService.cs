
/***************************************************************************
*       功能：     HRYGWage业务处理层
*       作者：     Roy
*       日期：     2016-05-08
*       描述：     
* *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.HRManager;
using Project.Repository.HRManager;

namespace Project.Service.HRManager
{
    public class YGWageService
    {

        #region 构造函数
        private readonly YGWageRepository _yGWageRepository;
        private static readonly YGWageService Instance = new YGWageService();

        public YGWageService()
        {
            this._yGWageRepository = new YGWageRepository();
        }

        public static YGWageService GetInstance()
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
        public System.Int32 Add(YGWageEntity entity)
        {
            return _yGWageRepository.Save(entity);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
            try
            {
                var entity = _yGWageRepository.GetById(pkId);
                _yGWageRepository.Delete(entity);
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
        public bool Delete(YGWageEntity entity)
        {
            try
            {
                _yGWageRepository.Delete(entity);
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
        public bool Update(YGWageEntity entity)
        {
            try
            {
                _yGWageRepository.Update(entity);
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
        public YGWageEntity GetModelByPk(System.Int32 pkId)
        {
            return _yGWageRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【】和总【】数</returns>
        public System.Tuple<IList<YGWageEntity>, int> Search(YGWageEntity where, int skipResults, int maxResults)
        {
            var expr = PredicateBuilder.True<YGWageEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            // if (!string.IsNullOrEmpty(where.EmployeeID))
            expr = expr.And(p => p.EmployeeID == where.EmployeeID);
            // if (!string.IsNullOrEmpty(where.GWGZ))
            //  expr = expr.And(p => p.GWGZ == where.GWGZ);
            // if (!string.IsNullOrEmpty(where.XZGZ))
            //  expr = expr.And(p => p.XZGZ == where.XZGZ);
            // if (!string.IsNullOrEmpty(where.CreatorUserCode))
            //  expr = expr.And(p => p.CreatorUserCode == where.CreatorUserCode);
            // if (!string.IsNullOrEmpty(where.CreattorUserName))
            //  expr = expr.And(p => p.CreattorUserName == where.CreattorUserName);
            // if (!string.IsNullOrEmpty(where.CreationTime))
            //  expr = expr.And(p => p.CreationTime == where.CreationTime);
            // if (!string.IsNullOrEmpty(where.LastModificationTime))
            //  expr = expr.And(p => p.LastModificationTime == where.LastModificationTime);
            // if (!string.IsNullOrEmpty(where.LastModifierUserCode))
            //  expr = expr.And(p => p.LastModifierUserCode == where.LastModifierUserCode);
            #endregion
            var list = _yGWageRepository.Query().Where(expr).OrderBy(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _yGWageRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<YGWageEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<YGWageEntity> GetList(YGWageEntity where)
        {
            var expr = PredicateBuilder.True<YGWageEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            // if (!string.IsNullOrEmpty(where.EmployeeID))
             expr = expr.And(p => p.EmployeeID == where.EmployeeID);
            // if (!string.IsNullOrEmpty(where.GWGZ))
            //  expr = expr.And(p => p.GWGZ == where.GWGZ);
            // if (!string.IsNullOrEmpty(where.XZGZ))
            //  expr = expr.And(p => p.XZGZ == where.XZGZ);
            // if (!string.IsNullOrEmpty(where.CreatorUserCode))
            //  expr = expr.And(p => p.CreatorUserCode == where.CreatorUserCode);
            // if (!string.IsNullOrEmpty(where.CreattorUserName))
            //  expr = expr.And(p => p.CreattorUserName == where.CreattorUserName);
            // if (!string.IsNullOrEmpty(where.CreationTime))
            //  expr = expr.And(p => p.CreationTime == where.CreationTime);
            // if (!string.IsNullOrEmpty(where.LastModificationTime))
            //  expr = expr.And(p => p.LastModificationTime == where.LastModificationTime);
            // if (!string.IsNullOrEmpty(where.LastModifierUserCode))
            //  expr = expr.And(p => p.LastModifierUserCode == where.LastModifierUserCode);
            #endregion
            var list = _yGWageRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法

        #endregion
    }
}





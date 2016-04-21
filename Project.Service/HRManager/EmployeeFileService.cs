
/***************************************************************************
*       功能：     HREmployeeFile业务处理层
*       作者：     Roy
*       日期：     2016/4/21
*       描述：     
* *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.HRManager;
using Project.Repository.HRManager;

namespace Project.Service.HRManager
{
    public class EmployeeFileService
    {

        #region 构造函数
        private readonly EmployeeFileRepository _employeeFileRepository;
        private static readonly EmployeeFileService Instance = new EmployeeFileService();

        public EmployeeFileService()
        {
            this._employeeFileRepository = new EmployeeFileRepository();
        }

        public static EmployeeFileService GetInstance()
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
        public System.Int32 Add(EmployeeFileEntity entity)
        {
            return _employeeFileRepository.Save(entity);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
            try
            {
                var entity = _employeeFileRepository.GetById(pkId);
                _employeeFileRepository.Delete(entity);
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
        public bool Delete(EmployeeFileEntity entity)
        {
            try
            {
                _employeeFileRepository.Delete(entity);
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
        public bool Update(EmployeeFileEntity entity)
        {
            try
            {
                _employeeFileRepository.Update(entity);
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
        public EmployeeFileEntity GetModelByPk(System.Int32 pkId)
        {
            return _employeeFileRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【】和总【】数</returns>
        public System.Tuple<IList<EmployeeFileEntity>, int> Search(EmployeeFileEntity where, int skipResults, int maxResults)
        {
            var expr = PredicateBuilder.True<EmployeeFileEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            if (where.EmployeeID.HasValue && where.EmployeeID.Value > 0)
                expr = expr.And(p => p.EmployeeID == where.EmployeeID);
            // if (!string.IsNullOrEmpty(where.FName))
            //  expr = expr.And(p => p.FName == where.FName);
            // if (!string.IsNullOrEmpty(where.FileUrl))
            //  expr = expr.And(p => p.FileUrl == where.FileUrl);
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
            var list = _employeeFileRepository.Query().Where(expr).OrderBy(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _employeeFileRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<EmployeeFileEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<EmployeeFileEntity> GetList(EmployeeFileEntity where)
        {
            var expr = PredicateBuilder.True<EmployeeFileEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);

            expr = expr.And(p => p.EmployeeID == where.EmployeeID);
            // if (!string.IsNullOrEmpty(where.FName))
            //  expr = expr.And(p => p.FName == where.FName);
            // if (!string.IsNullOrEmpty(where.FileUrl))
            //  expr = expr.And(p => p.FileUrl == where.FileUrl);
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
            var list = _employeeFileRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法

        #endregion
    }
}





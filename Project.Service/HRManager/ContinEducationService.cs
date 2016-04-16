
/***************************************************************************
*       功能：     HRContinEducation业务处理层
*       作者：     Roy
*       日期：     2016/4/16
*       描述：     
* *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.HRManager;
using Project.Repository.HRManager;

namespace Project.Service.HRManager
{
    public class ContinEducationService
    {

        #region 构造函数
        private readonly ContinEducationRepository _continEducationRepository;
        private static readonly ContinEducationService Instance = new ContinEducationService();

        public ContinEducationService()
        {
            this._continEducationRepository = new ContinEducationRepository();
        }

        public static ContinEducationService GetInstance()
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
        public System.Int32 Add(ContinEducationEntity entity)
        {
            return _continEducationRepository.Save(entity);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
            try
            {
                var entity = _continEducationRepository.GetById(pkId);
                _continEducationRepository.Delete(entity);
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
        public bool Delete(ContinEducationEntity entity)
        {
            try
            {
                _continEducationRepository.Delete(entity);
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
        public bool Update(ContinEducationEntity entity)
        {
            try
            {
                _continEducationRepository.Update(entity);
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
        public ContinEducationEntity GetModelByPk(System.Int32 pkId)
        {
            return _continEducationRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【】和总【】数</returns>
        public System.Tuple<IList<ContinEducationEntity>, int> Search(ContinEducationEntity where, int skipResults, int maxResults)
        {
            var expr = PredicateBuilder.True<ContinEducationEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            // if (!string.IsNullOrEmpty(where.EmployeeID))
            //  expr = expr.And(p => p.EmployeeID == where.EmployeeID);
            // if (!string.IsNullOrEmpty(where.EmployeeCode))
            //  expr = expr.And(p => p.EmployeeCode == where.EmployeeCode);
            // if (!string.IsNullOrEmpty(where.DepartmentCode))
            //  expr = expr.And(p => p.DepartmentCode == where.DepartmentCode);
            // if (!string.IsNullOrEmpty(where.CreditType))
            //  expr = expr.And(p => p.CreditType == where.CreditType);
            // if (!string.IsNullOrEmpty(where.CreditTypeName))
            //  expr = expr.And(p => p.CreditTypeName == where.CreditTypeName);
            // if (!string.IsNullOrEmpty(where.Score))
            //  expr = expr.And(p => p.Score == where.Score);
            // if (!string.IsNullOrEmpty(where.GetTime))
            //  expr = expr.And(p => p.GetTime == where.GetTime);
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
            var list = _continEducationRepository.Query().Where(expr).OrderBy(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _continEducationRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<ContinEducationEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<ContinEducationEntity> GetList(ContinEducationEntity where)
        {
            var expr = PredicateBuilder.True<ContinEducationEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            if (where.EmployeeID.HasValue && where.EmployeeID > 0)
                expr = expr.And(p => p.EmployeeID == where.EmployeeID);
            // if (!string.IsNullOrEmpty(where.EmployeeCode))
            //  expr = expr.And(p => p.EmployeeCode == where.EmployeeCode);
            // if (!string.IsNullOrEmpty(where.DepartmentCode))
            //  expr = expr.And(p => p.DepartmentCode == where.DepartmentCode);
            // if (!string.IsNullOrEmpty(where.CreditType))
            //  expr = expr.And(p => p.CreditType == where.CreditType);
            // if (!string.IsNullOrEmpty(where.CreditTypeName))
            //  expr = expr.And(p => p.CreditTypeName == where.CreditTypeName);
            // if (!string.IsNullOrEmpty(where.Score))
            //  expr = expr.And(p => p.Score == where.Score);
            // if (!string.IsNullOrEmpty(where.GetTime))
            //  expr = expr.And(p => p.GetTime == where.GetTime);
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
            var list = _continEducationRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法

        #endregion
    }
}






/***************************************************************************
*       功能：     HREmployeeYearMain业务处理层
*       作者：     Roy
*       日期：     2016-01-22
*       描述：     员工年休管理
* *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.HRManager;
using Project.Repository.HRManager;
using Project.Infrastructure.FrameworkCore.DataNhibernate;
using System;

namespace Project.Service.HRManager
{
    public class EmployeeYearMainService
    {

        #region 构造函数
        private readonly EmployeeYearMainRepository _employeeYearMainRepository;
        private readonly EmployeeYearDetailRepository _employeeYearDetailRepository;
        private static readonly EmployeeYearMainService Instance = new EmployeeYearMainService();

        public EmployeeYearMainService()
        {
            this._employeeYearMainRepository = new EmployeeYearMainRepository();
            this._employeeYearDetailRepository = new EmployeeYearDetailRepository();
        }

        public static EmployeeYearMainService GetInstance()
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
        public Tuple<bool, string> Add(EmployeeYearMainEntity entity)
        {
            var addCountTmep = entity.LeftCount;
            var where = new EmployeeYearMainEntity();
            where.EmployeeCode = entity.EmployeeCode;
            var detailEntity = new EmployeeYearDetailEntity()
            {
                CreatorUserName = entity.CreatorUserName,
                UseType = 0,
                DepartmentCode = entity.DepartmentCode,
                EmployeeCode = entity.EmployeeCode,
                EmployeeName = entity.EmployeeName,
                UseCount = addCountTmep
            };
            var tempentity = GetList(where).ToList().FirstOrDefault();
            using (var tx = NhTransactionHelper.BeginTransaction())
            {
                try
                {
                    if (tempentity != null)
                        entity = tempentity;

                    detailEntity.BeforeUseCount = tempentity == null ? 0 : entity.LeftCount;
                    detailEntity.LeftCount = detailEntity.BeforeUseCount + detailEntity.UseCount;
                    entity.LeftCount = detailEntity.LeftCount;
                    entity.LastModificationTime = DateTime.Now;
                    var pkId = _employeeYearMainRepository.Save(entity);
                    _employeeYearDetailRepository.Save(detailEntity);

                    tx.Commit();
                    return new Tuple<bool, string>(true, ""); ;
                }
                catch (Exception e)
                {
                    tx.Rollback();
                    throw;
                }
            }
            //return _employeeYearMainRepository.Save(entity);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public new Tuple<bool, string> DeleteByPkId(System.Int32 pkId)
        {
            using (var tx = NhTransactionHelper.BeginTransaction())
            {
                try
                {
                    var entity = _employeeYearMainRepository.GetById(pkId);
                    _employeeYearMainRepository.Delete(entity);
                    EmployeeYearDetailEntity where = new EmployeeYearDetailEntity();
                    where.EmployeeCode = entity.EmployeeCode;
                    var expr = PredicateBuilder.True<EmployeeYearDetailEntity>();
                    _employeeYearDetailRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList().ForEach(item =>
                    {
                        _employeeYearDetailRepository.Delete(item);
                    });

                    tx.Commit();
                    return new Tuple<bool, string>(true, ""); ;
                }
                catch (Exception e)
                {
                    tx.Rollback();
                    throw;
                }
            }

        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        public bool Delete(EmployeeYearMainEntity entity)
        {
            try
            {
                _employeeYearMainRepository.Delete(entity);
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
        public Tuple<bool, string> Update(EmployeeYearMainEntity entity)
        {
            try
            {
                _employeeYearMainRepository.Update(entity);
                return new Tuple<bool, string>(true, "");
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// 通过主键获取实体
        /// </summary>
        /// <param name="pkId">主键</param>
        /// <returns></returns>
        public EmployeeYearMainEntity GetModelByPk(System.Int32 pkId)
        {
            return _employeeYearMainRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【员工年休管理】和总【员工年休管理】数</returns>
        public System.Tuple<IList<EmployeeYearMainEntity>, int> Search(EmployeeYearMainEntity where, int skipResults, int maxResults)
        {
            var expr = PredicateBuilder.True<EmployeeYearMainEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            // if (!string.IsNullOrEmpty(where.DepartmentCode))
            //  expr = expr.And(p => p.DepartmentCode == where.DepartmentCode);
            // if (!string.IsNullOrEmpty(where.EmployeeCode))
            //  expr = expr.And(p => p.EmployeeCode == where.EmployeeCode);
            // if (!string.IsNullOrEmpty(where.LeftCount))
            //  expr = expr.And(p => p.LeftCount == where.LeftCount);
            // if (!string.IsNullOrEmpty(where.Remark))
            //  expr = expr.And(p => p.Remark == where.Remark);
            // if (!string.IsNullOrEmpty(where.CreatorUserCode))
            //  expr = expr.And(p => p.CreatorUserCode == where.CreatorUserCode);
            // if (!string.IsNullOrEmpty(where.CreatorUserName))
            //  expr = expr.And(p => p.CreatorUserName == where.CreatorUserName);
            // if (!string.IsNullOrEmpty(where.CreateTime))
            //  expr = expr.And(p => p.CreateTime == where.CreateTime);
            // if (!string.IsNullOrEmpty(where.LastModificationTime))
            //  expr = expr.And(p => p.LastModificationTime == where.LastModificationTime);
            #endregion
            var list = _employeeYearMainRepository.Query().Where(expr).OrderBy(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _employeeYearMainRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<EmployeeYearMainEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<EmployeeYearMainEntity> GetList(EmployeeYearMainEntity where)
        {
            var expr = PredicateBuilder.True<EmployeeYearMainEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            // if (!string.IsNullOrEmpty(where.DepartmentCode))
            //  expr = expr.And(p => p.DepartmentCode == where.DepartmentCode);
            if (!string.IsNullOrEmpty(where.EmployeeCode))
                expr = expr.And(p => p.EmployeeCode == where.EmployeeCode);
            // if (!string.IsNullOrEmpty(where.LeftCount))
            //  expr = expr.And(p => p.LeftCount == where.LeftCount);
            // if (!string.IsNullOrEmpty(where.Remark))
            //  expr = expr.And(p => p.Remark == where.Remark);
            // if (!string.IsNullOrEmpty(where.CreatorUserCode))
            //  expr = expr.And(p => p.CreatorUserCode == where.CreatorUserCode);
            // if (!string.IsNullOrEmpty(where.CreatorUserName))
            //  expr = expr.And(p => p.CreatorUserName == where.CreatorUserName);
            // if (!string.IsNullOrEmpty(where.CreateTime))
            //  expr = expr.And(p => p.CreateTime == where.CreateTime);
            // if (!string.IsNullOrEmpty(where.LastModificationTime))
            //  expr = expr.And(p => p.LastModificationTime == where.LastModificationTime);
            #endregion
            var list = _employeeYearMainRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法

        #endregion
    }
}





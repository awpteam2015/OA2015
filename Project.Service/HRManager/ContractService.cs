
/***************************************************************************
*       功能：     HRContract业务处理层
*       作者：     李伟伟
*       日期：     2016/1/23
*       描述：     用于记录合同（合同内工资类型等都过滤暂时不考虑）
* *************************************************************************/

using System;
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.HRManager;
using Project.Repository.HRManager;
using Project.Service.HRManager.Validate;

namespace Project.Service.HRManager
{
    public class ContractService
    {

        #region 构造函数
        private readonly ContractRepository _contractRepository;
        private static readonly ContractService Instance = new ContractService();

        public ContractService()
        {
            this._contractRepository = new ContractRepository();
        }

        public static ContractService GetInstance()
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
        public Tuple<bool, string> Add(ContractEntity entity)
        {

            var validateResult = ContractValidate.GetInstance().IsHasSameContractCode(entity.ContractNo);
            if (!validateResult.Item1)
            {
                return validateResult;
            }


            using (var tx = NhTransactionHelper.BeginTransaction())
            {
                try
                {
                    _contractRepository.Save(entity);
                    if (entity.ParentId > 0)
                    {
                        var parentEntity = this.GetModelByPk(entity.ParentId);
                        parentEntity.IsActive = 2;
                        _contractRepository.Update(parentEntity);
                    }
                    tx.Commit();
                    return new Tuple<bool, string>(true, "");
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
        /// <param name="pkId"></param>
        public Tuple<bool, string> DeleteByPkId(System.Int32 pkId)
        {
            try
            {
                var entity = _contractRepository.GetById(pkId);
                _contractRepository.Delete(entity);
                return new Tuple<bool, string>(true,"");
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        public Tuple<bool, string> Delete(ContractEntity entity)
        {
            try
            {
                _contractRepository.Delete(entity);
                return new Tuple<bool, string>(true,"");
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public Tuple<bool, string> Update(ContractEntity entity)
        {
            var validateResult = ContractValidate.GetInstance().IsHasSameContractCode(entity.ContractNo);
            if (!validateResult.Item1)
            {
                return validateResult;
            }
            try
            {
                _contractRepository.Update(entity);
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
        public ContractEntity GetModelByPk(System.Int32 pkId)
        {
            return _contractRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【用于记录合同（合同内工资类型等都过滤暂时不考虑）】和总【用于记录合同（合同内工资类型等都过滤暂时不考虑）】数</returns>
        public System.Tuple<IList<ContractEntity>, int> Search(ContractEntity where, int skipResults, int maxResults)
        {
            var expr = PredicateBuilder.True<ContractEntity>();
            #region
        
            if (!string.IsNullOrEmpty(where.EmployeeCode))
                expr = expr.And(p => p.EmployeeCode == where.EmployeeCode);
            if (!string.IsNullOrEmpty(where.DepartmentCode))
                expr = expr.And(p => p.DepartmentCode == where.DepartmentCode);

            if (where.BeginDate != null && where.BeginDate != DateTime.MinValue)
                expr = expr.And(p => p.BeginDate >= where.BeginDate);
            if (where.EndDate != null && where.EndDate!=DateTime.MinValue)
                expr = expr.And(p => p.EndDate <= where.EndDate);
            if (where.State>0)
                expr = expr.And(p => p.State == where.State);
            if (where.IsActive>0)
                expr = expr.And(p => p.IsActive == where.IsActive);
            if (!string.IsNullOrEmpty(where.ContractNo))
                expr = expr.And(p => p.ContractNo == where.ContractNo);
            if (!string.IsNullOrEmpty(where.FirstParty))
                expr = expr.And(p => p.FirstParty == where.FirstParty);
            if (!string.IsNullOrEmpty(where.SecondParty))
                expr = expr.And(p => p.SecondParty == where.SecondParty);
            if (!string.IsNullOrEmpty(where.ContractContent))
                expr = expr.And(p => p.ContractContent == where.ContractContent);
            if (!string.IsNullOrEmpty(where.IdentityCardNo))
                expr = expr.And(p => p.IdentityCardNo == where.IdentityCardNo);
            #endregion
            var list = _contractRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _contractRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<ContractEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<ContractEntity> GetList(ContractEntity where)
        {
            var expr = PredicateBuilder.True<ContractEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            // if (!string.IsNullOrEmpty(where.EmployeeCode))
            //  expr = expr.And(p => p.EmployeeCode == where.EmployeeCode);
            // if (!string.IsNullOrEmpty(where.DepartmentCode))
            //  expr = expr.And(p => p.DepartmentCode == where.DepartmentCode);
            // if (!string.IsNullOrEmpty(where.DepartmentName))
            //  expr = expr.And(p => p.DepartmentName == where.DepartmentName);
            // if (!string.IsNullOrEmpty(where.BeginDate))
            //  expr = expr.And(p => p.BeginDate == where.BeginDate);
            // if (!string.IsNullOrEmpty(where.EndDate))
            //  expr = expr.And(p => p.EndDate == where.EndDate);
            // if (!string.IsNullOrEmpty(where.Remark))
            //  expr = expr.And(p => p.Remark == where.Remark);
            // if (!string.IsNullOrEmpty(where.CreatorUserCode))
            //  expr = expr.And(p => p.CreatorUserCode == where.CreatorUserCode);
            // if (!string.IsNullOrEmpty(where.CreateTime))
            //  expr = expr.And(p => p.CreateTime == where.CreateTime);
            // if (!string.IsNullOrEmpty(where.LastModifierUserCode))
            //  expr = expr.And(p => p.LastModifierUserCode == where.LastModifierUserCode);
            // if (!string.IsNullOrEmpty(where.LastModificationTime))
            //  expr = expr.And(p => p.LastModificationTime == where.LastModificationTime);
            // if (!string.IsNullOrEmpty(where.IsDelete))
            //  expr = expr.And(p => p.IsDelete == where.IsDelete);
            // if (!string.IsNullOrEmpty(where.State))
            //  expr = expr.And(p => p.State == where.State);
            // if (!string.IsNullOrEmpty(where.IsActive))
            //  expr = expr.And(p => p.IsActive == where.IsActive);
            if (!string.IsNullOrEmpty(where.ContractNo))
                expr = expr.And(p => p.ContractNo == where.ContractNo);
            // if (!string.IsNullOrEmpty(where.FirstParty))
            //  expr = expr.And(p => p.FirstParty == where.FirstParty);
            // if (!string.IsNullOrEmpty(where.SecondParty))
            //  expr = expr.And(p => p.SecondParty == where.SecondParty);
            // if (!string.IsNullOrEmpty(where.ContractContent))
            //  expr = expr.And(p => p.ContractContent == where.ContractContent);
            // if (!string.IsNullOrEmpty(where.IdentityCardNo))
            //  expr = expr.And(p => p.IdentityCardNo == where.IdentityCardNo);
            #endregion
            var list = _contractRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法

        #endregion
    }
}





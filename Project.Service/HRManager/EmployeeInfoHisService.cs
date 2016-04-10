
/***************************************************************************
*       功能：     HREmployeeInfoHis业务处理层
*       作者：     Roy
*       日期：     2016-02-16
*       描述：     通过FSate字段进行过滤是还是历史记录
  人员基础信息，如需要增加多字段请使用扩展表
* *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.HRManager;
using Project.Repository.HRManager;

namespace Project.Service.HRManager
{
    public class EmployeeInfoHisService
    {

        #region 构造函数
        private readonly EmployeeInfoHisRepository _employeeInfoHisRepository;
        private static readonly EmployeeInfoHisService Instance = new EmployeeInfoHisService();

        public EmployeeInfoHisService()
        {
            this._employeeInfoHisRepository = new EmployeeInfoHisRepository();
        }

        public static EmployeeInfoHisService GetInstance()
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
        public System.Int32 Add(EmployeeInfoHisEntity entity)
        {
            return _employeeInfoHisRepository.Save(entity);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
            try
            {
                var entity = _employeeInfoHisRepository.GetById(pkId);
                _employeeInfoHisRepository.Delete(entity);
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
        public bool Delete(EmployeeInfoHisEntity entity)
        {
            try
            {
                _employeeInfoHisRepository.Delete(entity);
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
        public bool Update(EmployeeInfoHisEntity entity)
        {
            try
            {
                _employeeInfoHisRepository.Update(entity);
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
        public EmployeeInfoHisEntity GetModelByPk(System.Int32 pkId)
        {
            return _employeeInfoHisRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【通过FSate字段进行过滤是还是历史记录
        ///人员基础信息，如需要增加多字段请使用扩展表】和总【通过FSate字段进行过滤是还是历史记录
        ///人员基础信息，如需要增加多字段请使用扩展表】数</returns>
        public System.Tuple<IList<EmployeeInfoHisEntity>, int> Search(EmployeeInfoHisEntity where, int skipResults, int maxResults)
        {
            var expr = PredicateBuilder.True<EmployeeInfoHisEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            // if (!string.IsNullOrEmpty(where.EmployeeID))
            //  expr = expr.And(p => p.EmployeeID == where.EmployeeID);
            if (!string.IsNullOrEmpty(where.EmployeeCode))
                expr = expr.And(p => p.EmployeeCode == where.EmployeeCode);
            // if (!string.IsNullOrEmpty(where.EmployeeName))
            //  expr = expr.And(p => p.EmployeeName == where.EmployeeName);
            // if (!string.IsNullOrEmpty(where.DepartmentCode))
            //  expr = expr.And(p => p.DepartmentCode == where.DepartmentCode);
            // if (!string.IsNullOrEmpty(where.JobName))
            //  expr = expr.And(p => p.JobName == where.JobName);
            // if (!string.IsNullOrEmpty(where.PayCode))
            //  expr = expr.And(p => p.PayCode == where.PayCode);
            // if (!string.IsNullOrEmpty(where.Sex))
            //  expr = expr.And(p => p.Sex == where.Sex);
            // if (!string.IsNullOrEmpty(where.CertNo))
            //  expr = expr.And(p => p.CertNo == where.CertNo);
            // if (!string.IsNullOrEmpty(where.Birthday))
            //  expr = expr.And(p => p.Birthday == where.Birthday);
            // if (!string.IsNullOrEmpty(where.TechnicalTitleName))
            //  expr = expr.And(p => p.TechnicalTitleName == where.TechnicalTitleName);
            // if (!string.IsNullOrEmpty(where.TechnicalTitle))
            //  expr = expr.And(p => p.TechnicalTitle == where.TechnicalTitle);
            // if (!string.IsNullOrEmpty(where.DutiesName))
            //  expr = expr.And(p => p.DutiesName == where.DutiesName);
            // if (!string.IsNullOrEmpty(where.Duties))
            //  expr = expr.And(p => p.Duties == where.Duties);
            // if (!string.IsNullOrEmpty(where.WorkingYears))
            //  expr = expr.And(p => p.WorkingYears == where.WorkingYears);
            // if (!string.IsNullOrEmpty(where.WorkState))
            //  expr = expr.And(p => p.WorkState == where.WorkState);
            // if (!string.IsNullOrEmpty(where.EmployeeType))
            //  expr = expr.And(p => p.EmployeeType == where.EmployeeType);
            // if (!string.IsNullOrEmpty(where.EmployeeTypeName))
            //  expr = expr.And(p => p.EmployeeTypeName == where.EmployeeTypeName);
            // if (!string.IsNullOrEmpty(where.HomeAddress))
            //  expr = expr.And(p => p.HomeAddress == where.HomeAddress);
            // if (!string.IsNullOrEmpty(where.MobileNO))
            //  expr = expr.And(p => p.MobileNO == where.MobileNO);
            // if (!string.IsNullOrEmpty(where.ImageUrl))
            //  expr = expr.And(p => p.ImageUrl == where.ImageUrl);
            // if (!string.IsNullOrEmpty(where.Sort))
            //  expr = expr.And(p => p.Sort == where.Sort);
            // if (!string.IsNullOrEmpty(where.State))
            //  expr = expr.And(p => p.State == where.State);
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
            // if (!string.IsNullOrEmpty(where.WorkStateName))
            //  expr = expr.And(p => p.WorkStateName == where.WorkStateName);
            expr = expr.And(p => p.IsInsert != 1);
            #endregion
            var list = _employeeInfoHisRepository.Query().Where(expr).OrderBy(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _employeeInfoHisRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<EmployeeInfoHisEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<EmployeeInfoHisEntity> GetList(EmployeeInfoHisEntity where)
        {
            var expr = PredicateBuilder.True<EmployeeInfoHisEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            // if (!string.IsNullOrEmpty(where.EmployeeID))
            //  expr = expr.And(p => p.EmployeeID == where.EmployeeID);
            if (!string.IsNullOrEmpty(where.EmployeeCode))
                expr = expr.And(p => p.EmployeeCode == where.EmployeeCode);
            // if (!string.IsNullOrEmpty(where.EmployeeName))
            //  expr = expr.And(p => p.EmployeeName == where.EmployeeName);
            // if (!string.IsNullOrEmpty(where.DepartmentCode))
            //  expr = expr.And(p => p.DepartmentCode == where.DepartmentCode);
            // if (!string.IsNullOrEmpty(where.JobName))
            //  expr = expr.And(p => p.JobName == where.JobName);
            // if (!string.IsNullOrEmpty(where.PayCode))
            //  expr = expr.And(p => p.PayCode == where.PayCode);
            // if (!string.IsNullOrEmpty(where.Sex))
            //  expr = expr.And(p => p.Sex == where.Sex);
            // if (!string.IsNullOrEmpty(where.CertNo))
            //  expr = expr.And(p => p.CertNo == where.CertNo);
            // if (!string.IsNullOrEmpty(where.Birthday))
            //  expr = expr.And(p => p.Birthday == where.Birthday);
            // if (!string.IsNullOrEmpty(where.TechnicalTitleName))
            //  expr = expr.And(p => p.TechnicalTitleName == where.TechnicalTitleName);
            // if (!string.IsNullOrEmpty(where.TechnicalTitle))
            //  expr = expr.And(p => p.TechnicalTitle == where.TechnicalTitle);
            // if (!string.IsNullOrEmpty(where.DutiesName))
            //  expr = expr.And(p => p.DutiesName == where.DutiesName);
            // if (!string.IsNullOrEmpty(where.Duties))
            //  expr = expr.And(p => p.Duties == where.Duties);
            // if (!string.IsNullOrEmpty(where.WorkingYears))
            //  expr = expr.And(p => p.WorkingYears == where.WorkingYears);
            // if (!string.IsNullOrEmpty(where.WorkState))
            //  expr = expr.And(p => p.WorkState == where.WorkState);
            // if (!string.IsNullOrEmpty(where.EmployeeType))
            //  expr = expr.And(p => p.EmployeeType == where.EmployeeType);
            // if (!string.IsNullOrEmpty(where.EmployeeTypeName))
            //  expr = expr.And(p => p.EmployeeTypeName == where.EmployeeTypeName);
            // if (!string.IsNullOrEmpty(where.HomeAddress))
            //  expr = expr.And(p => p.HomeAddress == where.HomeAddress);
            // if (!string.IsNullOrEmpty(where.MobileNO))
            //  expr = expr.And(p => p.MobileNO == where.MobileNO);
            // if (!string.IsNullOrEmpty(where.ImageUrl))
            //  expr = expr.And(p => p.ImageUrl == where.ImageUrl);
            // if (!string.IsNullOrEmpty(where.Sort))
            //  expr = expr.And(p => p.Sort == where.Sort);
            // if (!string.IsNullOrEmpty(where.State))
            //  expr = expr.And(p => p.State == where.State);
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
            // if (!string.IsNullOrEmpty(where.WorkStateName))
            //  expr = expr.And(p => p.WorkStateName == where.WorkStateName);
            #endregion
            var list = _employeeInfoHisRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法

        #endregion
    }
}





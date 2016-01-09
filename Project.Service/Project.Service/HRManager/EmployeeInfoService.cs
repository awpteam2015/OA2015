
/***************************************************************************
*       功能：     HREmployeeInfo业务处理层
*       作者：     ROY
*       日期：     2016-01-09
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
    public class EmployeeInfoService
    {

        #region 构造函数
        private readonly EmployeeInfoRepository _employeeInfoRepository;
        private static readonly EmployeeInfoService Instance = new EmployeeInfoService();

        public EmployeeInfoService()
        {
            this._employeeInfoRepository = new EmployeeInfoRepository();
        }

        public static EmployeeInfoService GetInstance()
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
        public System.Int32 Add(EmployeeInfoEntity entity)
        {
            return _employeeInfoRepository.Save(entity);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
            try
            {
                var entity = _employeeInfoRepository.GetById(pkId);
                _employeeInfoRepository.Delete(entity);
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
        public bool Delete(EmployeeInfoEntity entity)
        {
            try
            {
                _employeeInfoRepository.Delete(entity);
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
        public bool Update(EmployeeInfoEntity entity)
        {
            try
            {
                _employeeInfoRepository.Update(entity);
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
        public EmployeeInfoEntity GetModelByPk(System.Int32 pkId)
        {
            return _employeeInfoRepository.GetById(pkId);
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
        public System.Tuple<IList<EmployeeInfoEntity>, int> Search(EmployeeInfoEntity where, int skipResults, int maxResults)
        {
            var expr = PredicateBuilder.True<EmployeeInfoEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            // if (!string.IsNullOrEmpty(where.EmployeeCode))
            //  expr = expr.And(p => p.EmployeeCode == where.EmployeeCode);
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
            // if (!string.IsNullOrEmpty(where.TechnicalTitle))
            //  expr = expr.And(p => p.TechnicalTitle == where.TechnicalTitle);
            // if (!string.IsNullOrEmpty(where.Duties))
            //  expr = expr.And(p => p.Duties == where.Duties);
            // if (!string.IsNullOrEmpty(where.WorkState))
            //  expr = expr.And(p => p.WorkState == where.WorkState);
            // if (!string.IsNullOrEmpty(where.EmployeeType))
            //  expr = expr.And(p => p.EmployeeType == where.EmployeeType);
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
            #endregion
            var list = _employeeInfoRepository.Query().Where(expr).OrderBy(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _employeeInfoRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<EmployeeInfoEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<EmployeeInfoEntity> GetList(EmployeeInfoEntity where)
        {
            var expr = PredicateBuilder.True<EmployeeInfoEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            // if (!string.IsNullOrEmpty(where.EmployeeCode))
            //  expr = expr.And(p => p.EmployeeCode == where.EmployeeCode);
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
            // if (!string.IsNullOrEmpty(where.TechnicalTitle))
            //  expr = expr.And(p => p.TechnicalTitle == where.TechnicalTitle);
            // if (!string.IsNullOrEmpty(where.Duties))
            //  expr = expr.And(p => p.Duties == where.Duties);
            // if (!string.IsNullOrEmpty(where.WorkState))
            //  expr = expr.And(p => p.WorkState == where.WorkState);
            // if (!string.IsNullOrEmpty(where.EmployeeType))
            //  expr = expr.And(p => p.EmployeeType == where.EmployeeType);
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
            #endregion
            var list = _employeeInfoRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法

        #endregion
    }
}






/***************************************************************************
*       功能：     HRWorkExperience业务处理层
*       作者：     Roy
*       日期：     2016-01-17
*       描述：     员工工作经历记录
* *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.HRManager;
using Project.Repository.HRManager;

namespace Project.Service.HRManager
{
    public class WorkExperienceService
    {

        #region 构造函数
        private readonly WorkExperienceRepository _workExperienceRepository;
        private static readonly WorkExperienceService Instance = new WorkExperienceService();

        public WorkExperienceService()
        {
            this._workExperienceRepository = new WorkExperienceRepository();
        }

        public static WorkExperienceService GetInstance()
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
        public System.Int32 Add(WorkExperienceEntity entity)
        {
            return _workExperienceRepository.Save(entity);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
            try
            {
                var entity = _workExperienceRepository.GetById(pkId);
                _workExperienceRepository.Delete(entity);
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
        public bool Delete(WorkExperienceEntity entity)
        {
            try
            {
                _workExperienceRepository.Delete(entity);
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
        public bool Update(WorkExperienceEntity entity)
        {
            try
            {
                _workExperienceRepository.Update(entity);
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
        public WorkExperienceEntity GetModelByPk(System.Int32 pkId)
        {
            return _workExperienceRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【员工工作经历记录】和总【员工工作经历记录】数</returns>
        public System.Tuple<IList<WorkExperienceEntity>, int> Search(WorkExperienceEntity where, int skipResults, int maxResults)
        {
            var expr = PredicateBuilder.True<WorkExperienceEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            // if (!string.IsNullOrEmpty(where.EmployeeCode))
            //  expr = expr.And(p => p.EmployeeCode == where.EmployeeCode);
            // if (!string.IsNullOrEmpty(where.DepartmentCode))
            //  expr = expr.And(p => p.DepartmentCode == where.DepartmentCode);
            // if (!string.IsNullOrEmpty(where.WorkCompany))
            //  expr = expr.And(p => p.WorkCompany == where.WorkCompany);
            // if (!string.IsNullOrEmpty(where.Duties))
            //  expr = expr.And(p => p.Duties == where.Duties);
            // if (!string.IsNullOrEmpty(where.BeginDate))
            //  expr = expr.And(p => p.BeginDate == where.BeginDate);
            // if (!string.IsNullOrEmpty(where.EndDate))
            //  expr = expr.And(p => p.EndDate == where.EndDate);
            // if (!string.IsNullOrEmpty(where.WorkContent))
            //  expr = expr.And(p => p.WorkContent == where.WorkContent);
            // if (!string.IsNullOrEmpty(where.LeaveReason))
            //  expr = expr.And(p => p.LeaveReason == where.LeaveReason);
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
            var list = _workExperienceRepository.Query().Where(expr).OrderBy(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _workExperienceRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<WorkExperienceEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<WorkExperienceEntity> GetList(WorkExperienceEntity where)
        {
            var expr = PredicateBuilder.True<WorkExperienceEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);

            expr = expr.And(p => p.EmployeeID == where.EmployeeID);
            // if (!string.IsNullOrEmpty(where.DepartmentCode))
            //  expr = expr.And(p => p.DepartmentCode == where.DepartmentCode);
            // if (!string.IsNullOrEmpty(where.WorkCompany))
            //  expr = expr.And(p => p.WorkCompany == where.WorkCompany);
            // if (!string.IsNullOrEmpty(where.Duties))
            //  expr = expr.And(p => p.Duties == where.Duties);
            // if (!string.IsNullOrEmpty(where.BeginDate))
            //  expr = expr.And(p => p.BeginDate == where.BeginDate);
            // if (!string.IsNullOrEmpty(where.EndDate))
            //  expr = expr.And(p => p.EndDate == where.EndDate);
            // if (!string.IsNullOrEmpty(where.WorkContent))
            //  expr = expr.And(p => p.WorkContent == where.WorkContent);
            // if (!string.IsNullOrEmpty(where.LeaveReason))
            //  expr = expr.And(p => p.LeaveReason == where.LeaveReason);
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
            var list = _workExperienceRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法

        #endregion
    }
}





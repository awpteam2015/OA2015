
/***************************************************************************
*       功能：     HREmployeeRotate业务处理层
*       作者：     ROY
*       日期：     2016-01-09
*       描述：     用于 轮转人员计划
  (轮转人员的设置放入组管理，新建一个组进行管理)
* *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.HRManager;
using Project.Repository.HRManager;

namespace Project.Service.HRManager
{
    public class EmployeeRotateService
    {

        #region 构造函数
        private readonly EmployeeRotateRepository _employeeRotateRepository;
        private static readonly EmployeeRotateService Instance = new EmployeeRotateService();

        public EmployeeRotateService()
        {
            this._employeeRotateRepository = new EmployeeRotateRepository();
        }

        public static EmployeeRotateService GetInstance()
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
        public System.Int32 Add(EmployeeRotateEntity entity)
        {
            return _employeeRotateRepository.Save(entity);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
            try
            {
                var entity = _employeeRotateRepository.GetById(pkId);
                _employeeRotateRepository.Delete(entity);
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
        public bool Delete(EmployeeRotateEntity entity)
        {
            try
            {
                _employeeRotateRepository.Delete(entity);
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
        public bool Update(EmployeeRotateEntity entity)
        {
            try
            {
                _employeeRotateRepository.Update(entity);
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
        public EmployeeRotateEntity GetModelByPk(System.Int32 pkId)
        {
            return _employeeRotateRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【用于 轮转人员计划
        ///(轮转人员的设置放入组管理，新建一个组进行管理)】和总【用于 轮转人员计划
        ///(轮转人员的设置放入组管理，新建一个组进行管理)】数</returns>
        public System.Tuple<IList<EmployeeRotateEntity>, int> Search(EmployeeRotateEntity where, int skipResults, int maxResults)
        {
            var expr = PredicateBuilder.True<EmployeeRotateEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            // if (!string.IsNullOrEmpty(where.EmployeeCode))
            //  expr = expr.And(p => p.EmployeeCode == where.EmployeeCode);
            // if (!string.IsNullOrEmpty(where.DepartmentCode))
            //  expr = expr.And(p => p.DepartmentCode == where.DepartmentCode);
            // if (!string.IsNullOrEmpty(where.RotatEDetpCode))
            //  expr = expr.And(p => p.RotatEDetpCode == where.RotatEDetpCode);
            // if (!string.IsNullOrEmpty(where.BeginDate))
            //  expr = expr.And(p => p.BeginDate == where.BeginDate);
            // if (!string.IsNullOrEmpty(where.EenData))
            //  expr = expr.And(p => p.EenData == where.EenData);
            // if (!string.IsNullOrEmpty(where.Evaluate))
            //  expr = expr.And(p => p.Evaluate == where.Evaluate);
            // if (!string.IsNullOrEmpty(where.EvaluatePersone))
            //  expr = expr.And(p => p.EvaluatePersone == where.EvaluatePersone);
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
            var list = _employeeRotateRepository.Query().Where(expr).OrderBy(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _employeeRotateRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<EmployeeRotateEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<EmployeeRotateEntity> GetList(EmployeeRotateEntity where)
        {
            var expr = PredicateBuilder.True<EmployeeRotateEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            // if (!string.IsNullOrEmpty(where.EmployeeCode))
            //  expr = expr.And(p => p.EmployeeCode == where.EmployeeCode);
            // if (!string.IsNullOrEmpty(where.DepartmentCode))
            //  expr = expr.And(p => p.DepartmentCode == where.DepartmentCode);
            // if (!string.IsNullOrEmpty(where.RotatEDetpCode))
            //  expr = expr.And(p => p.RotatEDetpCode == where.RotatEDetpCode);
            // if (!string.IsNullOrEmpty(where.BeginDate))
            //  expr = expr.And(p => p.BeginDate == where.BeginDate);
            // if (!string.IsNullOrEmpty(where.EenData))
            //  expr = expr.And(p => p.EenData == where.EenData);
            // if (!string.IsNullOrEmpty(where.Evaluate))
            //  expr = expr.And(p => p.Evaluate == where.Evaluate);
            // if (!string.IsNullOrEmpty(where.EvaluatePersone))
            //  expr = expr.And(p => p.EvaluatePersone == where.EvaluatePersone);
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
            var list = _employeeRotateRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法

        #endregion
    }
}





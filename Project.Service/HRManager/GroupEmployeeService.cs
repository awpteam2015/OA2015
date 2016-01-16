
/***************************************************************************
*       功能：     HRGroupEmployee业务处理层
*       作者：     ROY
*       日期：     2016-01-09
*       描述：     组成员情况
* *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.HRManager;
using Project.Repository.HRManager;

namespace Project.Service.HRManager
{
    public class GroupEmployeeService
    {

        #region 构造函数
        private readonly GroupEmployeeRepository _groupEmployeeRepository;
        private static readonly GroupEmployeeService Instance = new GroupEmployeeService();

        public GroupEmployeeService()
        {
            this._groupEmployeeRepository = new GroupEmployeeRepository();
        }

        public static GroupEmployeeService GetInstance()
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
        public System.Int32 Add(GroupEmployeeEntity entity)
        {
            return _groupEmployeeRepository.Save(entity);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
            try
            {
                var entity = _groupEmployeeRepository.GetById(pkId);
                _groupEmployeeRepository.Delete(entity);
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
        public bool Delete(GroupEmployeeEntity entity)
        {
            try
            {
                _groupEmployeeRepository.Delete(entity);
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
        public bool Update(GroupEmployeeEntity entity)
        {
            try
            {
                _groupEmployeeRepository.Update(entity);
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
        public GroupEmployeeEntity GetModelByPk(System.Int32 pkId)
        {
            return _groupEmployeeRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【组成员情况】和总【组成员情况】数</returns>
        public System.Tuple<IList<GroupEmployeeEntity>, int> Search(GroupEmployeeEntity where, int skipResults, int maxResults)
        {
            var expr = PredicateBuilder.True<GroupEmployeeEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            if (!string.IsNullOrEmpty(where.GroupCode))
                expr = expr.And(p => p.GroupCode == where.GroupCode);
            // if (!string.IsNullOrEmpty(where.EmployeeCode))
            //  expr = expr.And(p => p.EmployeeCode == where.EmployeeCode);
            // if (!string.IsNullOrEmpty(where.CreatorUserCode))
            //  expr = expr.And(p => p.CreatorUserCode == where.CreatorUserCode);
            // if (!string.IsNullOrEmpty(where.CreatorUserName))
            //  expr = expr.And(p => p.CreatorUserName == where.CreatorUserName);
            // if (!string.IsNullOrEmpty(where.CreateTime))
            //  expr = expr.And(p => p.CreateTime == where.CreateTime);
            #endregion
            var list = _groupEmployeeRepository.Query().Where(expr).OrderBy(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _groupEmployeeRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<GroupEmployeeEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<GroupEmployeeEntity> GetList(GroupEmployeeEntity where)
        {
            var expr = PredicateBuilder.True<GroupEmployeeEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            if (!string.IsNullOrEmpty(where.GroupCode))
                expr = expr.And(p => p.GroupCode == where.GroupCode);
            // if (!string.IsNullOrEmpty(where.EmployeeCode))
            //  expr = expr.And(p => p.EmployeeCode == where.EmployeeCode);
            // if (!string.IsNullOrEmpty(where.CreatorUserCode))
            //  expr = expr.And(p => p.CreatorUserCode == where.CreatorUserCode);
            // if (!string.IsNullOrEmpty(where.CreatorUserName))
            //  expr = expr.And(p => p.CreatorUserName == where.CreatorUserName);
            // if (!string.IsNullOrEmpty(where.CreateTime))
            //  expr = expr.And(p => p.CreateTime == where.CreateTime);
            #endregion
            var list = _groupEmployeeRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法

        #endregion
    }
}





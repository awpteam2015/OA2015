
/***************************************************************************
*       功能：     PMDepartment业务处理层
*       作者：     李伟伟
*       日期：     2015/12/23
*       描述：     部门基础信息表
* *************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;
using Project.Model.PermissionManager;
using Project.Repository.PermissionManager;

namespace Project.Service.PermissionManager
{
    public class DepartmentService
    {

        #region 构造函数
        private readonly DepartmentRepository _departmentRepository;
        private static readonly DepartmentService Instance = new DepartmentService();

        public DepartmentService()
        {
            this._departmentRepository = new DepartmentRepository();
        }

        public static DepartmentService GetInstance()
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
        public System.Int32 Add(DepartmentEntity entity)
        {
            return _departmentRepository.Save(entity);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
            try
            {
                var entity = _departmentRepository.GetById(pkId);
                _departmentRepository.Delete(entity);
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
        public bool Delete(DepartmentEntity entity)
        {
            try
            {
                _departmentRepository.Delete(entity);
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
        public bool Update(DepartmentEntity entity)
        {
            try
            {
                _departmentRepository.Update(entity);
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
        public DepartmentEntity GetModelByPk(System.Int32 pkId)
        {
            return _departmentRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【部门基础信息表】和总【部门基础信息表】数</returns>
        public System.Tuple<IList<DepartmentEntity>, int> Search(DepartmentEntity entity, int skipResults, int maxResults)
        {
            var expr = PredicateBuilder.True<DepartmentEntity>();
            #region
            // if (!string.IsNullOrEmpty(entity.PkId))
            //  expr = expr.And(p => p.PkId == entity.PkId);
            // if (!string.IsNullOrEmpty(entity.DepartmentCode))
            //  expr = expr.And(p => p.DepartmentCode == entity.DepartmentCode);
            // if (!string.IsNullOrEmpty(entity.DepartmentName))
            //  expr = expr.And(p => p.DepartmentName == entity.DepartmentName);
            // if (!string.IsNullOrEmpty(entity.ParentDepartmentCode))
            //  expr = expr.And(p => p.ParentDepartmentCode == entity.ParentDepartmentCode);
            // if (!string.IsNullOrEmpty(entity.Remark))
            //  expr = expr.And(p => p.Remark == entity.Remark);
            #endregion
            var list = _departmentRepository.Query().Where(expr).OrderBy(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _departmentRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<DepartmentEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<DepartmentEntity> GetList(DepartmentEntity entity)
        {
            var expr = PredicateBuilder.True<DepartmentEntity>();
            #region
            if (!string.IsNullOrEmpty(entity.DepartmentCode))
                expr = expr.And(p => p.DepartmentCode == entity.DepartmentCode);
            if (!string.IsNullOrEmpty(entity.DepartmentName))
                expr = expr.And(p => p.DepartmentName == entity.DepartmentName);
            if (!string.IsNullOrEmpty(entity.ParentDepartmentCode))
                expr = expr.And(p => p.ParentDepartmentCode == entity.ParentDepartmentCode);
            if (!string.IsNullOrEmpty(entity.Remark))
                expr = expr.And(p => p.Remark.Contains(entity.Remark));
            #endregion
            var list = _departmentRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法

        public IList<DepartmentEntity> GetTreeList(DepartmentEntity entity)
        {
            var listAll = this.GetList(entity);
            listAll = GetChildList(listAll, null);
            return listAll;
        }

        private IList<DepartmentEntity> GetChildList(IList<DepartmentEntity> allList, DepartmentEntity parentDepartmentEntity)
        {
            var list = new List<DepartmentEntity>();
            if (parentDepartmentEntity == null)
            {
                list.AddRange(allList.Where(p => p.ParentDepartmentCode == "0"));
                list.ForEach(p =>
                {
                     GetChildList(allList, p);
                });
            }
            else
            {
                var childList = allList.Where(p => p.ParentDepartmentCode == parentDepartmentEntity.DepartmentCode).ToList();
                if (childList.Any())
                {
                    parentDepartmentEntity.children = childList;
                    childList.ForEach(p =>
                    {
                        GetChildList(allList, p);
                    });
                }
            }
            return list;
        }



        #endregion
    }
}





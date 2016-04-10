
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
using Project.Service.PermissionManager.Validate;
using AutoMapper;

namespace Project.Service.PermissionManager
{
    public class DepartmentService
    {

        #region 构造函数
        private readonly DepartmentRepository _departmentRepository;

        private readonly RoleDepartmentRepository _roleDepartmentRepository;

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
        public Tuple<bool, string> Add(DepartmentEntity entity)
        {
            var validateResult = DepartmentValidate.GetInstance().IsHasSameDepartmentCode(entity.DepartmentCode);
            if (!validateResult.Item1)
            {
                return validateResult;
            }
            var addResult = _departmentRepository.Save(entity);
            if (addResult > 0)
            {
                return new Tuple<bool, string>(true, "");
            }
            else
            {
                return new Tuple<bool, string>(false, "");
            }
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
        public Tuple<bool, string> Update(DepartmentEntity entity)
        {
            var validateResult = DepartmentValidate.GetInstance().IsHasSameDepartmentCode(entity.DepartmentCode, entity.PkId);
            if (!validateResult.Item1)
            {
                return validateResult;
            }

            try
            {
                _departmentRepository.Merge(entity);
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
        public DepartmentEntity GetModelByPk(System.Int32 pkId)
        {
            return _departmentRepository.GetById(pkId);
        }

        public DepartmentEntity GetModelByDepartmentCode(System.String departmentCode)
        {
            var expr = PredicateBuilder.True<DepartmentEntity>();
            #region
            if (!string.IsNullOrEmpty(departmentCode))
                expr = expr.And(p => p.DepartmentCode == departmentCode);
            #endregion
            var list = _departmentRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list.FirstOrDefault();
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

        //public string GetChildDepartmentCode()
        //{  var listAll = this.GetList(new DepartmentEntity());
        //   GetChildList(listAll,new DepartmentEntity(){DepartmentCode = "01"});
        //    return "";
        //}



        public IList<DepartmentEntity> GetTreeList(DepartmentEntity entity, bool isShowTop = false)
        {
            var listAll = this.GetList(entity);

            var list = new List<DepartmentEntity>();
            if (isShowTop)
            {
                list.Add(new DepartmentEntity() { DepartmentCode = "0", DepartmentName = "顶级节点" });
            }
            else
            {
                list.AddRange(listAll.Where(p => p.ParentDepartmentCode == "0"));
            }

            list.ForEach(p =>
            {
                GetChildList(listAll, p);
            }
            );

            return list;
        }

        public IList<DepartmentEntity> GetTreeList(DepartmentEntity entity, List<UserDepartmentEntity> sourceList, bool isAdmin, bool isShowTop = false)
        {

            var listAll = this.GetList(entity);

            if (!isAdmin)
            {
                for (int i = 0; i < listAll.Count; i++)
                {
                    if (!sourceList.Any(p => p.DepartmentCode == listAll[i].DepartmentCode))
                    {
                        listAll.RemoveAt(i);
                        i--;
                    }

                }

                if (!listAll.Any(item => item.ParentDepartmentCode == "0"))
                {
                    listAll.ForEach(p => p.ParentDepartmentCode = (p.ParentDepartmentCode == "330110" ? "0" : p.ParentDepartmentCode));
                }
            }

            var list = new List<DepartmentEntity>();
            if (isShowTop)
            {
                list.Add(new DepartmentEntity() { DepartmentCode = "0", DepartmentName = "顶级节点" });
            }
            else
            {
                list.AddRange(listAll.Where(p => p.ParentDepartmentCode == "0"));
            }

            list.ForEach(p =>
            {
                GetChildList(listAll, p);
            }
            );

            return list;
        }


        private void GetChildList(IList<DepartmentEntity> allList, DepartmentEntity parentDepartmentEntity)
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

        public string[] GetChiledArr(System.String departmentCode, List<UserDepartmentEntity> userDepartList, bool isAdmin)
        {
            if(isAdmin&& (string.IsNullOrEmpty(departmentCode)|| departmentCode == "0"))
                return new string[] { };
            if (string.IsNullOrEmpty(departmentCode) || departmentCode == "0")
            {
                var retList= userDepartList.Select(p => p.DepartmentCode).ToList().ToArray();
                if (retList.Length <= 0)
                    return new string[] {"999999"};
                return retList;
            }
            return new string[] { departmentCode };

            var parentDepartmentEntity = GetModelByDepartmentCode(departmentCode);
            var extEntity = new DepartmentExtEntity() { childrenAll = new List<DepartmentEntity>() };

            var listAll = this.GetList(new DepartmentEntity());
            GetChildList(listAll, parentDepartmentEntity);

            Mapper.Map(extEntity, parentDepartmentEntity);
            GetChildArry(extEntity, parentDepartmentEntity);
            extEntity.childrenAll.Add(parentDepartmentEntity);
            // parentDepartmentEntity.children.ForEach()
            return extEntity.childrenAll.Select(item => item.DepartmentCode).ToList().ToArray();
        }

        private void GetChildArry(DepartmentExtEntity sourceDepartmentEntity, DepartmentEntity parentDepartmentEntity)
        {

            var childList = parentDepartmentEntity.children;

            if (childList != null && childList.Any())
            {
                foreach (var item in childList)
                {
                    sourceDepartmentEntity.childrenAll.Add(item);
                    GetChildArry(sourceDepartmentEntity, item);
                };
            }

        }

        /// <summary>
        /// 角色所对用的部门详情
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<int> GetDepartList_Checked(int roleId)
        {
            return _roleDepartmentRepository.Query().Where(p => p.RoleId == roleId).Select(p => p.DepartId).ToList();
        }

        #endregion
    }
}





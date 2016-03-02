
/***************************************************************************
*       功能：     PMRole业务处理层
*       作者：     李伟伟
*       日期：     2015/12/23
*       描述：     EC系统角色列表
* *************************************************************************/

using System;
using System.Linq;
using System.Collections.Generic;
using NHibernate.Type;
using Project.Infrastructure.FrameworkCore.DataNhibernate;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.PermissionManager;
using Project.Repository.PermissionManager;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;

namespace Project.Service.PermissionManager
{
    public class RoleService
    {

        #region 构造函数
        private readonly RoleRepository _roleRepository;
        private readonly RoleFunctionDetailRepository _roleFunctionDetailRepository;
        private readonly FunctionDetailRepository _functionDetailRepository;


        private static readonly RoleService Instance = new RoleService();

        public RoleService()
        {
            this._roleRepository = new RoleRepository();
            this._roleFunctionDetailRepository = new RoleFunctionDetailRepository();
            this._functionDetailRepository = new FunctionDetailRepository();
        }

        public static RoleService GetInstance()
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
        public System.Int32 Add(RoleEntity entity)
        {
            return _roleRepository.Save(entity);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
            try
            {
                var entity = _roleRepository.GetById(pkId);
                _roleRepository.Delete(entity);
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
        public bool Delete(RoleEntity entity)
        {
            try
            {
                _roleRepository.Delete(entity);
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
        public bool Update(RoleEntity entity)
        {
            try
            {
                _roleRepository.Update(entity);
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
        public RoleEntity GetModelByPk(System.Int32 pkId)
        {
            return _roleRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【EC系统角色列表】和总【EC系统角色列表】数</returns>
        public System.Tuple<IList<RoleEntity>, int> Search(RoleEntity entity, int skipResults, int maxResults)
        {
            var expr = PredicateBuilder.True<RoleEntity>();
            #region
            // if (!string.IsNullOrEmpty(entity.PkId))
            //  expr = expr.And(p => p.PkId == entity.PkId);
            if (!string.IsNullOrEmpty(entity.RoleName))
                expr = expr.And(p => p.RoleName.Contains(entity.RoleName));
            // if (!string.IsNullOrEmpty(entity.Remark))
            //  expr = expr.And(p => p.Remark == entity.Remark);
            #endregion
            var list = _roleRepository.Query().Where(expr).OrderBy(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _roleRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<RoleEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<RoleEntity> GetList(RoleEntity entity)
        {
            var expr = PredicateBuilder.True<RoleEntity>();
            #region
            // if (!string.IsNullOrEmpty(entity.PkId))
            //  expr = expr.And(p => p.PkId == entity.PkId);
            // if (!string.IsNullOrEmpty(entity.RoleName))
            //  expr = expr.And(p => p.RoleName == entity.RoleName);
            // if (!string.IsNullOrEmpty(entity.Remark))
            //  expr = expr.And(p => p.Remark == entity.Remark);
            #endregion
            var list = _roleRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }

        /// <summary>
        /// 获取角色对应权限
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IList<RoleFunctionDetailEntity> GetRoleFunctionDetailList(RoleFunctionDetailEntity entity)
        {
            var expr = PredicateBuilder.True<RoleFunctionDetailEntity>();
            #region
            if (entity.RoleId > 0)
                expr = expr.And(p => p.RoleId == entity.RoleId);
            // if (!string.IsNullOrEmpty(entity.RoleName))
            //  expr = expr.And(p => p.RoleName == entity.RoleName);
            // if (!string.IsNullOrEmpty(entity.Remark))
            //  expr = expr.And(p => p.Remark == entity.Remark);
            #endregion
            var list = _roleFunctionDetailRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }

        /// <summary>
        /// 角色所对用的权限详情
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public List<int> GetFunctionDetailList_Checked(int roleId)
        {
            return _roleFunctionDetailRepository.Query().Where(p => p.RoleId == roleId).Select(p => p.FunctionDetailId).ToList();
        }

        #endregion


        #region 新增方法
        /// <summary>
        /// 设置角色权限
        /// </summary>
        public bool SetRowFunction(int roleId, int functionPkId, int functionDetailPkId, bool isCheck)
        {
            using (var tx = NhTransactionHelper.BeginTransaction())
            {
                try
                {
                    var functionDetailList = new List<FunctionDetailEntity>();
                    if (functionPkId > 0)
                    {
                        functionDetailList = FunctionService.GetInstance().GetFunctionDetailList(new FunctionDetailEntity() { FunctionId = functionPkId }).ToList();
                    }
                    else
                    {
                        functionDetailList.Add(_functionDetailRepository.GetById(functionDetailPkId));
                    }


                    var oldRoleFunctionDetailList = GetRoleFunctionDetailList(new RoleFunctionDetailEntity() { RoleId = roleId });
                    if (isCheck)
                    {

                        var newRoleFunctionDetailList = functionDetailList.Where(p => oldRoleFunctionDetailList.All(x => p.PkId != x.FunctionDetailId));

                        newRoleFunctionDetailList.ForEach(p =>
                               {
                                   var temp = new RoleFunctionDetailEntity
                                   {
                                       FunctionDetailId = p.PkId,
                                       FunctionId = p.FunctionId,
                                       RoleId = roleId
                                   };
                                   _roleFunctionDetailRepository.Save(temp);
                               });
                    }
                    else
                    {
                        functionDetailList.ForEach(p =>
                        {
                            _roleFunctionDetailRepository.Delete(oldRoleFunctionDetailList.Single(x => x.FunctionDetailId == p.PkId));
                        });
                    }
                    tx.Commit();
                    return true;
                }
                catch (Exception e)
                {
                    tx.Rollback();
                    throw;
                }
            }
        }

        #endregion
    }
}





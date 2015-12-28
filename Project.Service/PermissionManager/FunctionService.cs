
/***************************************************************************
*       功能：     PMFunction业务处理层
*       作者：     李伟伟
*       日期：     2015/12/23
*       描述：     模块功能
* *************************************************************************/

using System;
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.PermissionManager;
using Project.Repository.PermissionManager;
using Project.Infrastructure.FrameworkCore.ToolKit.LinqExpansion;


namespace Project.Service.PermissionManager
{
    public class FunctionService
    {

        #region 构造函数
        private readonly FunctionRepository _functionRepository;
        private readonly FunctionDetailRepository _functionDetailRepository;
        private static readonly FunctionService Instance = new FunctionService();

        public FunctionService()
        {
            this._functionRepository = new FunctionRepository();
            this._functionDetailRepository = new FunctionDetailRepository();
        }

        public static FunctionService GetInstance()
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
        public System.Int32 Add(FunctionEntity entity)
        {
            return _functionRepository.Save(entity);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
            try
            {
                var entity = _functionRepository.GetById(pkId);
                _functionRepository.Delete(entity);
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
        public bool Delete(FunctionEntity entity)
        {
            try
            {
                _functionRepository.Delete(entity);
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
        public bool Update(FunctionEntity entity)
        {
            var oldEntity = FunctionService.GetInstance().GetModelByPk(entity.PkId);

            var date = DateTime.Now;
            entity.FunctionDetailList.ToList().ForEach(p =>
            {
                if (p.PkId < 0)
                {
                    p.CreationTime = date;
                    p.CreatorUserCode = "";
                }
                else
                {
                    var oldRowEntity = oldEntity.FunctionDetailList.SingleOrDefault(x => x.PkId == p.PkId);
                    p.CreationTime = oldRowEntity.CreationTime;
                    p.CreatorUserCode = oldRowEntity.CreatorUserCode;
                }
                p.FunctionId = entity.PkId;
                p.LastModificationTime = date;
                p.LastModifierUserCode = "";
            });

            var deleteList = oldEntity.FunctionDetailList.Where(
                    p => entity.FunctionDetailList.All(x => x.PkId != p.PkId)).ToList();

            using (var tx = NhTransactionHelper.BeginTransaction())
            {
                try
                {
                    _functionRepository.Merge(entity);
                    deleteList.ForEach(p => { _functionDetailRepository.Delete(p); });
                    tx.Commit();
                    return true;
                }
                catch
                {
                    tx.Rollback();
                    throw;
                }
            }
        }


        /// <summary>
        /// 通过主键获取实体
        /// </summary>
        /// <param name="pkId">主键</param>
        /// <returns></returns>
        public FunctionEntity GetModelByPk(System.Int32 pkId)
        {
            return _functionRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【模块功能】和总【模块功能】数</returns>
        public System.Tuple<IList<FunctionEntity>, int> Search(FunctionEntity entity, int skipResults, int maxResults)
        {
            var expr = PredicateBuilder.True<FunctionEntity>();
            #region
            // if (!string.IsNullOrEmpty(entity.PkId))
            //  expr = expr.And(p => p.PkId == entity.PkId);
            // if (!string.IsNullOrEmpty(entity.FunctionnName))
            //  expr = expr.And(p => p.FunctionnName == entity.FunctionnName);
            // if (!string.IsNullOrEmpty(entity.ModuleId))
            //  expr = expr.And(p => p.ModuleId == entity.ModuleId);
            // if (!string.IsNullOrEmpty(entity.FunctionUrl))
            //  expr = expr.And(p => p.FunctionUrl == entity.FunctionUrl);
            // if (!string.IsNullOrEmpty(entity.Area))
            //  expr = expr.And(p => p.Area == entity.Area);
            // if (!string.IsNullOrEmpty(entity.Controller))
            //  expr = expr.And(p => p.Controller == entity.Controller);
            // if (!string.IsNullOrEmpty(entity.Action))
            //  expr = expr.And(p => p.Action == entity.Action);
            // if (!string.IsNullOrEmpty(entity.IsDisplayOnMenu))
            //  expr = expr.And(p => p.IsDisplayOnMenu == entity.IsDisplayOnMenu);
            // if (!string.IsNullOrEmpty(entity.RankId))
            //  expr = expr.And(p => p.RankId == entity.RankId);
            // if (!string.IsNullOrEmpty(entity.Remark))
            //  expr = expr.And(p => p.Remark == entity.Remark);
            #endregion
            var list = _functionRepository.Query().Where(expr).OrderBy(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _functionRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<FunctionEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<FunctionEntity> GetList(FunctionEntity entity)
        {
            var expr = PredicateBuilder.True<FunctionEntity>();
            #region
            // if (!string.IsNullOrEmpty(entity.PkId))
            //  expr = expr.And(p => p.PkId == entity.PkId);
            // if (!string.IsNullOrEmpty(entity.FunctionnName))
            //  expr = expr.And(p => p.FunctionnName == entity.FunctionnName);
            // if (!string.IsNullOrEmpty(entity.ModuleId))
            //  expr = expr.And(p => p.ModuleId == entity.ModuleId);
            // if (!string.IsNullOrEmpty(entity.FunctionUrl))
            //  expr = expr.And(p => p.FunctionUrl == entity.FunctionUrl);
            // if (!string.IsNullOrEmpty(entity.Area))
            //  expr = expr.And(p => p.Area == entity.Area);
            // if (!string.IsNullOrEmpty(entity.Controller))
            //  expr = expr.And(p => p.Controller == entity.Controller);
            // if (!string.IsNullOrEmpty(entity.Action))
            //  expr = expr.And(p => p.Action == entity.Action);
            // if (!string.IsNullOrEmpty(entity.IsDisplayOnMenu))
            //  expr = expr.And(p => p.IsDisplayOnMenu == entity.IsDisplayOnMenu);
            // if (!string.IsNullOrEmpty(entity.RankId))
            //  expr = expr.And(p => p.RankId == entity.RankId);
            // if (!string.IsNullOrEmpty(entity.Remark))
            //  expr = expr.And(p => p.Remark == entity.Remark);
            #endregion
            var list = _functionRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法

        #endregion
    }
}





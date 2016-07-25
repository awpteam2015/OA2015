
 /***************************************************************************
 *       功能：     RMRiverProblemApply业务处理层
 *       作者：     李伟伟
 *       日期：     2016/7/24
 *       描述：     河道问题申请单
 * *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.RiverManager;
using Project.Repository.RiverManager;

namespace Project.Service.RiverManager
{
    public class RiverProblemApplyService
    {
       
       #region 构造函数
        private readonly RiverProblemApplyRepository  _riverProblemApplyRepository;
            private static readonly RiverProblemApplyService Instance = new RiverProblemApplyService();

        public RiverProblemApplyService()
        {
           this._riverProblemApplyRepository =new RiverProblemApplyRepository();
        }
        
         public static  RiverProblemApplyService GetInstance()
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
        public System.Int32 Add(RiverProblemApplyEntity entity)
        {
            return _riverProblemApplyRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _riverProblemApplyRepository.GetById(pkId);
            _riverProblemApplyRepository.Delete(entity);
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
        public bool Delete(RiverProblemApplyEntity entity)
        {
         try
            {
            _riverProblemApplyRepository.Delete(entity);
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
        public bool Update(RiverProblemApplyEntity entity)
        {
          try
            {
            _riverProblemApplyRepository.Update(entity);
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
        public RiverProblemApplyEntity GetModelByPk(System.Int32 pkId)
        {
            return _riverProblemApplyRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【河道问题申请单】和总【河道问题申请单】数</returns>
        public System.Tuple<IList<RiverProblemApplyEntity>, int> Search(RiverProblemApplyEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<RiverProblemApplyEntity>();
            #region
            // if (!string.IsNullOrEmpty(where.PkId))
            //  expr = expr.And(p => p.PkId == where.PkId);
            if (!string.IsNullOrEmpty(where.Title))
                expr = expr.And(p => p.Title.Contains(where.Title) );
            // if (!string.IsNullOrEmpty(where.Des))
            //  expr = expr.And(p => p.Des == where.Des);
            if (where.ProblemType>0)
                expr = expr.And(p => p.ProblemType == where.ProblemType);
            // if (!string.IsNullOrEmpty(where.PicUrl))
            //  expr = expr.And(p => p.PicUrl == where.PicUrl);
            if (!string.IsNullOrEmpty(where.DepartmentCode)&& where.DepartmentCode!="0")
                expr = expr.And(p => p.DepartmentCode == where.DepartmentCode);
            // if (!string.IsNullOrEmpty(where.RiverId))
            //  expr = expr.And(p => p.RiverId == where.RiverId);
            if (!string.IsNullOrEmpty(where.RiverName))
                expr = expr.And(p => p.RiverName == where.RiverName);
            if (!string.IsNullOrEmpty(where.UserCode))
                expr = expr.And(p => p.UserCode == where.UserCode);
            if (!string.IsNullOrEmpty(where.UserName))
                expr = expr.And(p => p.UserName == where.UserName);
            // if (!string.IsNullOrEmpty(where.Coords))
            //  expr = expr.And(p => p.Coords == where.Coords);
            if (where.State>0)
                expr = expr.And(p => p.State == where.State);
            if (where.IsUrgent < 2)
                expr = expr.And(p => p.IsUrgent == where.IsUrgent);
            if (where.IsSendMessage < 2)
                expr = expr.And(p => p.IsSendMessage == where.IsSendMessage);
            if (where.IsExposure < 2)
                expr = expr.And(p => p.IsExposure == where.IsExposure);
            // if (!string.IsNullOrEmpty(where.DepartmentRemark))
            //  expr = expr.And(p => p.DepartmentRemark == where.DepartmentRemark);
            // if (!string.IsNullOrEmpty(where.DepartmentOpTime))
            //  expr = expr.And(p => p.DepartmentOpTime == where.DepartmentOpTime);
            // if (!string.IsNullOrEmpty(where.TopDepartmentRemark))
            //  expr = expr.And(p => p.TopDepartmentRemark == where.TopDepartmentRemark);
            // if (!string.IsNullOrEmpty(where.TopDepartmentOpTime))
            //  expr = expr.And(p => p.TopDepartmentOpTime == where.TopDepartmentOpTime);
            // if (!string.IsNullOrEmpty(where.FinishOpTime))
            //  expr = expr.And(p => p.FinishOpTime == where.FinishOpTime);
            // if (!string.IsNullOrEmpty(where.FinishRemark))
            //  expr = expr.And(p => p.FinishRemark == where.FinishRemark);
            // if (!string.IsNullOrEmpty(where.ReturnRemark))
            //  expr = expr.And(p => p.ReturnRemark == where.ReturnRemark);
            // if (!string.IsNullOrEmpty(where.ReturnOpTime))
            //  expr = expr.And(p => p.ReturnOpTime == where.ReturnOpTime);
            // if (!string.IsNullOrEmpty(where.IsExposure))
            //  expr = expr.And(p => p.IsExposure == where.IsExposure);
            // if (!string.IsNullOrEmpty(where.ExposureLever))
            //  expr = expr.And(p => p.ExposureLever == where.ExposureLever);
            // if (!string.IsNullOrEmpty(where.IsSendMessage))
            //  expr = expr.And(p => p.IsSendMessage == where.IsSendMessage);
            // if (!string.IsNullOrEmpty(where.IsActive))
            //  expr = expr.And(p => p.IsActive == where.IsActive);
            // if (!string.IsNullOrEmpty(where.CreatorUserName))
            //  expr = expr.And(p => p.CreatorUserName == where.CreatorUserName);
            // if (!string.IsNullOrEmpty(where.CreatorUserCode))
            //  expr = expr.And(p => p.CreatorUserCode == where.CreatorUserCode);
            //if (!string.IsNullOrEmpty(where.CreationTime))
            //    expr = expr.And(p => p.CreationTime == where.CreationTime);
            // if (!string.IsNullOrEmpty(where.LastModifierUserName))
            //  expr = expr.And(p => p.LastModifierUserName == where.LastModifierUserName);
            // if (!string.IsNullOrEmpty(where.LastModifierUserCode))
            //  expr = expr.And(p => p.LastModifierUserCode == where.LastModifierUserCode);
            // if (!string.IsNullOrEmpty(where.LastModificationTime))
            //  expr = expr.And(p => p.LastModificationTime == where.LastModificationTime);
            // if (!string.IsNullOrEmpty(where.Remark))
            //  expr = expr.And(p => p.Remark == where.Remark);
            // if (!string.IsNullOrEmpty(where.DeleteRemark))
            //  expr = expr.And(p => p.DeleteRemark == where.DeleteRemark);
            // if (!string.IsNullOrEmpty(where.IsDeleted))
            //  expr = expr.And(p => p.IsDeleted == where.IsDeleted);
            // if (!string.IsNullOrEmpty(where.DeleteUserName))
            //  expr = expr.And(p => p.DeleteUserName == where.DeleteUserName);
            // if (!string.IsNullOrEmpty(where.DeleteUserCode))
            //  expr = expr.And(p => p.DeleteUserCode == where.DeleteUserCode);
            // if (!string.IsNullOrEmpty(where.DeleteTime))
            //  expr = expr.And(p => p.DeleteTime == where.DeleteTime);
            #endregion
            var list = _riverProblemApplyRepository.Query().Where(expr).OrderByDescending(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _riverProblemApplyRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<RiverProblemApplyEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<RiverProblemApplyEntity> GetList(RiverProblemApplyEntity where)
        {
               var expr = PredicateBuilder.True<RiverProblemApplyEntity>();
             #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.Title))
              //  expr = expr.And(p => p.Title == where.Title);
              // if (!string.IsNullOrEmpty(where.Des))
              //  expr = expr.And(p => p.Des == where.Des);
              // if (!string.IsNullOrEmpty(where.ProblemType))
              //  expr = expr.And(p => p.ProblemType == where.ProblemType);
              // if (!string.IsNullOrEmpty(where.PicUrl))
              //  expr = expr.And(p => p.PicUrl == where.PicUrl);
              // if (!string.IsNullOrEmpty(where.DepartmentCode))
              //  expr = expr.And(p => p.DepartmentCode == where.DepartmentCode);
              // if (!string.IsNullOrEmpty(where.RiverId))
              //  expr = expr.And(p => p.RiverId == where.RiverId);
              // if (!string.IsNullOrEmpty(where.RiverName))
              //  expr = expr.And(p => p.RiverName == where.RiverName);
              // if (!string.IsNullOrEmpty(where.UserCode))
              //  expr = expr.And(p => p.UserCode == where.UserCode);
              // if (!string.IsNullOrEmpty(where.UserName))
              //  expr = expr.And(p => p.UserName == where.UserName);
              // if (!string.IsNullOrEmpty(where.Coords))
              //  expr = expr.And(p => p.Coords == where.Coords);
              // if (!string.IsNullOrEmpty(where.State))
              //  expr = expr.And(p => p.State == where.State);
              // if (!string.IsNullOrEmpty(where.DepartmentRemark))
              //  expr = expr.And(p => p.DepartmentRemark == where.DepartmentRemark);
              // if (!string.IsNullOrEmpty(where.DepartmentOpTime))
              //  expr = expr.And(p => p.DepartmentOpTime == where.DepartmentOpTime);
              // if (!string.IsNullOrEmpty(where.TopDepartmentRemark))
              //  expr = expr.And(p => p.TopDepartmentRemark == where.TopDepartmentRemark);
              // if (!string.IsNullOrEmpty(where.TopDepartmentOpTime))
              //  expr = expr.And(p => p.TopDepartmentOpTime == where.TopDepartmentOpTime);
              // if (!string.IsNullOrEmpty(where.FinishOpTime))
              //  expr = expr.And(p => p.FinishOpTime == where.FinishOpTime);
              // if (!string.IsNullOrEmpty(where.FinishRemark))
              //  expr = expr.And(p => p.FinishRemark == where.FinishRemark);
              // if (!string.IsNullOrEmpty(where.ReturnRemark))
              //  expr = expr.And(p => p.ReturnRemark == where.ReturnRemark);
              // if (!string.IsNullOrEmpty(where.ReturnOpTime))
              //  expr = expr.And(p => p.ReturnOpTime == where.ReturnOpTime);
              // if (!string.IsNullOrEmpty(where.IsExposure))
              //  expr = expr.And(p => p.IsExposure == where.IsExposure);
              // if (!string.IsNullOrEmpty(where.ExposureLever))
              //  expr = expr.And(p => p.ExposureLever == where.ExposureLever);
              // if (!string.IsNullOrEmpty(where.IsSendMessage))
              //  expr = expr.And(p => p.IsSendMessage == where.IsSendMessage);
              // if (!string.IsNullOrEmpty(where.IsActive))
              //  expr = expr.And(p => p.IsActive == where.IsActive);
              // if (!string.IsNullOrEmpty(where.CreatorUserName))
              //  expr = expr.And(p => p.CreatorUserName == where.CreatorUserName);
              // if (!string.IsNullOrEmpty(where.CreatorUserCode))
              //  expr = expr.And(p => p.CreatorUserCode == where.CreatorUserCode);
              // if (!string.IsNullOrEmpty(where.CreationTime))
              //  expr = expr.And(p => p.CreationTime == where.CreationTime);
              // if (!string.IsNullOrEmpty(where.LastModifierUserName))
              //  expr = expr.And(p => p.LastModifierUserName == where.LastModifierUserName);
              // if (!string.IsNullOrEmpty(where.LastModifierUserCode))
              //  expr = expr.And(p => p.LastModifierUserCode == where.LastModifierUserCode);
              // if (!string.IsNullOrEmpty(where.LastModificationTime))
              //  expr = expr.And(p => p.LastModificationTime == where.LastModificationTime);
              // if (!string.IsNullOrEmpty(where.Remark))
              //  expr = expr.And(p => p.Remark == where.Remark);
              // if (!string.IsNullOrEmpty(where.DeleteRemark))
              //  expr = expr.And(p => p.DeleteRemark == where.DeleteRemark);
              // if (!string.IsNullOrEmpty(where.IsDeleted))
              //  expr = expr.And(p => p.IsDeleted == where.IsDeleted);
              // if (!string.IsNullOrEmpty(where.DeleteUserName))
              //  expr = expr.And(p => p.DeleteUserName == where.DeleteUserName);
              // if (!string.IsNullOrEmpty(where.DeleteUserCode))
              //  expr = expr.And(p => p.DeleteUserCode == where.DeleteUserCode);
              // if (!string.IsNullOrEmpty(where.DeleteTime))
              //  expr = expr.And(p => p.DeleteTime == where.DeleteTime);
 #endregion
            var list = _riverProblemApplyRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 


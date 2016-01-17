
 /***************************************************************************
 *       功能：     HRLearningExperiences业务处理层
 *       作者：     Roy
 *       日期：     2016-01-17
 *       描述：     员工学习经历
 * *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.HRManager;
using Project.Repository.HRManager;

namespace Project.Service.HRManager
{
    public class LearningExperiencesService
    {
       
       #region 构造函数
        private readonly LearningExperiencesRepository  _learningExperiencesRepository;
            private static readonly LearningExperiencesService Instance = new LearningExperiencesService();

        public LearningExperiencesService()
        {
           this._learningExperiencesRepository =new LearningExperiencesRepository();
        }
        
         public static  LearningExperiencesService GetInstance()
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
        public System.Int32 Add(LearningExperiencesEntity entity)
        {
            return _learningExperiencesRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _learningExperiencesRepository.GetById(pkId);
            _learningExperiencesRepository.Delete(entity);
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
        public bool Delete(LearningExperiencesEntity entity)
        {
         try
            {
            _learningExperiencesRepository.Delete(entity);
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
        public bool Update(LearningExperiencesEntity entity)
        {
          try
            {
            _learningExperiencesRepository.Update(entity);
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
        public LearningExperiencesEntity GetModelByPk(System.Int32 pkId)
        {
            return _learningExperiencesRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【员工学习经历】和总【员工学习经历】数</returns>
        public System.Tuple<IList<LearningExperiencesEntity>, int> Search(LearningExperiencesEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<LearningExperiencesEntity>();
                  #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.EmployeeCode))
              //  expr = expr.And(p => p.EmployeeCode == where.EmployeeCode);
              // if (!string.IsNullOrEmpty(where.DepartmentCode))
              //  expr = expr.And(p => p.DepartmentCode == where.DepartmentCode);
              // if (!string.IsNullOrEmpty(where.ProfessionCode))
              //  expr = expr.And(p => p.ProfessionCode == where.ProfessionCode);
              // if (!string.IsNullOrEmpty(where.School))
              //  expr = expr.And(p => p.School == where.School);
              // if (!string.IsNullOrEmpty(where.Degree))
              //  expr = expr.And(p => p.Degree == where.Degree);
              // if (!string.IsNullOrEmpty(where.Education))
              //  expr = expr.And(p => p.Education == where.Education);
              // if (!string.IsNullOrEmpty(where.VerifyPersone))
              //  expr = expr.And(p => p.VerifyPersone == where.VerifyPersone);
              // if (!string.IsNullOrEmpty(where.Reward))
              //  expr = expr.And(p => p.Reward == where.Reward);
              // if (!string.IsNullOrEmpty(where.Certificate))
              //  expr = expr.And(p => p.Certificate == where.Certificate);
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
            var list = _learningExperiencesRepository.Query().Where(expr).OrderBy(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _learningExperiencesRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<LearningExperiencesEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<LearningExperiencesEntity> GetList(LearningExperiencesEntity where)
        {
               var expr = PredicateBuilder.True<LearningExperiencesEntity>();
             #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.EmployeeCode))
              //  expr = expr.And(p => p.EmployeeCode == where.EmployeeCode);
              // if (!string.IsNullOrEmpty(where.DepartmentCode))
              //  expr = expr.And(p => p.DepartmentCode == where.DepartmentCode);
              // if (!string.IsNullOrEmpty(where.ProfessionCode))
              //  expr = expr.And(p => p.ProfessionCode == where.ProfessionCode);
              // if (!string.IsNullOrEmpty(where.School))
              //  expr = expr.And(p => p.School == where.School);
              // if (!string.IsNullOrEmpty(where.Degree))
              //  expr = expr.And(p => p.Degree == where.Degree);
              // if (!string.IsNullOrEmpty(where.Education))
              //  expr = expr.And(p => p.Education == where.Education);
              // if (!string.IsNullOrEmpty(where.VerifyPersone))
              //  expr = expr.And(p => p.VerifyPersone == where.VerifyPersone);
              // if (!string.IsNullOrEmpty(where.Reward))
              //  expr = expr.And(p => p.Reward == where.Reward);
              // if (!string.IsNullOrEmpty(where.Certificate))
              //  expr = expr.And(p => p.Certificate == where.Certificate);
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
            var list = _learningExperiencesRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 


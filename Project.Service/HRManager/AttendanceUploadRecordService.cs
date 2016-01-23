
 /***************************************************************************
 *       功能：     HRAttendanceUploadRecord业务处理层
 *       作者：     李伟伟
 *       日期：     2016/1/23
 *       描述：     人事考勤上传记录
 * *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.HRManager;
using Project.Repository.HRManager;

namespace Project.Service.HRManager
{
    public class AttendanceUploadRecordService
    {
       
       #region 构造函数
        private readonly AttendanceUploadRecordRepository  _attendanceUploadRecordRepository;
            private static readonly AttendanceUploadRecordService Instance = new AttendanceUploadRecordService();

        public AttendanceUploadRecordService()
        {
           this._attendanceUploadRecordRepository =new AttendanceUploadRecordRepository();
        }
        
         public static  AttendanceUploadRecordService GetInstance()
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
        public System.Int32 Add(AttendanceUploadRecordEntity entity)
        {
            return _attendanceUploadRecordRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _attendanceUploadRecordRepository.GetById(pkId);
            _attendanceUploadRecordRepository.Delete(entity);
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
        public bool Delete(AttendanceUploadRecordEntity entity)
        {
         try
            {
            _attendanceUploadRecordRepository.Delete(entity);
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
        public bool Update(AttendanceUploadRecordEntity entity)
        {
          try
            {
            _attendanceUploadRecordRepository.Update(entity);
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
        public AttendanceUploadRecordEntity GetModelByPk(System.Int32 pkId)
        {
            return _attendanceUploadRecordRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【人事考勤上传记录】和总【人事考勤上传记录】数</returns>
        public System.Tuple<IList<AttendanceUploadRecordEntity>, int> Search(AttendanceUploadRecordEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<AttendanceUploadRecordEntity>();
                  #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.DepartmentCode))
              //  expr = expr.And(p => p.DepartmentCode == where.DepartmentCode);
              // if (!string.IsNullOrEmpty(where.Date))
              //  expr = expr.And(p => p.Date == where.Date);
              // if (!string.IsNullOrEmpty(where.Remark))
              //  expr = expr.And(p => p.Remark == where.Remark);
              // if (!string.IsNullOrEmpty(where.CreatorUserCode))
              //  expr = expr.And(p => p.CreatorUserCode == where.CreatorUserCode);
              // if (!string.IsNullOrEmpty(where.CreatorUserName))
              //  expr = expr.And(p => p.CreatorUserName == where.CreatorUserName);
              // if (!string.IsNullOrEmpty(where.CreateTime))
              //  expr = expr.And(p => p.CreateTime == where.CreateTime);
              // if (!string.IsNullOrEmpty(where.FileUrl))
              //  expr = expr.And(p => p.FileUrl == where.FileUrl);
              // if (!string.IsNullOrEmpty(where.IsDelete))
              //  expr = expr.And(p => p.IsDelete == where.IsDelete);
 #endregion
            var list = _attendanceUploadRecordRepository.Query().Where(expr).OrderBy(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _attendanceUploadRecordRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<AttendanceUploadRecordEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<AttendanceUploadRecordEntity> GetList(AttendanceUploadRecordEntity where)
        {
               var expr = PredicateBuilder.True<AttendanceUploadRecordEntity>();
             #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.DepartmentCode))
              //  expr = expr.And(p => p.DepartmentCode == where.DepartmentCode);
              // if (!string.IsNullOrEmpty(where.Date))
              //  expr = expr.And(p => p.Date == where.Date);
              // if (!string.IsNullOrEmpty(where.Remark))
              //  expr = expr.And(p => p.Remark == where.Remark);
              // if (!string.IsNullOrEmpty(where.CreatorUserCode))
              //  expr = expr.And(p => p.CreatorUserCode == where.CreatorUserCode);
              // if (!string.IsNullOrEmpty(where.CreatorUserName))
              //  expr = expr.And(p => p.CreatorUserName == where.CreatorUserName);
              // if (!string.IsNullOrEmpty(where.CreateTime))
              //  expr = expr.And(p => p.CreateTime == where.CreateTime);
              // if (!string.IsNullOrEmpty(where.FileUrl))
              //  expr = expr.And(p => p.FileUrl == where.FileUrl);
              // if (!string.IsNullOrEmpty(where.IsDelete))
              //  expr = expr.And(p => p.IsDelete == where.IsDelete);
 #endregion
            var list = _attendanceUploadRecordRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 


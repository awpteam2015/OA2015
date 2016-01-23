
 /***************************************************************************
 *       功能：     HRAttendance业务处理层
 *       作者：     李伟伟
 *       日期：     2016/1/23
 *       描述：     人事考勤记录
 * *************************************************************************/
using System.Linq;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.DataNhibernate.Helpers;
using Project.Model.HRManager;
using Project.Repository.HRManager;

namespace Project.Service.HRManager
{
    public class AttendanceService
    {
       
       #region 构造函数
        private readonly AttendanceRepository  _attendanceRepository;
            private static readonly AttendanceService Instance = new AttendanceService();

        public AttendanceService()
        {
           this._attendanceRepository =new AttendanceRepository();
        }
        
         public static  AttendanceService GetInstance()
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
        public System.Int32 Add(AttendanceEntity entity)
        {
            return _attendanceRepository.Save(entity);
        }
        
        
         /// <summary>
        /// 删除
        /// </summary>
        /// <param name="pkId"></param>
        public bool DeleteByPkId(System.Int32 pkId)
        {
         try
            {
            var entity= _attendanceRepository.GetById(pkId);
            _attendanceRepository.Delete(entity);
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
        public bool Delete(AttendanceEntity entity)
        {
         try
            {
            _attendanceRepository.Delete(entity);
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
        public bool Update(AttendanceEntity entity)
        {
          try
            {
            _attendanceRepository.Update(entity);
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
        public AttendanceEntity GetModelByPk(System.Int32 pkId)
        {
            return _attendanceRepository.GetById(pkId);
        }


        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <param name="skipResults">开始</param>
        /// <param name="maxResults">结束</param>
        /// <returns>获取当前页【人事考勤记录】和总【人事考勤记录】数</returns>
        public System.Tuple<IList<AttendanceEntity>, int> Search(AttendanceEntity where, int skipResults, int maxResults)
        {
                var expr = PredicateBuilder.True<AttendanceEntity>();
                  #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.AttendanceUploadRecordId))
              //  expr = expr.And(p => p.AttendanceUploadRecordId == where.AttendanceUploadRecordId);
              // if (!string.IsNullOrEmpty(where.EmployeeCode))
              //  expr = expr.And(p => p.EmployeeCode == where.EmployeeCode);
              // if (!string.IsNullOrEmpty(where.DepartmentCode))
              //  expr = expr.And(p => p.DepartmentCode == where.DepartmentCode);
              // if (!string.IsNullOrEmpty(where.DepartmentName))
              //  expr = expr.And(p => p.DepartmentName == where.DepartmentName);
              // if (!string.IsNullOrEmpty(where.State))
              //  expr = expr.And(p => p.State == where.State);
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
              // if (!string.IsNullOrEmpty(where.IsDelete))
              //  expr = expr.And(p => p.IsDelete == where.IsDelete);
 #endregion
            var list = _attendanceRepository.Query().Where(expr).OrderBy(p => p.PkId).Skip(skipResults).Take(maxResults).ToList();
            var count = _attendanceRepository.Query().Where(expr).Count();
            return new System.Tuple<IList<AttendanceEntity>, int>(list, count);
        }

        /// <summary>
        /// 取列表
        /// </summary>
        /// <param name="entity">条件实体</param>
        /// <returns>返回列表</returns>
        public IList<AttendanceEntity> GetList(AttendanceEntity where)
        {
               var expr = PredicateBuilder.True<AttendanceEntity>();
             #region
              // if (!string.IsNullOrEmpty(where.PkId))
              //  expr = expr.And(p => p.PkId == where.PkId);
              // if (!string.IsNullOrEmpty(where.AttendanceUploadRecordId))
              //  expr = expr.And(p => p.AttendanceUploadRecordId == where.AttendanceUploadRecordId);
              // if (!string.IsNullOrEmpty(where.EmployeeCode))
              //  expr = expr.And(p => p.EmployeeCode == where.EmployeeCode);
              // if (!string.IsNullOrEmpty(where.DepartmentCode))
              //  expr = expr.And(p => p.DepartmentCode == where.DepartmentCode);
              // if (!string.IsNullOrEmpty(where.DepartmentName))
              //  expr = expr.And(p => p.DepartmentName == where.DepartmentName);
              // if (!string.IsNullOrEmpty(where.State))
              //  expr = expr.And(p => p.State == where.State);
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
              // if (!string.IsNullOrEmpty(where.IsDelete))
              //  expr = expr.And(p => p.IsDelete == where.IsDelete);
 #endregion
            var list = _attendanceRepository.Query().Where(expr).OrderBy(p => p.PkId).ToList();
            return list;
        }
        #endregion


        #region 新增方法
        
        #endregion
    }
}

    
 



 /***************************************************************************
 *       功能：     HRAttendance映射类
 *       作者：     ROY
 *       日期：     2016-01-09
 *       描述：     人事考勤记录
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.HRManager;
using Project.Model.PermissionManager;

namespace  Project.Map.HRManager
{
    public class AttendanceMap : BaseMap<AttendanceEntity,int>
    {
        public AttendanceMap():base("HR_Attendance")
        {
            this.MapPkidDefault<AttendanceEntity,int>();
 
            Map(p => p.EmployeeCode);    
            Map(p => p.DepartmentCode);    
            Map(p => p.State);    
            Map(p => p.Date);    
            Map(p => p.Remark);    
            Map(p => p.CreatorUserCode);    
            Map(p => p.CreatorUserName);    
            Map(p => p.CreateTime);    
        }
    }
}

    
 


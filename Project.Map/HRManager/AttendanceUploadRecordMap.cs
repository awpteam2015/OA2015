
 /***************************************************************************
 *       功能：     HRAttendanceUploadRecord映射类
 *       作者：     李伟伟
 *       日期：     2016/1/23
 *       描述：     人事考勤上传记录
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.HRManager;
using Project.Model.PermissionManager;

namespace  Project.Map.HRManager
{
    public class AttendanceUploadRecordMap : BaseMap<AttendanceUploadRecordEntity,int>
    {
        public AttendanceUploadRecordMap():base("HR_AttendanceUploadRecord")
        {
            this.MapPkidDefault<AttendanceUploadRecordEntity,int>();
 
            Map(p => p.DepartmentCode);    
            Map(p => p.Date);    
            Map(p => p.Remark);    
            Map(p => p.CreatorUserCode);     
            Map(p => p.CreationTime);    
            Map(p => p.FileUrl);    
            Map(p => p.IsDelete);    
        }
    }
}

    
 


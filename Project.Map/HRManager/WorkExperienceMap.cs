
 /***************************************************************************
 *       功能：     HRWorkExperience映射类
 *       作者：     ROY
 *       日期：     2016-01-09
 *       描述：     员工工作经历记录
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.HRManager;
using Project.Model.PermissionManager;

namespace  Project.Map.HRManager
{
    public class WorkExperienceMap : BaseMap<WorkExperienceEntity,int>
    {
        public WorkExperienceMap():base("HR_WorkExperience")
        {
            this.MapPkidDefault<WorkExperienceEntity,int>();
 
            Map(p => p.EmployeeCode);    
            Map(p => p.DepartmentCode);    
            Map(p => p.WorkCompany);    
            Map(p => p.Duties);    
            Map(p => p.BeginDate);    
            Map(p => p.EndDate);    
            Map(p => p.WorkContent);    
            Map(p => p.LeaveReason);    
            Map(p => p.Remark);    
            Map(p => p.CreatorUserCode);    
            Map(p => p.CreatorUserName);    
            Map(p => p.CreateTime);    
            Map(p => p.LastModificationTime);    
        }
    }
}

    
 


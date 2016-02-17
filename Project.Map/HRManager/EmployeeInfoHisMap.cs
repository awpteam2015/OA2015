
 /***************************************************************************
 *       功能：     HREmployeeInfoHis映射类
 *       作者：     Roy
 *       日期：     2016-02-16
 *       描述：     通过FSate字段进行过滤是还是历史记录
   人员基础信息，如需要增加多字段请使用扩展表
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.HRManager;
using Project.Model.PermissionManager;

namespace  Project.Map.HRManager
{
    public class EmployeeInfoHisMap : BaseMap<EmployeeInfoHisEntity,int>
    {
        public EmployeeInfoHisMap():base("HR_EmployeeInfoHis")
        {
            this.MapPkidDefault<EmployeeInfoHisEntity,int>();
 
            Map(p => p.EmployeeID);    
            Map(p => p.EmployeeCode);    
            Map(p => p.EmployeeName);    
            Map(p => p.DepartmentCode);    
            Map(p => p.JobName);    
            Map(p => p.PayCode);    
            Map(p => p.Sex);    
            Map(p => p.CertNo);    
            Map(p => p.Birthday);    
            Map(p => p.TechnicalTitleName);    
            Map(p => p.TechnicalTitle);    
            Map(p => p.DutiesName);    
            Map(p => p.Duties);    
            Map(p => p.WorkingYears);    
            Map(p => p.WorkState);    
            Map(p => p.EmployeeType);    
            Map(p => p.EmployeeTypeName);    
            Map(p => p.HomeAddress);    
            Map(p => p.MobileNO);    
            Map(p => p.ImageUrl);    
            Map(p => p.Sort);    
            Map(p => p.State);    
            Map(p => p.Remark);    
            Map(p => p.CreatorUserCode);    
            Map(p => p.CreatorUserName);    
            Map(p => p.CreateTime);    
            Map(p => p.LastModificationTime);    
            Map(p => p.WorkStateName);
            Map(p => p.IsDeleted);
        }
    }
}

    
 


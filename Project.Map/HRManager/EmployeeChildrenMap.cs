
 /***************************************************************************
 *       功能：     HREmployeeChildren映射类
 *       作者：     ROY
 *       日期：     2016-01-09
 *       描述：     职工子女登记
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.HRManager;
using Project.Model.PermissionManager;

namespace  Project.Map.HRManager
{
    public class EmployeeChildrenMap : BaseMap<EmployeeChildrenEntity,int>
    {
        public EmployeeChildrenMap():base("HR_EmployeeChildren")
        {
            this.MapPkidDefault<EmployeeChildrenEntity,int>();
 
            Map(p => p.EmployeeCode);    
            Map(p => p.ChildrenName);    
            Map(p => p.Sex);    
            Map(p => p.Relation);    
            Map(p => p.Certificate);    
            Map(p => p.JoinDate);    
            Map(p => p.Hospital);    
            Map(p => p.Remark);    
            Map(p => p.CreatorUserCode);    
            Map(p => p.CreatorUserName);    
            Map(p => p.CreateTime);    
            Map(p => p.LastModificationTime);    
        }
    }
}

    
 


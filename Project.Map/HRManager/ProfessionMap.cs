
 /***************************************************************************
 *       功能：     HRProfession映射类
 *       作者：     Roy
 *       日期：     2016-02-02
 *       描述：     职业资格
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.HRManager;
using Project.Model.PermissionManager;

namespace  Project.Map.HRManager
{
    public class ProfessionMap : BaseMap<ProfessionEntity,int>
    {
        public ProfessionMap():base("HR_Profession")
        {
            this.MapPkidDefault<ProfessionEntity,int>();
 
            Map(p => p.EmployeeID);    
            Map(p => p.EmployeeCode);    
            Map(p => p.DepartmentCode);    
            Map(p => p.Title);    
            Map(p => p.TypeName);    
            Map(p => p.RangeName);    
            Map(p => p.GetDate);    
            Map(p => p.CerNo);    
            Map(p => p.CreatorUserCode);    
            Map(p => p.CreatorUserName);    
            Map(p => p.CreationTime);    
            Map(p => p.LastModificationTime);    
            Map(p => p.LastModifierUserCode);    
        }
    }
}

    
 


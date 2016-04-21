
 /***************************************************************************
 *       功能：     HREmployeeFile映射类
 *       作者：     Roy
 *       日期：     2016/4/21
 *       描述：     
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.HRManager;
using Project.Model.PermissionManager;

namespace  Project.Map.HRManager
{
    public class EmployeeFileMap : BaseMap<EmployeeFileEntity,int>
    {
        public EmployeeFileMap():base("HR_EmployeeFile")
        {
            this.MapPkidDefault<EmployeeFileEntity,int>();
 
            Map(p => p.EmployeeID);    
            Map(p => p.FName);    
            Map(p => p.FileUrl);
            Map(p => p.FOrgName);
            Map(p => p.CreatorUserCode);    
            Map(p => p.CreattorUserName);    
            Map(p => p.CreationTime);    
            Map(p => p.LastModificationTime);    
            Map(p => p.LastModifierUserCode);    
        }
    }
}

    
 



 /***************************************************************************
 *       功能：     HRYGWage映射类
 *       作者：     Roy
 *       日期：     2016-05-08
 *       描述：     
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.HRManager;
using Project.Model.PermissionManager;

namespace  Project.Map.HRManager
{
    public class YGWageMap : BaseMap<YGWageEntity,int>
    {
        public YGWageMap():base("HR_YGWage")
        {
            this.MapPkidDefault<YGWageEntity,int>();
 
            Map(p => p.EmployeeID);    
            Map(p => p.GWGZ);    
            Map(p => p.XZGZ);    
            Map(p => p.CreatorUserCode);    
            Map(p => p.CreattorUserName);    
            Map(p => p.CreationTime);    
            Map(p => p.LastModificationTime);    
            Map(p => p.LastModifierUserCode);    
        }
    }
}

    
 


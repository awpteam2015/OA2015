
 /***************************************************************************
 *       功能：     HRContinEducation映射类
 *       作者：     Roy
 *       日期：     2016/4/16
 *       描述：     
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.HRManager;
using Project.Model.PermissionManager;

namespace  Project.Map.HRManager
{
    public class ContinEducationMap : BaseMap<ContinEducationEntity,int>
    {
        public ContinEducationMap():base("HR_ContinEducation")
        {
            this.MapPkidDefault<ContinEducationEntity,int>();
 
            Map(p => p.EmployeeID);    
            Map(p => p.EmployeeCode);    
            Map(p => p.DepartmentCode);    
            Map(p => p.CreditType);    
            Map(p => p.CreditTypeName);    
            Map(p => p.Score);    
            Map(p => p.GetTime);    
            Map(p => p.CreatorUserCode);    
            Map(p => p.CreattorUserName);    
            Map(p => p.CreationTime);    
            Map(p => p.LastModificationTime);    
            Map(p => p.LastModifierUserCode);    
        }
    }
}

    
 


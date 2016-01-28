
 /***************************************************************************
 *       功能：     HRTechnical映射类
 *       作者：     Roy
 *       日期：     2016-01-28
 *       描述：     职称等级
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.HRManager;
using Project.Model.PermissionManager;

namespace  Project.Map.HRManager
{
    public class TechnicalMap : BaseMap<TechnicalEntity,int>
    {
        public TechnicalMap():base("HR_Technical")
        {
            this.MapPkidDefault<TechnicalEntity,int>();
 
            Map(p => p.EmployeeCode);    
            Map(p => p.DepartmentCode);    
            Map(p => p.Title);    
            Map(p => p.LevNum);    
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

    
 


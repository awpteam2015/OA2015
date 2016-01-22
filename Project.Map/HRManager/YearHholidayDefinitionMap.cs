
 /***************************************************************************
 *       功能：     HRYearHholidayDefinition映射类
 *       作者：     Roy
 *       日期：     2016-01-22
 *       描述：     年休存休月定义
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.HRManager;
using Project.Model.PermissionManager;

namespace  Project.Map.HRManager
{
    public class YearHholidayDefinitionMap : BaseMap<YearHholidayDefinitionEntity,int>
    {
        public YearHholidayDefinitionMap():base("HR_YearHholidayDefinition")
        {
            this.MapPkidDefault<YearHholidayDefinitionEntity,int>();
 
            Map(p => p.YearsNum);    
            Map(p => p.BeginMonth);    
            Map(p => p.EndMonth);    
            Map(p => p.CreatorUserCode);    
            Map(p => p.CreatorUserName);    
            Map(p => p.CreateTime);    
            Map(p => p.LastModificationTime);    
        }
    }
}

    
 


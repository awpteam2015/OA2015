
 /***************************************************************************
 *       功能：     HRSanction映射类
 *       作者：     ROY
 *       日期：     2016-01-09
 *       描述：     职工奖罚及部门等奖罚
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.HRManager;
using Project.Model.PermissionManager;

namespace  Project.Map.HRManager
{
    public class SanctionMap : BaseMap<SanctionEntity,int>
    {
        public SanctionMap():base("HR_Sanction")
        {
            this.MapPkidDefault<SanctionEntity,int>();
 
            Map(p => p.SanctionType);    
            Map(p => p.SanctionObj);    
            Map(p => p.SanctionTitle);    
            Map(p => p.SanctionMoney);    
            Map(p => p.SanctionDate);    
            Map(p => p.Remark);    
            Map(p => p.CreatorUserCode);    
            Map(p => p.CreatorUserName);    
            Map(p => p.CreateTime);    
        }
    }
}

    
 


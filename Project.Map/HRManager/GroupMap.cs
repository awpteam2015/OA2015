
 /***************************************************************************
 *       功能：     HRGroup映射类
 *       作者：     ROY
 *       日期：     2016-01-09
 *       描述：     用于管理组
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.HRManager;
using Project.Model.PermissionManager;

namespace  Project.Map.HRManager
{
    public class GroupMap : BaseMap<GroupEntity,int>
    {
        public GroupMap():base("HR_Group")
        {
            this.MapPkidDefault<GroupEntity,int>();
 
            Map(p => p.GroupCode);    
            Map(p => p.GroupName);    
            Map(p => p.Sort);    
            Map(p => p.Remark);    
            Map(p => p.CreatorUserCode);    
            Map(p => p.CreatorUserName);    
            Map(p => p.CreateTime);    
            Map(p => p.LastModificationTime);    
            Map(p => p.IsDeleted);    
        }
    }
}

    
 


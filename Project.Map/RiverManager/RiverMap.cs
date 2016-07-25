
 /***************************************************************************
 *       功能：     RMRiver映射类
 *       作者：     李伟伟
 *       日期：     2016/7/23
 *       描述：     河道信息
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.PermissionManager;
using Project.Model.RiverManager;

namespace  Project.Map.RiverManager
{
    public class RiverMap : BaseMap<RiverEntity,int>
    {
        public RiverMap():base("RM_River")
        {
            this.MapPkidDefault<RiverEntity,int>();
 
            Map(p => p.RiverName);    
            Map(p => p.RiverRank);    
            Map(p => p.RiverArea);    
            Map(p => p.RiverLength);    
            Map(p => p.RiverCrossArea);    
            Map(p => p.Coords);    
            Map(p => p.IsActive);    
            Map(p => p.CreatorUserCode);    
            Map(p => p.CreationTime);    
            Map(p => p.LastModifierUserCode);    
            Map(p => p.LastModificationTime);    
            Map(p => p.Remark);

            Map(p => p.DepartmentCode);
            Map(p => p.DepartmentName);


            HasMany(p => p.RiverOwerList)
.AsSet()
.LazyLoad()
.Cascade.All().Inverse()
.KeyColumn("RiverId");
        }
    }
}

    
 


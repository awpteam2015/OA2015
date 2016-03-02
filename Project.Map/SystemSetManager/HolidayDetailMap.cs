
 /***************************************************************************
 *       功能：     SysHolidayDetail映射类
 *       作者：     Roy
 *       日期：     2016-01-20
 *       描述：     节假日管理
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.SystemSetManager;
using Project.Model.PermissionManager;

namespace  Project.Map.SystemSetManager
{
    public class HolidayDetailMap : BaseMap<HolidayDetailEntity,int>
    {
        public HolidayDetailMap():base("Sys_HolidayDetail")
        {
            this.MapPkidDefault<HolidayDetailEntity,int>();
 
            Map(p => p.HolidayName);
            Map(p => p.HolidayDate);
            Map(p => p.HolidayDateType);   
            Map(p => p.Remark);    
            Map(p => p.CreatorUserCode);    
            Map(p => p.CreatorUserName);    
            Map(p => p.CreateTime);    
            Map(p => p.LastModificationTime);    
        }
    }
}

    
 


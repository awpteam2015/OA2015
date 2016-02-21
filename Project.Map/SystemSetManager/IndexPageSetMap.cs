
 /***************************************************************************
 *       功能：     SMIndexPageSet映射类
 *       作者：     Roy
 *       日期：     2016-02-21
 *       描述：     
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.SystemSetManager;
using Project.Model.PermissionManager;

namespace  Project.Map.SystemSetManager
{
    public class IndexPageSetMap : BaseMap<IndexPageSetEntity,int>
    {
        public IndexPageSetMap():base("SM_IndexPageSet")
        {
            this.MapPkidDefault<IndexPageSetEntity,int>();
 
            Map(p => p.Des);    
        }
    }
}

    
 



 /***************************************************************************
 *       功能：     SMDictionary映射类
 *       作者：     ROY
 *       日期：     2016-01-10
 *       描述：     
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.HRManager;
using Project.Model.PermissionManager;

namespace  Project.Map.HRManager
{
    public class DictionaryMap : BaseMap<DictionaryEntity,int>
    {
        public DictionaryMap():base("SM_Dictionary")
        {
            this.MapPkidDefault<DictionaryEntity,int>();
 
            Map(p => p.KeyCode);    
            Map(p => p.ParentKeyCode);    
            Map(p => p.KeyName);    
            Map(p => p.KeyValue);    
        }
    }
}

    
 


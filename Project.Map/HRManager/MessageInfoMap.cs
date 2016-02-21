
/***************************************************************************
*       功能：     SysMessageInfo映射类
*       作者：     Roy
*       日期：     2016-02-21
*       描述：     站内短信
* *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.HRManager;
using Project.Model.PermissionManager;

namespace Project.Map.HRManager
{
    public class MessageInfoMap : BaseMap<MessageInfoEntity, int>
    {
        public MessageInfoMap() : base("Sys_MessageInfo")
        {
            this.MapPkidDefault<MessageInfoEntity, int>();

            Map(p => p.MesTitle);
            Map(p => p.MesContent);
            Map(p => p.ReceiveUserCode);
            Map(p => p.IsAll);
            Map(p => p.ReadUser);
            Map(p => p.CreatorUserCode);
            Map(p => p.CreatorUserName);
            Map(p => p.CreationTime);
            Map(p => p.LastModificationTime);
            Map(p => p.LastModifierUserCode);
            Map(p => p.DeleterUserCode);
            Map(p => p.DeletionTime);
        }
    }
}





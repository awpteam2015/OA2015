
/***************************************************************************
*       功能：     HRGoAbroad映射类
*       作者：     Roy
*       日期：     2016-01-19
*       描述：     记录人员出国情况
* *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.HRManager;
using Project.Model.PermissionManager;

namespace Project.Map.HRManager
{
    public class GoAbroadMap : BaseMap<GoAbroadEntity, int>
    {
        public GoAbroadMap()
            : base("HR_GoAbroad")
        {
            this.MapPkidDefault<GoAbroadEntity, int>();

            Map(p => p.EmployeeCode);
            Map(p => p.EmployeeName);
            Map(p => p.DepartmentCode);
            Map(p => p.DepartmentName);
            Map(p => p.Country);
            Map(p => p.BeginDate);
            Map(p => p.EndDate);
            Map(p => p.DaySum);
            Map(p => p.Reason);
            Map(p => p.Remark);
            Map(p => p.CreatorUserCode);
            Map(p => p.CreatorUserName);
            Map(p => p.CreateTime);
            Map(p => p.LastModificationTime);
        }
    }
}





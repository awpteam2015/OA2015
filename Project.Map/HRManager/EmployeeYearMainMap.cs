
/***************************************************************************
*       功能：     HREmployeeYearMain映射类
*       作者：     Roy
*       日期：     2016-01-22
*       描述：     员工年休管理
* *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.HRManager;
using Project.Model.PermissionManager;

namespace Project.Map.HRManager
{
    public class EmployeeYearMainMap : BaseMap<EmployeeYearMainEntity, int>
    {
        public EmployeeYearMainMap() : base("HR_EmployeeYearMain")
        {
            this.MapPkidDefault<EmployeeYearMainEntity, int>();

            Map(p => p.DepartmentCode);

            Map(p => p.EmployeeCode);
            Map(p => p.EmployeeName);
            Map(p => p.LeftCount);
            Map(p => p.Remark);
            Map(p => p.CreatorUserCode);
            Map(p => p.CreatorUserName);
            Map(p => p.CreateTime);
            Map(p => p.LastModificationTime);
            Map(p => p.IsDeleted);

            References(p => p.DepartmentEntity)
                .Not.Insert()
                .Not.Update()
                .PropertyRef("DepartmentCode")
                .Column("DepartmentCode");
        }
    }
}





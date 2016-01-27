
/***************************************************************************
*       功能：     HREmployeeYearDetail映射类
*       作者：     Roy
*       日期：     2016-01-22
*       描述：     年休登记
* *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.HRManager;
using Project.Model.PermissionManager;

namespace Project.Map.HRManager
{
    public class EmployeeYearDetailMap : BaseMap<EmployeeYearDetailEntity, int>
    {
        public EmployeeYearDetailMap() : base("HR_EmployeeYearDetail")
        {
            this.MapPkidDefault<EmployeeYearDetailEntity, int>();

            Map(p => p.DepartmentCode);
            Map(p => p.EmployeeCode);
            Map(p => p.EmployeeName);
            Map(p => p.UseType);
            Map(p => p.BeginDate);
            Map(p => p.EndDate);
            Map(p => p.UseCount);
            Map(p => p.BeforeUseCount);
            Map(p => p.LeftCount);
            Map(p => p.Remark);
            Map(p => p.CreatorUserCode);
            Map(p => p.CreatorUserName);
            Map(p => p.CreationTime);
            Map(p => p.LastModificationTime);
            Map(p => p.LastModifierUserCode);
            Map(p => p.IsDeleted);

            References(p => p.DepartmentEntity)
                .Not.Insert()
                .Not.Update()
                .PropertyRef("DepartmentCode")
                .Column("DepartmentCode");
        }
    }
}





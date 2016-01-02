
/***************************************************************************
*       功能：     PMDepartment映射类
*       作者：     李伟伟
*       日期：     2015/12/22
*       描述：     部门基础信息表
* *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.PermissionManager;

namespace Project.Map.PermissionManager
{
    public class DepartmentMap : BaseMap<DepartmentEntity, int>
    {
        public DepartmentMap(): base("PM_Department")
        {
            this.MapPkidDefault<DepartmentEntity, int>();

            Map(p => p.DepartmentCode);
            Map(p => p.DepartmentName);
            Map(p => p.ParentDepartmentCode);
            Map(p => p.Remark);

       //     HasMany(p => p.children)
       //.AsSet()
       //.LazyLoad()
       //.Cascade.All().Inverse()
       //.PropertyRef("DepartmentCode")
       //.KeyColumn("ParentDepartmentCode");
        }
    }
}





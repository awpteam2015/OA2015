
using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.PermissionManager;

namespace Project.Map.PermissionManager
{
    public class UserInfoMap : BaseMap<UserInfoEntity, int>
    {
        public UserInfoMap()
            : base("PM_UserInfo")
        {
            this.MapPkidDefault<UserInfoEntity, int>();

            Map(p => p.UserCode);
            Map(p => p.Password);
            Map(p => p.UserName);
            Map(p => p.Email);
            Map(p => p.Mobile);
            Map(p => p.IsDeleted);

            HasMany(p => p.UserDepartmentList)
    .AsSet()
    .LazyLoad()
    .Cascade.All().Inverse()
    .PropertyRef("UserCode")
    .KeyColumn("UserCode");

            HasMany(p => p.UserRoleList)
  .AsSet()
  .LazyLoad()
  .Cascade.All().Inverse()
  .PropertyRef("UserCode")
  .KeyColumn("UserCode");


        }
    }
}

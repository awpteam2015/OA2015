
/***************************************************************************
*       功能：     HRTechnical映射类
*       作者：     Roy
*       日期：     2016-01-28
*       描述：     职称等级
* *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.HRManager;
using Project.Model.PermissionManager;

namespace Project.Map.HRManager
{
    public class YearAssessmentMap : BaseMap<YearAssessmentEntity, int>
    {
        public YearAssessmentMap() : base("HR_YearAssessment")
        {
            this.MapPkidDefault<YearAssessmentEntity, int>();
            Map(p => p.EmployeeID);
            //Map(p => p.EmployeeCode);
            Map(p => p.KHYear);
            Map(p => p.KHComment);
            Map(p => p.CreatorUserCode);
            Map(p => p.CreatorUserName);
            Map(p => p.CreationTime);
            Map(p => p.LastModificationTime);
            Map(p => p.LastModifierUserCode);
        }
    }
}





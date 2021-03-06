﻿
/***************************************************************************
*       功能：     HRLearningExperiences映射类
*       作者：     Roy
*       日期：     2016-01-17
*       描述：     员工学习经历
* *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.HRManager;
using Project.Model.PermissionManager;

namespace Project.Map.HRManager
{
    public class LearningExperiencesMap : BaseMap<LearningExperiencesEntity, int>
    {
        public LearningExperiencesMap()
            : base("HR_LearningExperiences")
        {
            this.MapPkidDefault<LearningExperiencesEntity, int>();

            Map(p => p.EmployeeID);
            Map(p => p.DepartmentCode);
            Map(p => p.ProfessionCode);
            Map(p => p.School);
            Map(p => p.Degree);
            Map(p => p.Education);
            Map(p => p.SchoolYear);
            Map(p => p.CertNumber);
            Map(p => p.BeginDate);
            Map(p => p.EndDate);
            Map(p => p.Remark);
            Map(p => p.CreatorUserCode);
            Map(p => p.CreatorUserName);
            Map(p => p.CreationTime);
            Map(p => p.LastModificationTime);
        }
    }
}





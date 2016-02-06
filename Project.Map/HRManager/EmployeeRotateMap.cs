
 /***************************************************************************
 *       功能：     HREmployeeRotate映射类
 *       作者：     ROY
 *       日期：     2016-01-09
 *       描述：     用于 轮转人员计划
   (轮转人员的设置放入组管理，新建一个组进行管理)
 * *************************************************************************/

using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.HRManager;
using Project.Model.PermissionManager;

namespace  Project.Map.HRManager
{
    public class EmployeeRotateMap : BaseMap<EmployeeRotateEntity,int>
    {
        public EmployeeRotateMap():base("HR_EmployeeRotate")
        {
            this.MapPkidDefault<EmployeeRotateEntity,int>();
 
            Map(p => p.EmployeeCode);    
            Map(p => p.DepartmentCode);    
            Map(p => p.RotatEDetpCode);    
            Map(p => p.BeginDate);    
            Map(p => p.EenData);    
            Map(p => p.Evaluate);    
            Map(p => p.EvaluatePersone);    
            Map(p => p.Remark);    
            Map(p => p.CreatorUserCode);    
            Map(p => p.CreatorUserName);    
            Map(p => p.CreateTime);    
            Map(p => p.LastModificationTime);    
        }
    }
}

    
 



 /***************************************************************************
 *       功能：     HRContract映射类
 *       作者：     李伟伟
 *       日期：     2016/1/23
 *       描述：     用于记录合同（合同内工资类型等都过滤暂时不考虑）
 * *************************************************************************/

using NHibernate.Type;
using Project.Infrastructure.FrameworkCore.DataNhibernate.EntityMappings;
using Project.Model.Enum.HRManager;
using Project.Model.HRManager;
using Project.Model.PermissionManager;

namespace  Project.Map.HRManager
{
    public class ContractMap : BaseMap<ContractEntity,int>
    {
        public ContractMap():base("HR_Contract")
        {
            this.MapPkidDefault<ContractEntity,int>();
 
            Map(p => p.EmployeeCode);    
            Map(p => p.DepartmentCode);    
            Map(p => p.BeginDate);    
            Map(p => p.EndDate);    
            Map(p => p.Remark);    
            Map(p => p.CreatorUserCode);    
            Map(p => p.CreatorUserName);    
            Map(p => p.CreateTime);    
            Map(p => p.LastModificationTime);    
            Map(p => p.IsDelete);
            Map(p => p.State).CustomType<EnumStringType<ContractStateEnum>>();
            Map(p => p.IsActive).CustomType<EnumStringType<ContractIsActiveEnum>>();    
        }
    }
}

    
 


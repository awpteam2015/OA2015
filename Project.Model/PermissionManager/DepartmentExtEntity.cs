

/***************************************************************************
*       功能：     PMDepartment实体类
*       作者：     李伟伟
*       日期：     2015/12/22
*       描述：     部门基础信息表
*--------------------------------------修改记录-----------------------------
*1. Roy 加入类型机构或是科室
* *************************************************************************/
using System;
using System.Collections.Generic;
using Project.Infrastructure.FrameworkCore.Domain.Entities;
using Project.Infrastructure.FrameworkCore.Domain.Entities.Component;
using Project.Infrastructure.FrameworkCore.WebMvc.Models.ExtendUi;
using Project.Model.Enum;

namespace Project.Model.PermissionManager
{
    public class DepartmentExtEntity : DepartmentEntity
    {

        public virtual IList<DepartmentEntity> childrenAll
        {
            get;
            set;
        }
       
    }
}
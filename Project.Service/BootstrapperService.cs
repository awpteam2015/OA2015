using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Project.Infrastructure.FrameworkCore.AutoMapper;
using Project.Model.HRManager;
using Project.Model.PermissionManager;
using Project.Service.PermissionManager.DTO;

namespace Project.Service
{
    public static class BootstrapperService
    {
        public static void Init()
        {
            InitAutoMapper();
        }

        private static void InitAutoMapper()
        {
            Mapper.CreateMap<EmployeeInfoEntity, TechnicalEntity>().IgnoreAllNull();
            Mapper.CreateMap<EmployeeInfoEntity, EmployeeInfoEntity>().IgnoreAllNull();
            Mapper.CreateMap<EmployeeInfoEntity, EmployeeInfoHisEntity>().IgnoreAllNull();
            Mapper.CreateMap<EmployeeYearDetailEntity, EmployeeYearDetailEntity>().IgnoreAllNull();
            Mapper.CreateMap<EmployeeYearMainEntity, EmployeeYearMainEntity>().IgnoreAllNull();
            Mapper.CreateMap<ContractEntity, ContractEntity>().IgnoreAllNull();
            Mapper.CreateMap<DepartmentExtEntity, DepartmentEntity>().IgnoreAllNull();
            Mapper.CreateMap<AttendanceEntity, AttendanceEntity>().IgnoreAllNull();
            Mapper.CreateMap<AttendanceUploadRecordEntity, AttendanceUploadRecordEntity>().IgnoreAllNull();
            Mapper.CreateMap<FunctionDetailEntity, PermissionFunctionDetailDTO>();
            Mapper.CreateMap<UserInfoEntity, LoginUserInfoDTO>().ForMember(a=>a.PermissionCodeList,b=>b.MapFrom(src=>src.UserFunctionDetailList_Checked));
        }

    }
}

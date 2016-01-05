using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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
            Mapper.CreateMap<FunctionDetailEntity, PermissionFunctionDetailDTO>();

            Mapper.CreateMap<UserInfoEntity, LoginUserInfoDTO>().ForMember(a=>a.PermissionCodeList,b=>b.MapFrom(src=>src.UserFunctionDetailList_Checked));
        }

    }
}

using AutoMapper;
using Yuque.Core.Dtos.GlobalSettings;
using Yuque.Core.Dtos.Menus;
using Yuque.Core.Dtos.Regions;
using Yuque.Core.Dtos.Roles;
using Yuque.Core.Dtos.Users;
using Yuque.Core.Entities.SystemManagement;
using Yuque.Core.Entities.Users;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yuque.Core.MapProfiles
{
    /// <summary>
    /// 新增和修改数据的映射文件
    /// </summary>
    public class AutoMapperCreateProfile : Profile
    {
        public AutoMapperCreateProfile()
        {
            CreateMap<CreateMenuDto, Menu>();

            CreateMap<CreateRoleDto, Role>();

            CreateMap<CreateRegionDto, Region>();

            CreateMap<CreateUserDto, User>()
                .ForMember(a => a.UserRoles, a => a.Ignore());

            CreateMap<CreateUserRoleDto, UserRole>();

            CreateMap<CreateGlobalSettingDto, GlobalSettings>();
        }
    }
}

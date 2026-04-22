using AutoMapper;
using Yuque.Core.Dtos;
using Yuque.Core.Dtos.DownloadCenter;
using Yuque.Core.Dtos.Files;
using Yuque.Core.Dtos.GlobalSettings;
using Yuque.Core.Dtos.Menus;
using Yuque.Core.Dtos.Regions;
using Yuque.Core.Dtos.Roles;
using Yuque.Core.Dtos.ScheduleTasks;
using Yuque.Core.Dtos.Users;
using Yuque.Core.Entities.AsyncTasks;
using Yuque.Core.Entities.Schedules;
using Yuque.Core.Entities.SystemManagement;
using Yuque.Core.Entities.Users;
using Yuque.Infrastructure.Enums;
using File = Yuque.Core.Entities.SystemManagement.File;

namespace Yuque.Core.MapProfiles
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            #region User & Role

            CreateMap<User, UserDto>()
               .ForMember(a => a.Roles, a => a.Ignore())
               .ForMember(a => a.UserRoles, a => a.Ignore())
               .ForMember(a => a.Departments, a => a.Ignore())
               .ForMember(a => a.HasPassword, a => a.MapFrom(c => !string.IsNullOrWhiteSpace(c.Password)));

            CreateMap<UserDepartment, UserDepartmentDto>();

            CreateMap<Role, RoleDto>();

            CreateMap<UserRole, UserRoleDto>()
                .ForMember(a => a.RoleName, a => a.MapFrom(c => c.Role != null ? c.Role.Name : string.Empty))
                .ForMember(a => a.Platforms, a => a.MapFrom(c => c.Role != null ? c.Role.Platforms : PlatformType.All));

            CreateMap<UserToken, UserTokenDto>()
                .ForMember(a => a.UserName, a => a.MapFrom(c => c.User != null ? c.User.UserName : string.Empty));

            CreateMap<UserTokenCacheDto, UserTokenDto>();

            CreateMap<Permission, RolePermissionDto>();

            CreateMap<User, CurrentUserDto>()
                //.ForMember(a => a.Roles, a => a.MapFrom(c => c.UserRoles))
                .ForMember(a => a.HasPassword, a => a.MapFrom(c => !string.IsNullOrWhiteSpace(c.Password)));

            CreateMap<UserToken, UserTokenCacheDto>()
                .ForMember(a => a.UserName, a => a.MapFrom(c => c.User != null ? c.User.UserName : string.Empty));

            CreateMap<UserToken, UserTokenLogDto>()
                .ForMember(a => a.loginUser, a => a.MapFrom(c => c.User != null ? (c.User.RealName ?? c.User.UserName) : string.Empty))
                .ForMember(a => a.loginAt, a => a.MapFrom(c => c.CreatedAt));

            #endregion

            CreateMap<Menu, MenuDto>();

            CreateMap<Menu, MenuTreeDto>()
                .ForMember(a => a.Children, a => a.Ignore());

            CreateMap<MenuDto, MenuTreeDto>();

            CreateMap<SeedDataTask, SeedDataTaskDto>();

            CreateMap<ApiResource, ApiResourceDto>();

            CreateMap<ScheduleTask, ScheduleTaskDto>();

            CreateMap<ApiResourceDto, MenuResourceDto>();

            CreateMap<File, FileDto>();

            CreateMap<Region, RegionDto>();

            CreateMap<Region, RegionTreeDto>()
                .ForMember(a => a.Children, a => a.Ignore());

            CreateMap<RegionDto, RegionTreeDto>();

            CreateMap<ScheduleTask, ScheduleTaskExecuteDto>();

            CreateMap<ScheduleTaskExecuteDto, ScheduleTask>();

            

            

            CreateMap<AsyncTask, AsyncTaskDto>();

            CreateMap<File, ExportFileDto>();

            CreateMap<GlobalSettings, GlobalSettingDto>();

            CreateMap<DownloadItem, DownloadItemDto>();
        }
    }
}

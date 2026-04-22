using AutoMapper;
using Yuque.Core.Dtos.Menus;
using Yuque.Core.Entities.SystemManagement;
using Yuque.Core.Services.Interfaces;
using Yuque.EFCore.DbContexts;
using Yuque.EFCore.Repository;
using Yuque.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yuque.Core.Services.SystemManagement
{
    public class ApiResrouceCoreService(MainContext dbContext, IMapper mapper) : ServiceBase<ApiResource>(dbContext, mapper), IApiResrouceCoreService, IScopedDependency
    {
        public async Task<List<MenuResourceDto>> GetTreeListAsync()
        {
            var resources = await GetListAsync();
            return resources.GroupBy(a => new { a.NameSpace, a.ControllerName, a.GroupName }).OrderBy(a => a.Key.NameSpace).Select(a =>
            {
                var resource = new MenuResourceDto
                {
                    Name = a.Key.GroupName ?? string.Empty,
                    Code = $"{a.Key.NameSpace}.{a.Key.ControllerName}",
                };

                resource.Operations = a.Select(c => Mapper.Map<MenuResourceDto>(c)).ToList();

                return resource;
            }).ToList();
        }
    }
}

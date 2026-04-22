using Yuque.Core.Dtos.Menus;
using Yuque.Core.Entities.SystemManagement;
using Yuque.EFCore.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yuque.Core.Services.Interfaces
{
    public interface IApiResrouceCoreService : IServiceBase<ApiResource>
    {
        /// <summary>
        /// 获取接口资源定义树列表
        /// </summary>
        /// <returns></returns>
        Task<List<MenuResourceDto>> GetTreeListAsync();
    }
}

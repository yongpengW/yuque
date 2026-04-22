using Yuque.Core.Dtos.Regions;
using Yuque.Core.Entities.SystemManagement;
using Yuque.EFCore.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using File = Yuque.Core.Entities.SystemManagement.File;

namespace Yuque.Core.Services.Interfaces
{
    public interface IRegionService : IServiceBase<Region>
    {
        /// <summary>
        /// 获取区域树
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<List<RegionTreeDto>> GetTreeListAsync(RegionTreeQueryDto model);
    }
}

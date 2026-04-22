using Yuque.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yuque.Core.Dtos.Regions
{
    public class RegionQueryDto : PagedQueryModelBase
    {
        /// <summary>
        /// 父级区域 Id
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 是否只查询当前区域下
        /// </summary>
        public bool IsCurrent { get; set; } = false;
    }
}

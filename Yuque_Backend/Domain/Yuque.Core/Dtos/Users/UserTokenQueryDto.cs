using Yuque.Infrastructure.Enums;
using Yuque.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yuque.Core.Dtos.Users
{
    public class UserTokenQueryDto : PagedQueryModelBase
    {
        /// <summary>
        /// 所属平台
        /// </summary>
        public PlatformType platformType { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTimeOffset? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTimeOffset? EndTime { get; set; }
    }
}

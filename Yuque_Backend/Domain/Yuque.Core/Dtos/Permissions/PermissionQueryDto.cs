using Yuque.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yuque.Core.Dtos.Permissions
{
    public class PermissionQueryDto
    {
        /// <summary>
        /// 角色编号
        /// </summary>
        public long RoleId { get; set; }

        public PlatformType? PlatformType { get; set; }
    }
}

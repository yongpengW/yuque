using Yuque.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yuque.Core.Dtos.OpenApiConfigs
{
    public class AppConfigQueryDto : PagedQueryModelBase
    {
        public string? AppName { get; set; }
    }
}

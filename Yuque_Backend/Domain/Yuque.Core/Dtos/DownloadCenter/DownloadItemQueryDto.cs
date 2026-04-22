using Yuque.Infrastructure.Enums;
using Yuque.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yuque.Core.Dtos.DownloadCenter
{
    public class DownloadItemQueryDto : PagedQueryModelBase
    {
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public ExportState? State { get; set; }
    }
}

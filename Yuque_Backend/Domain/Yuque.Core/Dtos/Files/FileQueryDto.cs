using Yuque.Infrastructure.Enums;
using Yuque.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yuque.Core.Dtos.Files
{
    public class FileQueryDto : PagedQueryModelBase
    {
        /// <summary>
        /// 文件类型
        /// </summary>
        public FileType FileType { get; set; }
    }
}

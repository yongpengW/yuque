using Yuque.Core.Entities.SystemManagement;
using Yuque.EFCore.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yuque.Core.Services.Interfaces
{
    public interface IDownloadService : IServiceBase<DownloadItem>
    {
        Task<byte[]> ExportExcelAsync(string typeName, string queryData, string? password = null);
    }
}

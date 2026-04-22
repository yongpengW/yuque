using Microsoft.AspNetCore.Mvc;
using Yuque.Core.Attributes;
using Yuque.Core.Dtos.DownloadCenter;
using Yuque.Core.Dtos.Files;
using Yuque.Core.Entities.SystemManagement;
using Yuque.Core.Services.Interfaces;
using Yuque.EFCore.Repository;
using Yuque.Infrastructure.Enums;
using Yuque.Infrastructure.Exceptions;
using Yuque.Infrastructure.FileStroage;
using X.PagedList;
using Ardalis.Specification;

namespace Yuque.WebAPI.Controllers
{
    /// <summary>
    /// 下载中心
    /// </summary>
    public class DownloadController(IDownloadService downloadService,
        IFileStorageFactory storageFactory) : BaseController
    {
        [HttpGet("list"), NoLogging]
        public async Task<IPagedList<DownloadItemDto>> GetListAsync([FromQuery] DownloadItemQueryDto model)
        {
            var spec = Specifications<DownloadItem>.Create();

            spec.Query.Where(x => x.CreatedBy == CurrentUser.UserId);

            if (model.StartDate.HasValue)
            {
                spec.Query.Where(x => x.CreatedAt >= model.StartDate);
            }
            if (model.EndDate.HasValue)
            {
                spec.Query.Where(x => x.CreatedAt <= model.EndDate);
            }
            if (model.State.HasValue)
            {
                spec.Query.Where(x => x.State == model.State);
            }
            if (!string.IsNullOrEmpty(model.Keyword))
            {
                spec.Query.Where(x => x.Name.Contains(model.Keyword));
            }

            spec.Query.OrderByDescending(a => a.CreatedAt);

            var result = await downloadService.GetPagedListAsync<DownloadItemDto>(spec, model.Page, model.Limit);

            return result;
        }

        [HttpGet("download"), NoLogging]
        public async Task<ExportbufferDto> GetDownloadLinkAsync(long id)
        {
            var downloadItem = await downloadService.GetByIdAsync(id)
                ?? throw new BusinessException("未找到对应的导出记录");

            var aliyunStorage = storageFactory.GetStorage(FileStorageType.Aliyun);
            var dataByte = aliyunStorage.GetDownloadCenterFile(downloadItem.key);

            var result = new ExportbufferDto
            {
                Buffer = string.Join(",", dataByte),
                Size = dataByte.Length,
                FileName = downloadItem.Name,
                Type = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            };

            return result;
        }
    }
}

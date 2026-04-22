using AutoMapper;
using LinqKit;
using Yuque.Core.Dtos.DownloadCenter;
using Yuque.Core.Entities.SystemManagement;
using Yuque.Core.Services.Interfaces;
using Yuque.EFCore.DbContexts;
using Yuque.EFCore.Repository;
using Yuque.Excel.ExportStream;
using Yuque.Infrastructure;
using Yuque.Infrastructure.Exceptions;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Yuque.Core.Services.SystemManagement
{
    public class DownloadService(MainContext dbContext, IMapper mapper,
    Lazy<IUserService> userService,
    Lazy<ICurrentUser> currentUser
    ) : ServiceBase<DownloadItem>(dbContext, mapper), IDownloadService, IScopedDependency
    {
        private List<ExportExcelTypeMapDto>? _exportTypeMap;
        private List<ExportExcelTypeMapDto> ExportTypeMap => _exportTypeMap ??= InitExportTypeMap();

        private List<ExportExcelTypeMapDto> InitExportTypeMap()
        {
            //lazy load export type map
            return
            [
                // To Do
            ];
        }


        private ExportExcelTypeMapDto GetExportTypeMap(string typeName)
        {
            var exportType = ExportTypeMap.FirstOrDefault(x => x.TypeName == typeName)
                ?? throw new ArgumentException($"未找到导出类型 {typeName}");

            return exportType;
        }

        public async Task<byte[]> ExportExcelAsync(string typeName, string queryData, string? password = null)
        {
            var exportType = GetExportTypeMap(typeName);

            var queryModel = JsonSerializer.Deserialize(queryData, exportType.QueryModel)
                ?? throw new ArgumentException($"无法反序列化查询数据为 {exportType.QueryModel.Name}");

            var method = exportType.Method
                ?? throw new ArgumentException($"未找到导出方法 {exportType.Method}");

            var dataByte = await method(queryModel);

            if (exportType.Encrypt && !string.IsNullOrEmpty(password))
            {
                dataByte = ExcelEncryptHelper.EncryptExcel(dataByte, password);
            }

            return dataByte;
        }
    }
}

using AutoMapper;
using Yuque.Core.Entities.OpenAppConfigs;
using Yuque.EFCore.DbContexts;
using Yuque.EFCore.Repository;
using Yuque.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yuque.Core.Services.OpenAppConfigs
{
    /// <summary>
    /// 开放API配置服务
    /// </summary>
    public interface IAppConfigService : IServiceBase<AppConfig>
    {

    }
    public class AppConfigService(MainContext dbContext, IMapper mapper) : ServiceBase<AppConfig>(dbContext, mapper), IAppConfigService, IScopedDependency
    {

    }
}

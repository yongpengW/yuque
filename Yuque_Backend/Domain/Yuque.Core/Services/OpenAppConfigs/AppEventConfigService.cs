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
    public interface IAppEventConfigService : IServiceBase<AppEventConfig>
    {
    }
    public class AppEventConfigService(MainContext dbContext, IMapper mapper) : ServiceBase<AppEventConfig>(dbContext, mapper), IAppEventConfigService, IScopedDependency
    {
    }
}

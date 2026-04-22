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
    public interface IAppNotificationConfigService : IServiceBase<AppNotificationConfig>
    {
    }
    public class AppNotificationConfigService(MainContext dbContext, IMapper mapper) : ServiceBase<AppNotificationConfig>(dbContext, mapper), IAppNotificationConfigService, IScopedDependency
    {
    }
}

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
    public interface IAppWebhookConfigService : IServiceBase<AppWebhookConfig>
    {
    }
    public class AppWebhookConfigService(MainContext dbContext, IMapper mapper) : ServiceBase<AppWebhookConfig>(dbContext, mapper), IAppWebhookConfigService, IScopedDependency
    {
    }
}

using AutoMapper;
using Yuque.Core.Entities.Schedules;
using Yuque.Core.Services.Interfaces;
using Yuque.EFCore.DbContexts;
using Yuque.EFCore.Repository;
using Yuque.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yuque.Core.Services.Schedules
{
    public class SeedDataTaskCoreService(MainContext dbContext, IMapper mapper) : ServiceBase<SeedDataTask>(dbContext, mapper), ISeedDataTaskCoreService, IScopedDependency
    {

    }
}

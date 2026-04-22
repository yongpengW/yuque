using AutoMapper;
using Yuque.Core.Entities.Users;
using Yuque.EFCore.DbContexts;
using Yuque.EFCore.Repository;
using Yuque.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yuque.Core.Services.Users
{
    public interface IUserDepartmentService : IServiceBase<UserDepartment>
    {

    }
    public class UserDepartmentService(MainContext dbContext, IMapper mapper) : ServiceBase<UserDepartment>(dbContext, mapper), IUserDepartmentService, IScopedDependency
    {

    }
}

using Yuque.Core.Entities.Schedules;
using Yuque.EFCore.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yuque.Core.Services.Interfaces
{
    public interface IScheduleTaskService : IServiceBase<ScheduleTask>
    {
        /// <summary>
        /// 初始化定时任务
        /// </summary>
        /// <returns></returns>
        Task InitializeAsync();
    }
}

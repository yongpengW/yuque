using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Yuque.Core.Dtos.ScheduleTasks;
using Yuque.Core.Entities.Schedules;
using Yuque.Core.Schedules;
using Yuque.Core.Services.Interfaces;
using Yuque.EFCore.DbContexts;
using Yuque.EFCore.Repository;
using Yuque.Infrastructure;
using Yuque.Infrastructure.Constants;
using Yuque.Infrastructure.TypeFinders;
using Yuque.Redis;
using Yuque.Infrastructure.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yuque.Core.Services.Schedules
{
    public class ScheduleTaskService(MainContext dbContext, 
        IMapper mapper, 
        IServiceScopeFactory scopeFactory, 
        IRedisService redisService) : ServiceBase<ScheduleTask>(dbContext, mapper), IScheduleTaskService, IScopedDependency
    {
        public async Task InitializeAsync()
        {
            var cronType = typeof(CronScheduleService);

            foreach (var cronService in TypeFinders.SearchTypes(cronType, TypeFinders.TypeClassification.Class))
            {
                var code = cronService.FullName;

                var cacheKey = CoreRedisConstants.ScheduleTaskCache.Format(code);

                // 从 Redis 中删除缓存
                await redisService.DeleteAsync(CoreRedisConstants.ScheduleTaskCache.Format(cacheKey));

                var exists = await GetAsync(a => a.Code == code);


                if (exists is null)
                {
                    var name = DocsHelper.GetTypeComments(cronService.Assembly.GetName().Name, cronService);

                    exists = new ScheduleTask
                    {
                        Code = code,
                        IsEnable = true,
                        Name = name.IsNullOrEmpty() ? code : name
                    };

                    // 将任务插入数据库中
                    await this.InsertAsync(exists);
                }


                var cacheValue = Mapper.Map<ScheduleTaskExecuteDto>(exists);

                // 将任务添加到 Redis 中
                await redisService.SetAsync(cacheKey, cacheValue);
            }
        }

        public async Task UpdateScheduleTaskStatusAsync(long scheduleTaskId, bool IsEnable, string cronExpression, DateTime nextExecuteTime)
        {
            var scheduleTask = await GetAsync(a => a.Id == scheduleTaskId);
            scheduleTask.IsEnable = IsEnable;
            scheduleTask.Expression = cronExpression;
            scheduleTask.NextExecuteTime = nextExecuteTime;

            await this.UpdateAsync(scheduleTask);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        public async Task UpdateAsync(ScheduleTask task)
        {
            await base.UpdateAsync(task);

            var cacheValue = Mapper.Map<ScheduleTaskExecuteDto>(task);

            // 将任务添加到 Redis 中
            await redisService.SetAsync(CoreRedisConstants.ScheduleTaskCache.Format(task.Code), cacheValue);
        }
    }
}

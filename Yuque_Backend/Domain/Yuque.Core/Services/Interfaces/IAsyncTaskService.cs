using Yuque.Core.Dtos.DownloadCenter;
using Yuque.Core.Entities.AsyncTasks;
using Yuque.EFCore.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Yuque.Core.Services.Interfaces
{
    /// <summary>
    /// 异步任务接口定义
    /// </summary>
    public interface IAsyncTaskService : IServiceBase<AsyncTask>
    {
        /// <summary>
        /// 创建MQ异步任务
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<AsyncTask> CreateTaskAsync(object data, string code);

        /// <summary>
        /// 创建延迟MQ异步任务
        /// </summary>
        /// <param name="data"></param>
        /// <param name="code"></param>
        /// <param name="delayTier">延迟档位（1/3/5/10/30/60/120 分钟）</param>
        /// <returns></returns>
        Task<AsyncTask> CreateDelayedTaskAsync(object data, string code, Yuque.RabbitMQ.EventBus.DelayTier delayTier);

        /// <summary>
        /// 重试异步任务
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        Task<bool> RetryAsync(AsyncTask task);
    }
}

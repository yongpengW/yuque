using Yuque.Infrastructure.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace Yuque.Core.Entities.AsyncTasks
{
    public static class AsyncTaskExtensions
    {
        public static T GetData<T>(this AsyncTask task)
        {
            var result = JsonSerializer.Deserialize<T>(task.Data, JsonOptions.Default);
            if (result == null)
            {
                throw new InvalidOperationException($"Failed to deserialize AsyncTask data to type {typeof(T).Name}");
            }
            return result;
        }
    }
}

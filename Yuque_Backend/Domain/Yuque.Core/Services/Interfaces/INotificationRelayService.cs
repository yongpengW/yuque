using Yuque.Core.Entities.AsyncTasks;

namespace Yuque.Core.Services.Interfaces
{
    public interface INotificationRelayService
    {
        Task<AsyncTask> NotifyToUserAsync(long userId, object payload, string eventName = "Notification");

        Task<AsyncTask> NotifyToGroupAsync(string group, object payload, string eventName = "Notification");

        Task<AsyncTask> NotifyToAllAsync(object payload, string eventName = "Notification");
    }
}


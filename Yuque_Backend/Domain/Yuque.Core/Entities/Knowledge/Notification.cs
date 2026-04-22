using System.ComponentModel.DataAnnotations;
using Yuque.EFCore.Entities;

namespace Yuque.Core.Entities.Knowledge
{
    /// <summary>站内通知（收件箱）</summary>
    public class Notification : AuditedEntity
    {
        public long UserId { get; set; }

        [Required]
        public ContentNotificationType Type { get; set; }

        [MaxLength(300)]
        public string? Title { get; set; }

        public string? Body { get; set; }

        /// <summary>跳转上下文（jsonb）</summary>
        public string? Payload { get; set; }

        public DateTimeOffset? ReadAt { get; set; }
    }
}

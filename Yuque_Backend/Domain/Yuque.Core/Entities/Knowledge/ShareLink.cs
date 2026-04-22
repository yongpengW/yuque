using System.ComponentModel.DataAnnotations;
using Yuque.EFCore.Entities;

namespace Yuque.Core.Entities.Knowledge
{
    /// <summary>外部分享链接（预留）</summary>
    public class ShareLink : AuditedEntity
    {
        [Required]
        [MaxLength(128)]
        public string TokenHash { get; set; } = string.Empty;

        [Required]
        [MaxLength(30)]
        public string TargetType { get; set; } = string.Empty;

        public long? KnowledgeBaseId { get; set; }

        public long? KnowledgeBaseEntryId { get; set; }

        [MaxLength(255)]
        public string? PasswordHash { get; set; }

        public DateTimeOffset? ExpireAt { get; set; }

        public string? Settings { get; set; }

        public long CreatedByUserId { get; set; }

        public DateTimeOffset? RevokedAt { get; set; }
    }
}

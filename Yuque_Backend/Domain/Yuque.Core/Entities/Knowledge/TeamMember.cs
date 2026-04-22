using System.ComponentModel.DataAnnotations;
using Yuque.EFCore.Entities;

namespace Yuque.Core.Entities.Knowledge
{
    /// <summary>团队成员</summary>
    public class TeamMember : AuditedEntity
    {
        public long TeamId { get; set; }

        public long UserId { get; set; }

        [Required]
        public TeamMemberRole Role { get; set; }

        public long? InvitedByUserId { get; set; }

        public DateTimeOffset JoinedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}

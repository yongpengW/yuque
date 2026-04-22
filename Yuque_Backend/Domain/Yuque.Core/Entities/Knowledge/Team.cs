using System.ComponentModel.DataAnnotations;
using Yuque.EFCore.Entities;

namespace Yuque.Core.Entities.Knowledge
{
    /// <summary>
    /// 团队（含个人空间）。OwnerUserId 仅业务关联 User.Id，不设数据库外键。
    /// </summary>
    public class Team : AuditedEntity
    {
        [Required]
        public TeamType Type { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Slug { get; set; } = string.Empty;

        public string? Description { get; set; }

        [MaxLength(500)]
        public string? AvatarUrl { get; set; }

        /// <summary>主负责人，对应 User.Id</summary>
        public long OwnerUserId { get; set; }

        /// <summary>扩展配置 JSON（jsonb）</summary>
        public string? Settings { get; set; }
    }
}

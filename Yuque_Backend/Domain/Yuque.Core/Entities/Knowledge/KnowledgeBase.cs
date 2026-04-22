using System.ComponentModel.DataAnnotations;
using Yuque.EFCore.Entities;

namespace Yuque.Core.Entities.Knowledge
{
    /// <summary>
    /// 知识库（对应设计文档 repositories）。与 <see cref="Team"/> 业务 Id 关联。
    /// </summary>
    public class KnowledgeBase : AuditedEntity
    {
        public long TeamId { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(150)]
        public string Slug { get; set; } = string.Empty;

        public string? Description { get; set; }

        [MaxLength(500)]
        public string? CoverUrl { get; set; }

        [Required]
        public KnowledgeBaseVisibility Visibility { get; set; } = KnowledgeBaseVisibility.Private;

        /// <summary>导航、导出策略等（jsonb）</summary>
        public string? Settings { get; set; }

        public DateTimeOffset? ArchivedAt { get; set; }
    }
}

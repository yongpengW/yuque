using System.ComponentModel.DataAnnotations;
using Yuque.EFCore.Entities;

namespace Yuque.Core.Entities.Knowledge
{
    /// <summary>文档业务表，与文档类 <see cref="KnowledgeBaseEntry"/> 1:1</summary>
    public class Document : AuditedEntity
    {
        /// <summary>对应 entry 主键</summary>
        public long KnowledgeBaseEntryId { get; set; }

        public long KnowledgeBaseId { get; set; }

        [Required]
        public DocumentStatus Status { get; set; } = DocumentStatus.Draft;

        public DateTimeOffset? PublishedAt { get; set; }

        /// <summary>当前版本，逻辑关联 <see cref="DocumentVersion.Id"/></summary>
        public long? CurrentVersionId { get; set; }

        [Required]
        [MaxLength(50)]
        public string ContentFormat { get; set; } = "block_json";

        /// <summary>封面、摘要等（jsonb）</summary>
        public string? Properties { get; set; }
    }
}

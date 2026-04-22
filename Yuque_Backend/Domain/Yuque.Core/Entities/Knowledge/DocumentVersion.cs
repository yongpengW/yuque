using System.ComponentModel.DataAnnotations;
using Yuque.EFCore.Entities;

namespace Yuque.Core.Entities.Knowledge
{
    /// <summary>文档版本（正文 jsonb）</summary>
    public class DocumentVersion : AuditedEntity
    {
        public long DocumentId { get; set; }

        public int VersionNo { get; set; }

        /// <summary>作者 User.Id，与 CreatedBy 语义一致时可只维护其一</summary>
        public long AuthorId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Title { get; set; } = string.Empty;

        /// <summary>块编辑器 JSON，列类型 jsonb</summary>
        [Required]
        public string Body { get; set; } = "{}";

        [MaxLength(500)]
        public string? ChangeNote { get; set; }
    }
}

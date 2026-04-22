using System.ComponentModel.DataAnnotations;
using Yuque.EFCore.Entities;

namespace Yuque.Core.Entities.Knowledge
{
    /// <summary>文档评论</summary>
    public class Comment : AuditedEntity
    {
        public long DocumentId { get; set; }

        public long? KnowledgeBaseEntryId { get; set; }

        public long? ParentId { get; set; }

        public long AuthorId { get; set; }

        [Required]
        public string Body { get; set; } = string.Empty;

        /// <summary>段落/块定位（jsonb）</summary>
        public string? Anchor { get; set; }

        [Required]
        public CommentStatus Status { get; set; } = CommentStatus.Open;
    }
}

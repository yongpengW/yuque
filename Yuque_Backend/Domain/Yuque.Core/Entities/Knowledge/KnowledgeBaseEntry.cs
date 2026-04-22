using System.ComponentModel.DataAnnotations;
using Yuque.EFCore.Entities;

namespace Yuque.Core.Entities.Knowledge
{
    /// <summary>知识库目录树节点（文件夹或文档占位）</summary>
    public class KnowledgeBaseEntry : AuditedEntity
    {
        public long KnowledgeBaseId { get; set; }

        public long? ParentId { get; set; }

        [Required]
        public KnowledgeEntryType EntryType { get; set; }

        [Required]
        [MaxLength(500)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Slug { get; set; } = string.Empty;

        public int SortOrder { get; set; }
    }
}

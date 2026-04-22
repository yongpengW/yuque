using System.ComponentModel.DataAnnotations;
using Yuque.EFCore.Entities;

namespace Yuque.Core.Entities.Knowledge
{
    /// <summary>语雀附件元数据（独立表，不复用系统 File）</summary>
    public class Attachment : AuditedEntity
    {
        public long KnowledgeBaseId { get; set; }

        public long? DocumentId { get; set; }

        public long UploaderId { get; set; }

        [Required]
        [MaxLength(500)]
        public string StorageKey { get; set; } = string.Empty;

        [Required]
        [MaxLength(500)]
        public string FileName { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? MimeType { get; set; }

        public long SizeBytes { get; set; }

        [MaxLength(128)]
        public string? Checksum { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using Yuque.EFCore.Entities;

namespace Yuque.Core.Entities.Knowledge
{
    /// <summary>文档级权限覆盖</summary>
    public class DocumentPermission : AuditedEntity
    {
        public long DocumentId { get; set; }

        public long UserId { get; set; }

        [Required]
        public DocumentGrantPermission Permission { get; set; }
    }
}

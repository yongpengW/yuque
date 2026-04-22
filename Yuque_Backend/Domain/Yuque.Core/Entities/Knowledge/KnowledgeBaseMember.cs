using System.ComponentModel.DataAnnotations;
using Yuque.EFCore.Entities;

namespace Yuque.Core.Entities.Knowledge
{
    /// <summary>知识库协作成员（库级 ACL）</summary>
    public class KnowledgeBaseMember : AuditedEntity
    {
        public long KnowledgeBaseId { get; set; }

        public long UserId { get; set; }

        [Required]
        public KnowledgeBaseMemberRole Role { get; set; }

        public long? InvitedByUserId { get; set; }
    }
}

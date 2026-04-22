using System.ComponentModel.DataAnnotations;
using Yuque.EFCore.Entities;

namespace Yuque.Core.Entities.Knowledge
{
    /// <summary>收藏（知识库或文档节点）</summary>
    public class Favorite : AuditedEntity
    {
        public long UserId { get; set; }

        [Required]
        public FavoriteTargetType TargetType { get; set; }

        public long? KnowledgeBaseId { get; set; }

        public long? KnowledgeBaseEntryId { get; set; }
    }
}

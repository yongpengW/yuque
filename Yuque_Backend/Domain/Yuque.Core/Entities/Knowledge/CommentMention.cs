using Yuque.EFCore.Entities;

namespace Yuque.Core.Entities.Knowledge
{
    /// <summary>评论中的 @ 提及</summary>
    public class CommentMention : Entity
    {
        public long CommentId { get; set; }

        public long MentionedUserId { get; set; }

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
    }
}

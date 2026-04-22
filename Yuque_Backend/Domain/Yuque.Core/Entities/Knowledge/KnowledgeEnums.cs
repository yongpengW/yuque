namespace Yuque.Core.Entities.Knowledge
{
    /// <summary>团队类型：个人空间 / 组织</summary>
    public enum TeamType : short
    {
        Personal = 0,
        Organization = 1
    }

    /// <summary>团队成员角色</summary>
    public enum TeamMemberRole : short
    {
        Owner = 0,
        Admin = 1,
        Member = 2,
        Guest = 3
    }

    /// <summary>知识库可见性</summary>
    public enum KnowledgeBaseVisibility : short
    {
        Private = 0,
        Team = 1,
        Public = 2
    }

    /// <summary>知识库成员角色（库级 ACL）</summary>
    public enum KnowledgeBaseMemberRole : short
    {
        Viewer = 0,
        Editor = 1,
        Admin = 2
    }

    /// <summary>目录树节点类型</summary>
    public enum KnowledgeEntryType : short
    {
        Folder = 0,
        Document = 1
    }

    /// <summary>文档业务状态</summary>
    public enum DocumentStatus : short
    {
        Draft = 0,
        Published = 1,
        Archived = 2
    }

    /// <summary>文档级授权（覆盖继承）</summary>
    public enum DocumentGrantPermission : short
    {
        View = 0,
        Comment = 1,
        Edit = 2,
        Admin = 3
    }

    /// <summary>评论状态</summary>
    public enum CommentStatus : short
    {
        Open = 0,
        Resolved = 1
    }

    /// <summary>站内通知类型</summary>
    public enum ContentNotificationType : short
    {
        Comment = 0,
        Mention = 1,
        Permission = 2,
        Share = 3,
        System = 4
    }

    /// <summary>收藏目标类型</summary>
    public enum FavoriteTargetType : short
    {
        KnowledgeBase = 0,
        Document = 1
    }
}

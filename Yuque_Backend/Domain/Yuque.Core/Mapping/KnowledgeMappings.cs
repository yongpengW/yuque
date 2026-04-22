using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Yuque.Core.Entities.Knowledge;
using Yuque.EFCore.Mapping;

namespace Yuque.Core.Mapping
{
    public class TeamMapping : MapBase<Team>
    {
        public override void Configure(EntityTypeBuilder<Team> builder)
        {
            base.Configure(builder);
            builder.Property(e => e.Settings).HasColumnType("jsonb");
            builder.HasIndex(e => e.Slug).IsUnique();
            builder.HasIndex(e => e.OwnerUserId);
            builder.HasIndex(e => new { e.Type, e.OwnerUserId });
        }
    }

    public class TeamMemberMapping : MapBase<TeamMember>
    {
        public override void Configure(EntityTypeBuilder<TeamMember> builder)
        {
            base.Configure(builder);
            builder.HasIndex(e => new { e.TeamId, e.UserId }).IsUnique();
            builder.HasIndex(e => e.UserId);
        }
    }

    public class KnowledgeBaseMapping : MapBase<KnowledgeBase>
    {
        public override void Configure(EntityTypeBuilder<KnowledgeBase> builder)
        {
            base.Configure(builder);
            builder.ToTable("KnowledgeBase");
            builder.Property(e => e.Settings).HasColumnType("jsonb");
            builder.HasIndex(e => new { e.TeamId, e.Slug })
                .IsUnique()
                .HasFilter("\"IsDeleted\" = false");
            builder.HasIndex(e => e.TeamId);
        }
    }

    public class KnowledgeBaseMemberMapping : MapBase<KnowledgeBaseMember>
    {
        public override void Configure(EntityTypeBuilder<KnowledgeBaseMember> builder)
        {
            base.Configure(builder);
            builder.ToTable("KnowledgeBaseMember");
            builder.HasIndex(e => new { e.KnowledgeBaseId, e.UserId }).IsUnique();
            builder.HasIndex(e => e.UserId);
        }
    }

    public class KnowledgeBaseEntryMapping : MapBase<KnowledgeBaseEntry>
    {
        public override void Configure(EntityTypeBuilder<KnowledgeBaseEntry> builder)
        {
            base.Configure(builder);
            builder.ToTable("KnowledgeBaseEntry");
            builder.HasIndex(e => new { e.KnowledgeBaseId, e.ParentId, e.Slug })
                .IsUnique()
                .HasFilter("\"IsDeleted\" = false");
            builder.HasIndex(e => new { e.KnowledgeBaseId, e.ParentId });
        }
    }

    public class DocumentMapping : MapBase<Document>
    {
        public override void Configure(EntityTypeBuilder<Document> builder)
        {
            base.Configure(builder);
            builder.Property(e => e.Properties).HasColumnType("jsonb");
            builder.HasIndex(e => e.KnowledgeBaseEntryId).IsUnique();
            builder.HasIndex(e => e.KnowledgeBaseId);
            builder.HasIndex(e => e.CurrentVersionId);
        }
    }

    public class DocumentVersionMapping : MapBase<DocumentVersion>
    {
        public override void Configure(EntityTypeBuilder<DocumentVersion> builder)
        {
            base.Configure(builder);
            builder.Property(e => e.Body).HasColumnType("jsonb");
            builder.HasIndex(e => new { e.DocumentId, e.VersionNo }).IsUnique();
            builder.HasIndex(e => e.DocumentId);
        }
    }

    public class DocumentPermissionMapping : MapBase<DocumentPermission>
    {
        public override void Configure(EntityTypeBuilder<DocumentPermission> builder)
        {
            base.Configure(builder);
            builder.HasIndex(e => new { e.DocumentId, e.UserId }).IsUnique();
        }
    }

    public class CommentMapping : MapBase<Comment>
    {
        public override void Configure(EntityTypeBuilder<Comment> builder)
        {
            base.Configure(builder);
            builder.Property(e => e.Anchor).HasColumnType("jsonb");
            builder.HasIndex(e => e.DocumentId);
            builder.HasIndex(e => e.ParentId);
        }
    }

    public class CommentMentionMapping : MapBase<CommentMention>
    {
        public override void Configure(EntityTypeBuilder<CommentMention> builder)
        {
            base.Configure(builder);
            builder.HasIndex(e => new { e.CommentId, e.MentionedUserId }).IsUnique();
            builder.HasIndex(e => e.MentionedUserId);
        }
    }

    public class AttachmentMapping : MapBase<Attachment>
    {
        public override void Configure(EntityTypeBuilder<Attachment> builder)
        {
            base.Configure(builder);
            builder.HasIndex(e => e.KnowledgeBaseId);
            builder.HasIndex(e => e.DocumentId);
            builder.HasIndex(e => e.UploaderId);
        }
    }

    public class NotificationMapping : MapBase<Notification>
    {
        public override void Configure(EntityTypeBuilder<Notification> builder)
        {
            base.Configure(builder);
            builder.Property(e => e.Payload).HasColumnType("jsonb");
            builder.HasIndex(e => new { e.UserId, e.ReadAt, e.CreatedAt });
        }
    }

    public class FavoriteMapping : MapBase<Favorite>
    {
        public override void Configure(EntityTypeBuilder<Favorite> builder)
        {
            base.Configure(builder);
            builder.HasIndex(e => e.UserId);
            builder.HasIndex(e => new { e.UserId, e.KnowledgeBaseId });
            builder.HasIndex(e => new { e.UserId, e.KnowledgeBaseEntryId });
        }
    }

    public class ShareLinkMapping : MapBase<ShareLink>
    {
        public override void Configure(EntityTypeBuilder<ShareLink> builder)
        {
            base.Configure(builder);
            builder.Property(e => e.Settings).HasColumnType("jsonb");
            builder.HasIndex(e => e.TokenHash).IsUnique();
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Yuque.WebAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitializeDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiResource",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false, comment: ""),
                    Code = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true, comment: ""),
                    GroupName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true, comment: ""),
                    RoutePattern = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true, comment: ""),
                    NameSpace = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true, comment: ""),
                    ControllerName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true, comment: ""),
                    ActionName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true, comment: ""),
                    RequestMethod = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true, comment: ""),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    Remark = table.Column<string>(type: "text", nullable: true, comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiResource", x => x.Id);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "AppConfig",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AppKey = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false, comment: ""),
                    AppName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false, comment: ""),
                    SecretKey = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false, comment: ""),
                    Sessionkey = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false, comment: ""),
                    AccessToken = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true, comment: ""),
                    AccessValidTime = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    Remark = table.Column<string>(type: "text", nullable: true, comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppConfig", x => x.Id);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "AppEventConfig",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AppId = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false, comment: ""),
                    Method = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true, comment: ""),
                    EventCode = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true, comment: ""),
                    HookUrl = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true, comment: ""),
                    Type = table.Column<int>(type: "integer", nullable: false, comment: ""),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    Remark = table.Column<string>(type: "text", nullable: true, comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppEventConfig", x => x.Id);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "AppNotificationConfig",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AppId = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false, comment: ""),
                    Type = table.Column<int>(type: "integer", nullable: false, comment: ""),
                    Phones = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true, comment: ""),
                    NoticeUrl = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true, comment: ""),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    Remark = table.Column<string>(type: "text", nullable: true, comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppNotificationConfig", x => x.Id);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "AppWebhookConfig",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AppId = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false, comment: ""),
                    Method = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true, comment: ""),
                    HookUrl = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true, comment: ""),
                    Type = table.Column<int>(type: "integer", nullable: false, comment: ""),
                    IsEnabled = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    Remark = table.Column<string>(type: "text", nullable: true, comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppWebhookConfig", x => x.Id);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "AsyncTask",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    State = table.Column<int>(type: "integer", nullable: false, comment: ""),
                    Code = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false, comment: ""),
                    Data = table.Column<string>(type: "text", nullable: false, comment: ""),
                    ErrorMessage = table.Column<string>(type: "text", nullable: true, comment: ""),
                    Result = table.Column<string>(type: "text", nullable: true, comment: ""),
                    RetryCount = table.Column<int>(type: "integer", nullable: false, comment: ""),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    Remark = table.Column<string>(type: "text", nullable: true, comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AsyncTask", x => x.Id);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "Attachment",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    KnowledgeBaseId = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    DocumentId = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    UploaderId = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    StorageKey = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false, comment: ""),
                    FileName = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false, comment: ""),
                    MimeType = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true, comment: ""),
                    SizeBytes = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    Checksum = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true, comment: ""),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    Remark = table.Column<string>(type: "text", nullable: true, comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.Id);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DocumentId = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    KnowledgeBaseEntryId = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    ParentId = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    AuthorId = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    Body = table.Column<string>(type: "text", nullable: false, comment: ""),
                    Anchor = table.Column<string>(type: "jsonb", nullable: true, comment: ""),
                    Status = table.Column<short>(type: "smallint", nullable: false, comment: ""),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    Remark = table.Column<string>(type: "text", nullable: true, comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.Id);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "CommentMention",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CommentId = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    MentionedUserId = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentMention", x => x.Id);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "Document",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    KnowledgeBaseEntryId = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    KnowledgeBaseId = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    Status = table.Column<short>(type: "smallint", nullable: false, comment: ""),
                    PublishedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: ""),
                    CurrentVersionId = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    ContentFormat = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false, comment: ""),
                    Properties = table.Column<string>(type: "jsonb", nullable: true, comment: ""),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    Remark = table.Column<string>(type: "text", nullable: true, comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.Id);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "DocumentPermission",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DocumentId = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    UserId = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    Permission = table.Column<short>(type: "smallint", nullable: false, comment: ""),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    Remark = table.Column<string>(type: "text", nullable: true, comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentPermission", x => x.Id);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "DocumentVersion",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DocumentId = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    VersionNo = table.Column<int>(type: "integer", nullable: false, comment: ""),
                    AuthorId = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    Title = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false, comment: ""),
                    Body = table.Column<string>(type: "jsonb", nullable: false, comment: ""),
                    ChangeNote = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true, comment: ""),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    Remark = table.Column<string>(type: "text", nullable: true, comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentVersion", x => x.Id);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "DownloadItem",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false, comment: ""),
                    Extension = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false, comment: ""),
                    Size = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    bucket = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false, comment: ""),
                    key = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false, comment: ""),
                    StorageType = table.Column<int>(type: "integer", nullable: false, comment: ""),
                    State = table.Column<int>(type: "integer", nullable: false, comment: ""),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    Remark = table.Column<string>(type: "text", nullable: true, comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DownloadItem", x => x.Id);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "Favorite",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    TargetType = table.Column<short>(type: "smallint", nullable: false, comment: ""),
                    KnowledgeBaseId = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    KnowledgeBaseEntryId = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    Remark = table.Column<string>(type: "text", nullable: true, comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favorite", x => x.Id);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false, comment: ""),
                    Type = table.Column<int>(type: "integer", nullable: false, comment: ""),
                    Size = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    Extension = table.Column<string>(type: "character varying(16)", maxLength: 16, nullable: false, comment: ""),
                    Url = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false, comment: ""),
                    Path = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false, comment: ""),
                    StorageType = table.Column<int>(type: "integer", nullable: false, comment: ""),
                    ContentType = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false, comment: ""),
                    Hash = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false, comment: ""),
                    OriginalId = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    State = table.Column<int>(type: "integer", nullable: false, comment: ""),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    Remark = table.Column<string>(type: "text", nullable: true, comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.Id);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "GlobalSettings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AppId = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    Key = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false, comment: ""),
                    ConfigurationJson = table.Column<string>(type: "text", nullable: true, comment: ""),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    Remark = table.Column<string>(type: "text", nullable: true, comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlobalSettings", x => x.Id);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "KnowledgeBase",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TeamId = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false, comment: ""),
                    Slug = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false, comment: ""),
                    Description = table.Column<string>(type: "text", nullable: true, comment: ""),
                    CoverUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true, comment: ""),
                    Visibility = table.Column<short>(type: "smallint", nullable: false, comment: ""),
                    Settings = table.Column<string>(type: "jsonb", nullable: true, comment: ""),
                    ArchivedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: ""),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    Remark = table.Column<string>(type: "text", nullable: true, comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnowledgeBase", x => x.Id);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "KnowledgeBaseEntry",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    KnowledgeBaseId = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    ParentId = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    EntryType = table.Column<short>(type: "smallint", nullable: false, comment: ""),
                    Title = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false, comment: ""),
                    Slug = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false, comment: ""),
                    SortOrder = table.Column<int>(type: "integer", nullable: false, comment: ""),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    Remark = table.Column<string>(type: "text", nullable: true, comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnowledgeBaseEntry", x => x.Id);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "KnowledgeBaseMember",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    KnowledgeBaseId = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    UserId = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    Role = table.Column<short>(type: "smallint", nullable: false, comment: ""),
                    InvitedByUserId = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    Remark = table.Column<string>(type: "text", nullable: true, comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KnowledgeBaseMember", x => x.Id);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false, comment: ""),
                    Code = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false, comment: ""),
                    ParentId = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    Type = table.Column<int>(type: "integer", nullable: false, comment: ""),
                    PlatformType = table.Column<int>(type: "integer", nullable: false, comment: ""),
                    Icon = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true, comment: ""),
                    IconType = table.Column<int>(type: "integer", nullable: false, comment: ""),
                    ActiveIcon = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true, comment: ""),
                    ActiveIconType = table.Column<int>(type: "integer", nullable: false, comment: ""),
                    Url = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true, comment: ""),
                    Order = table.Column<int>(type: "integer", nullable: false, comment: ""),
                    IsVisible = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    IsExternalLink = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    IdSequences = table.Column<string>(type: "text", nullable: false, comment: ""),
                    SystemId = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    Remark = table.Column<string>(type: "text", nullable: true, comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menu_Menu_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    Type = table.Column<short>(type: "smallint", nullable: false, comment: ""),
                    Title = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true, comment: ""),
                    Body = table.Column<string>(type: "text", nullable: true, comment: ""),
                    Payload = table.Column<string>(type: "jsonb", nullable: true, comment: ""),
                    ReadAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: ""),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    Remark = table.Column<string>(type: "text", nullable: true, comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.Id);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "OperationLog",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IpAddress = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true, comment: ""),
                    LogType = table.Column<int>(type: "integer", nullable: false, comment: ""),
                    UserAgent = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true, comment: ""),
                    OperationContent = table.Column<string>(type: "text", nullable: false, comment: ""),
                    MenuCode = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false, comment: ""),
                    ErrorTracker = table.Column<string>(type: "text", nullable: true, comment: ""),
                    OperationMenu = table.Column<string>(type: "text", nullable: false, comment: ""),
                    Method = table.Column<string>(type: "text", nullable: false, comment: ""),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    Remark = table.Column<string>(type: "text", nullable: true, comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OperationLog", x => x.Id);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "Region",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false, comment: ""),
                    ShortName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false, comment: ""),
                    Code = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false, comment: ""),
                    ParentId = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    Level = table.Column<int>(type: "integer", nullable: false, comment: ""),
                    Order = table.Column<int>(type: "integer", nullable: false, comment: ""),
                    IdSequences = table.Column<string>(type: "text", nullable: false, comment: ""),
                    IsEnable = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    Remark = table.Column<string>(type: "text", nullable: true, comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.Id);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false, comment: ""),
                    Platforms = table.Column<int>(type: "integer", nullable: false, comment: ""),
                    Code = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false, comment: ""),
                    IsSystem = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    Order = table.Column<int>(type: "integer", nullable: false, comment: ""),
                    IsEnable = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    SystemId = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    Remark = table.Column<string>(type: "text", nullable: true, comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "ScheduleTask",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false, comment: ""),
                    Code = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true, comment: ""),
                    IsEnable = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    Expression = table.Column<string>(type: "text", nullable: true, comment: ""),
                    NextExecuteTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    LastExecuteTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleTask", x => x.Id);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "SeedDataTask",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LastWriteTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false, comment: ""),
                    Code = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true, comment: ""),
                    IsEnable = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    ExecuteTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    ExecuteStatus = table.Column<int>(type: "integer", nullable: false, comment: ""),
                    ConfigPath = table.Column<string>(type: "text", nullable: true, comment: ""),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeedDataTask", x => x.Id);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "ShareLink",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TokenHash = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false, comment: ""),
                    TargetType = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false, comment: ""),
                    KnowledgeBaseId = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    KnowledgeBaseEntryId = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    PasswordHash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true, comment: ""),
                    ExpireAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: ""),
                    Settings = table.Column<string>(type: "jsonb", nullable: true, comment: ""),
                    CreatedByUserId = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    RevokedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true, comment: ""),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    Remark = table.Column<string>(type: "text", nullable: true, comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShareLink", x => x.Id);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<short>(type: "smallint", nullable: false, comment: ""),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false, comment: ""),
                    Slug = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false, comment: ""),
                    Description = table.Column<string>(type: "text", nullable: true, comment: ""),
                    AvatarUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true, comment: ""),
                    OwnerUserId = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    Settings = table.Column<string>(type: "jsonb", nullable: true, comment: ""),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    Remark = table.Column<string>(type: "text", nullable: true, comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "TeamMember",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TeamId = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    UserId = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    Role = table.Column<short>(type: "smallint", nullable: false, comment: ""),
                    InvitedByUserId = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    JoinedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    Remark = table.Column<string>(type: "text", nullable: true, comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeamMember", x => x.Id);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Mobile = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true, comment: ""),
                    RealName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true, comment: ""),
                    UserName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false, comment: ""),
                    NickName = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true, comment: ""),
                    Password = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false, comment: ""),
                    PasswordSalt = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false, comment: ""),
                    IsEnable = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    Gender = table.Column<int>(type: "integer", nullable: false, comment: ""),
                    Avatar = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true, comment: ""),
                    Email = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false, comment: ""),
                    LastLoginTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    SignatureUrl = table.Column<string>(type: "character varying(512)", maxLength: 512, nullable: true, comment: ""),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    Remark = table.Column<string>(type: "text", nullable: true, comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "MenuResource",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MenuId = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    ApiResourceId = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    Remark = table.Column<string>(type: "text", nullable: true, comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuResource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuResource_ApiResource_ApiResourceId",
                        column: x => x.ApiResourceId,
                        principalTable: "ApiResource",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MenuResource_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    MenuId = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    DataRange = table.Column<int>(type: "integer", nullable: false, comment: ""),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    Remark = table.Column<string>(type: "text", nullable: true, comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permission_Menu_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Permission_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "ScheduleTaskRecord",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ScheduleTaskId = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    IsSuccess = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    ErrorMessage = table.Column<string>(type: "text", nullable: true, comment: ""),
                    ExecuteStartTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    ExpressionTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    ExecuteEndTime = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleTaskRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScheduleTaskRecord_ScheduleTask_ScheduleTaskId",
                        column: x => x.ScheduleTaskId,
                        principalTable: "ScheduleTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "UserDepartment",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    DepartmentId = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    Remark = table.Column<string>(type: "text", nullable: true, comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDepartment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserDepartment_Region_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Region",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserDepartment_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    RoleId = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    Remark = table.Column<string>(type: "text", nullable: true, comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "");

            migrationBuilder.CreateTable(
                name: "UserToken",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false, comment: "")
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false, comment: ""),
                    Token = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false, comment: ""),
                    TokenHash = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false, comment: ""),
                    RefreshToken = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false, comment: ""),
                    ExpirationDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    IpAddress = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false, comment: ""),
                    UserAgent = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: false, comment: ""),
                    PlatformType = table.Column<int>(type: "integer", nullable: false, comment: ""),
                    LoginMethodType = table.Column<int>(type: "integer", nullable: false, comment: ""),
                    RefreshTokenIsAvailable = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    LoginType = table.Column<int>(type: "integer", nullable: false, comment: ""),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, comment: ""),
                    CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    CreatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    UpdatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, comment: ""),
                    UpdatedBy = table.Column<long>(type: "bigint", nullable: true, comment: ""),
                    Remark = table.Column<string>(type: "text", nullable: true, comment: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserToken_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_DocumentId",
                table: "Attachment",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_KnowledgeBaseId",
                table: "Attachment",
                column: "KnowledgeBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Attachment_UploaderId",
                table: "Attachment",
                column: "UploaderId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_DocumentId",
                table: "Comment",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_ParentId",
                table: "Comment",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentMention_CommentId_MentionedUserId",
                table: "CommentMention",
                columns: new[] { "CommentId", "MentionedUserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommentMention_MentionedUserId",
                table: "CommentMention",
                column: "MentionedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_CurrentVersionId",
                table: "Document",
                column: "CurrentVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_KnowledgeBaseEntryId",
                table: "Document",
                column: "KnowledgeBaseEntryId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Document_KnowledgeBaseId",
                table: "Document",
                column: "KnowledgeBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentPermission_DocumentId_UserId",
                table: "DocumentPermission",
                columns: new[] { "DocumentId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DocumentVersion_DocumentId",
                table: "DocumentVersion",
                column: "DocumentId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentVersion_DocumentId_VersionNo",
                table: "DocumentVersion",
                columns: new[] { "DocumentId", "VersionNo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_UserId",
                table: "Favorite",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_UserId_KnowledgeBaseEntryId",
                table: "Favorite",
                columns: new[] { "UserId", "KnowledgeBaseEntryId" });

            migrationBuilder.CreateIndex(
                name: "IX_Favorite_UserId_KnowledgeBaseId",
                table: "Favorite",
                columns: new[] { "UserId", "KnowledgeBaseId" });

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeBase_TeamId",
                table: "KnowledgeBase",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeBase_TeamId_Slug",
                table: "KnowledgeBase",
                columns: new[] { "TeamId", "Slug" },
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeBaseEntry_KnowledgeBaseId_ParentId",
                table: "KnowledgeBaseEntry",
                columns: new[] { "KnowledgeBaseId", "ParentId" });

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeBaseEntry_KnowledgeBaseId_ParentId_Slug",
                table: "KnowledgeBaseEntry",
                columns: new[] { "KnowledgeBaseId", "ParentId", "Slug" },
                unique: true,
                filter: "\"IsDeleted\" = false");

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeBaseMember_KnowledgeBaseId_UserId",
                table: "KnowledgeBaseMember",
                columns: new[] { "KnowledgeBaseId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_KnowledgeBaseMember_UserId",
                table: "KnowledgeBaseMember",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_ParentId",
                table: "Menu",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuResource_ApiResourceId",
                table: "MenuResource",
                column: "ApiResourceId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuResource_MenuId",
                table: "MenuResource",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Notification_UserId_ReadAt_CreatedAt",
                table: "Notification",
                columns: new[] { "UserId", "ReadAt", "CreatedAt" });

            migrationBuilder.CreateIndex(
                name: "IX_Permission_MenuId",
                table: "Permission",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Permission_RoleId",
                table: "Permission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_ScheduleTaskRecord_ScheduleTaskId",
                table: "ScheduleTaskRecord",
                column: "ScheduleTaskId");

            migrationBuilder.CreateIndex(
                name: "IX_ShareLink_TokenHash",
                table: "ShareLink",
                column: "TokenHash",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Team_OwnerUserId",
                table: "Team",
                column: "OwnerUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_Slug",
                table: "Team",
                column: "Slug",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Team_Type_OwnerUserId",
                table: "Team",
                columns: new[] { "Type", "OwnerUserId" });

            migrationBuilder.CreateIndex(
                name: "IX_TeamMember_TeamId_UserId",
                table: "TeamMember",
                columns: new[] { "TeamId", "UserId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TeamMember_UserId",
                table: "TeamMember",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDepartment_DepartmentId",
                table: "UserDepartment",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserDepartment_UserId",
                table: "UserDepartment",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleId",
                table: "UserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserId",
                table: "UserRole",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserToken_UserId",
                table: "UserToken",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppConfig");

            migrationBuilder.DropTable(
                name: "AppEventConfig");

            migrationBuilder.DropTable(
                name: "AppNotificationConfig");

            migrationBuilder.DropTable(
                name: "AppWebhookConfig");

            migrationBuilder.DropTable(
                name: "AsyncTask");

            migrationBuilder.DropTable(
                name: "Attachment");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "CommentMention");

            migrationBuilder.DropTable(
                name: "Document");

            migrationBuilder.DropTable(
                name: "DocumentPermission");

            migrationBuilder.DropTable(
                name: "DocumentVersion");

            migrationBuilder.DropTable(
                name: "DownloadItem");

            migrationBuilder.DropTable(
                name: "Favorite");

            migrationBuilder.DropTable(
                name: "File");

            migrationBuilder.DropTable(
                name: "GlobalSettings");

            migrationBuilder.DropTable(
                name: "KnowledgeBase");

            migrationBuilder.DropTable(
                name: "KnowledgeBaseEntry");

            migrationBuilder.DropTable(
                name: "KnowledgeBaseMember");

            migrationBuilder.DropTable(
                name: "MenuResource");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropTable(
                name: "OperationLog");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "ScheduleTaskRecord");

            migrationBuilder.DropTable(
                name: "SeedDataTask");

            migrationBuilder.DropTable(
                name: "ShareLink");

            migrationBuilder.DropTable(
                name: "Team");

            migrationBuilder.DropTable(
                name: "TeamMember");

            migrationBuilder.DropTable(
                name: "UserDepartment");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "UserToken");

            migrationBuilder.DropTable(
                name: "ApiResource");

            migrationBuilder.DropTable(
                name: "Menu");

            migrationBuilder.DropTable(
                name: "ScheduleTask");

            migrationBuilder.DropTable(
                name: "Region");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}

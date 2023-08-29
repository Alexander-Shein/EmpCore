using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EmpCore.Persistence.EntityFrameworkCore.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
IF (NOT EXISTS (SELECT * 
	FROM INFORMATION_SCHEMA.TABLES 
	WHERE TABLE_SCHEMA = 'dbo' 
	AND  TABLE_NAME = 'BlogPost'))
BEGIN
	CREATE TABLE [dbo].[BlogPost]
	(
		[Id]					UNIQUEIDENTIFIER NOT NULL,
		[AuthorId]				VARCHAR(128) NOT NULL,
		[FeedbackEmailAddress]	NVARCHAR(256) NOT NULL CONSTRAINT [DF_BlogPost_FeedbackEmailAddress] DEFAULT '',
		[Title]					NVARCHAR(1024) NOT NULL		CONSTRAINT [DF_BlogPost_Title] DEFAULT '',
		[Content]				NVARCHAR(MAX) NOT NULL		CONSTRAINT [DF_BlogPost_Content] DEFAULT '',
		[PublishStatus]			NVARCHAR(24) NOT NULL		CONSTRAINT [DF_BlogPost_PublishStatus] DEFAULT '',
		[PublishDateTime]		DATETIME2 NULL,
		[IsDeleted]				BIT NOT NULL				CONSTRAINT [DF_BlogPost_IsDeleted] DEFAULT 0,
		[CreatedAt]				DATETIME2 NOT NULL			CONSTRAINT [DF_BlogPost_CreatedAt] DEFAULT GETDATE(),
		[UpdatedAt]				DATETIME2 NOT NULL			CONSTRAINT [DF_BlogPost_UpdatedAt] DEFAULT GETDATE(),

		CONSTRAINT [PK_BlogPost_Id] PRIMARY KEY (Id)
	);
END

IF (NOT EXISTS (SELECT * 
	FROM INFORMATION_SCHEMA.TABLES 
	WHERE TABLE_SCHEMA = 'dbo' 
	AND  TABLE_NAME = 'EmbeddedResource'))
BEGIN
CREATE TABLE [dbo].[EmbeddedResource]
(
	[Id]				INT IDENTITY(1,1) NOT NULL,
	[BlogPostId]		UNIQUEIDENTIFIER NOT NULL,
	[Url]				NVARCHAR(2048) NOT NULL		CONSTRAINT [DF_EmbeddedResource_Url] DEFAULT '',
	[Caption]			NVARCHAR(MAX) NOT NULL		CONSTRAINT [DF_EmbeddedResource_Caption] DEFAULT '',

	CONSTRAINT [PK_EmbeddedResource_Id] PRIMARY KEY (Id),
	CONSTRAINT [FK_EmbeddedResource_BlogPostId_BlogPost_Id] FOREIGN KEY ([BlogPostId]) REFERENCES [dbo].[BlogPost]([Id])
);
END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
DROP TABLE IF EXISTS [dbo].[EmbeddedResource];
DROP TABLE IF EXISTS [dbo].[BlogPost];");
        }
    }
}

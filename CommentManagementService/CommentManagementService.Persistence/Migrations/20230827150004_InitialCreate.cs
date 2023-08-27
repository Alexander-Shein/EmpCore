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
                 AND  TABLE_NAME = 'PublishedBlogPost'))
BEGIN
	CREATE TABLE [dbo].[PublishedBlogPost]
	(
		[Id]				UNIQUEIDENTIFIER NOT NULL

		CONSTRAINT [PK_PublishedBlogPost_Id] PRIMARY KEY (Id)
	);
END

IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Commentor'))
BEGIN
	CREATE TABLE [dbo].[Commentor]
	(
		[Id]				UNIQUEIDENTIFIER NOT NULL,
		[UserName] NVARCHAR(256) NOT NULL CONSTRAINT [DF_Commentor_UserName] DEFAULT '',

		CONSTRAINT [PK_Commentor_Id] PRIMARY KEY (Id)
	);
END

IF (NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Comment'))
BEGIN
	CREATE TABLE [dbo].[Comment]
	(
		[Id]					BIGINT IDENTITY(1,1) NOT NULL,
		[PublishedBlogPostId]	UNIQUEIDENTIFIER NOT NULL,
		[CommentorId]			UNIQUEIDENTIFIER NOT NULL,
		[Message]				NVARCHAR(1024) NOT NULL		CONSTRAINT [DF_Comment_Message] DEFAULT '',
		[CreatedAt]				DATETIME2 NOT NULL			CONSTRAINT [DF_Comment_CreatedAt] DEFAULT GETDATE(),
		[UpdatedAt]				DATETIME2 NOT NULL			CONSTRAINT [DF_Comment_UpdatedAt] DEFAULT GETDATE(),

		CONSTRAINT [PK_Comment_Id] PRIMARY KEY (Id),
	CONSTRAINT [FK_Commentor_PublishedBlogPostId_PublishedBlogPost_Id] FOREIGN KEY ([PublishedBlogPostId]) REFERENCES [dbo].[PublishedBlogPost]([Id]),
	CONSTRAINT [FK_Commentor_CommentorId_Commentor_Id] FOREIGN KEY ([CommentorId]) REFERENCES [dbo].[Commentor]([Id])
	);
END");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
DROP TABLE IF EXISTS [dbo].[PublishedBlogPost];
DROP TABLE IF EXISTS [dbo].[Commentor];
DROP TABLE IF EXISTS [dbo].[Comment];");
        }
    }
}

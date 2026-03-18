-- ============================================================
-- Run this ONCE on your AFRILEARN database.
-- Open SQL Server Management Studio or Azure Data Studio,
-- connect to your server, select database AFRILEARN, then execute.
-- ============================================================

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'TopicMaterials')
BEGIN
    CREATE TABLE [dbo].[TopicMaterials] (
        [Id]               INT             IDENTITY (1, 1) NOT NULL,
        [TopicId]           INT             NOT NULL,
        [Title]             NVARCHAR (200)  NOT NULL,
        [ResourceType]      NVARCHAR (50)   NOT NULL,
        [FilePath]          NVARCHAR (500)  NULL,
        [ExternalUrl]       NVARCHAR (1000) NULL,
        [FileName]          NVARCHAR (255)  NULL,
        [ContentType]       NVARCHAR (100)  NULL,
        [FileSizeBytes]     BIGINT          NULL,
        [UploadedByUserId]  INT             NOT NULL,
        [CreatedDate]       DATETIME        NULL DEFAULT (getdate()),
        [ModifiedDate]      DATETIME        NULL,
        CONSTRAINT [PK_TopicMaterials] PRIMARY KEY CLUSTERED ([Id] ASC),
        CONSTRAINT [FK_TopicMaterials_Topics] FOREIGN KEY ([TopicId]) REFERENCES [dbo].[Topics] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_TopicMaterials_Users] FOREIGN KEY ([UploadedByUserId]) REFERENCES [dbo].[Users] ([Id])
    );

    CREATE NONCLUSTERED INDEX [IX_TopicMaterials_TopicId] ON [dbo].[TopicMaterials]([TopicId] ASC);
    PRINT 'Table TopicMaterials created.';
END
ELSE
    PRINT 'Table TopicMaterials already exists.';

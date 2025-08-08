CREATE TABLE [dbo].[ClientArchive]
(
[id] [int] NOT NULL,
[Name] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[PhoneNumber] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[SiteUrl] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Email] [nvarchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Created] [date] NOT NULL,
[CreatedBy] [int] NULL,
[ActionDate] [datetime2] (3) NOT NULL,
[ActionType] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ActionBy] [int] NOT NULL,
[ActionId] [int] NOT NULL IDENTITY(1, 1)
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ClientArchive] ADD CONSTRAINT [PK_ClientArchive] PRIMARY KEY CLUSTERED ([ActionId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ClientArchive] ADD CONSTRAINT [FK_ClientArchive_Admin] FOREIGN KEY ([ActionBy]) REFERENCES [dbo].[Admin] ([id])
GO
ALTER TABLE [dbo].[ClientArchive] ADD CONSTRAINT [FK_ClientArchive_Admin1] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[Admin] ([id])
GO

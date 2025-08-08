CREATE TABLE [dbo].[LinkedinConnectionActionType]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[LinkedinConnectionActionType] ADD CONSTRAINT [PK_LinkedinConnectionActionType] PRIMARY KEY CLUSTERED ([id]) ON [PRIMARY]
GO

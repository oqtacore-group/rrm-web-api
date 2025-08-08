CREATE TABLE [dbo].[LinkedinConnectionAction]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[Date] [datetime2] (3) NOT NULL,
[ActionId] [int] NOT NULL,
[ProfileUrl] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[AdminId] [int] NOT NULL,
[OccupationText] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[MessageText] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LastUpdate] [datetime2] (3) NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[LinkedinConnectionAction] ADD CONSTRAINT [PK_LinkedinConnectionAction] PRIMARY KEY CLUSTERED ([id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[LinkedinConnectionAction] ADD CONSTRAINT [FK_LinkedinConnectionAction_LinkedinAdmin] FOREIGN KEY ([AdminId]) REFERENCES [dbo].[LinkedinAdmin] ([id])
GO
ALTER TABLE [dbo].[LinkedinConnectionAction] ADD CONSTRAINT [FK_LinkedinConnectionAction_LinkedinConnectionActionType] FOREIGN KEY ([ActionId]) REFERENCES [dbo].[LinkedinConnectionActionType] ([id])
GO

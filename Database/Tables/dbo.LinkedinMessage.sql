CREATE TABLE [dbo].[LinkedinMessage]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[UserId] [int] NOT NULL,
[Date] [datetime2] (3) NOT NULL,
[Text] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Name] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ProfileUrl] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[OrderId] [int] NOT NULL,
[Amocrm_added] [bit] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[LinkedinMessage] ADD CONSTRAINT [PK_LinkedinMessage] PRIMARY KEY CLUSTERED ([id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[LinkedinMessage] ADD CONSTRAINT [IX_LinkedinMessage] UNIQUE NONCLUSTERED ([OrderId], [UserId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[LinkedinMessage] ADD CONSTRAINT [FK_LinkedinMessage_LinkedinUser] FOREIGN KEY ([UserId]) REFERENCES [dbo].[LinkedinUser] ([id])
GO

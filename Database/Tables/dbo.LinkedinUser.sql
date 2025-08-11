CREATE TABLE [dbo].[LinkedinUser]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[DialogUrl] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ProfileUrl] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Name] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Date] [datetime2] (3) NOT NULL,
[Connected] [bit] NOT NULL,
[DateDisconnect] [datetime2] (3) NULL,
[AdminId] [int] NOT NULL,
[Amocrm_added] [bit] NULL,
[Amocrm_Id] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[LinkedinUser] ADD CONSTRAINT [PK_LinkedinUser] PRIMARY KEY CLUSTERED ([id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[LinkedinUser] ADD CONSTRAINT [IX_LinkedinUser] UNIQUE NONCLUSTERED ([DialogUrl], [ProfileUrl]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[LinkedinUser] ADD CONSTRAINT [FK_LinkedinUser_LinkedinAdmin] FOREIGN KEY ([AdminId]) REFERENCES [dbo].[LinkedinAdmin] ([id])
GO

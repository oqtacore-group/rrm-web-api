CREATE TABLE [dbo].[RegisteredChat]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[ChatId] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Name] [nvarchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Approved] [bit] NOT NULL,
[DateTime] [datetime2] (3) NOT NULL,
[Description] [nvarchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[TelegramBotName] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[TelegramBotId] [bigint] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[RegisteredChat] ADD CONSTRAINT [PK_RegisteredChat] PRIMARY KEY CLUSTERED ([id]) ON [PRIMARY]
GO

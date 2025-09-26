CREATE TABLE [dbo].[Log]
(
[Id] [int] NOT NULL IDENTITY(1, 1),
[Application] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Logged] [datetime2] (3) NOT NULL,
[Level] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Message] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Logger] [nvarchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Callsite] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Exception] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LogonUserEmail] [nvarchar] (255) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Log] ADD CONSTRAINT [PK_dbo.Log] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO

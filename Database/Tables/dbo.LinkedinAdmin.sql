CREATE TABLE [dbo].[LinkedinAdmin]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[AdminKey] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ProfileUrl] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[LastUpdate] [datetime2] (3) NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[LinkedinAdmin] ADD CONSTRAINT [PK_LinkedinAdmin] PRIMARY KEY CLUSTERED ([id]) ON [PRIMARY]
GO

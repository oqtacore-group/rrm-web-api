CREATE TABLE [dbo].[Client]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PhoneNumber] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[SiteUrl] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Email] [nvarchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Created] [date] NOT NULL,
[CreatedBy] [int] NULL,
[IsDeleted] [bit] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Client] ADD CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED ([id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Client] ADD CONSTRAINT [FK_Client_Admin] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[Admin] ([id])
GO

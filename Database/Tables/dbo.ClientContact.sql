CREATE TABLE [dbo].[ClientContact]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[ClientId] [int] NOT NULL,
[PhoneNumber] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Name] [nvarchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Email] [nvarchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ContactDataId] [int] NOT NULL,
[CreatedBy] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ClientContact] ADD CONSTRAINT [PK_ClientContact] PRIMARY KEY CLUSTERED ([id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ClientContact] ADD CONSTRAINT [FK_ClientContact_Admin] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[Admin] ([id])
GO
ALTER TABLE [dbo].[ClientContact] ADD CONSTRAINT [FK_ClientContact_Client] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Client] ([id])
GO
ALTER TABLE [dbo].[ClientContact] ADD CONSTRAINT [FK_ClientContact_ContactData] FOREIGN KEY ([ContactDataId]) REFERENCES [dbo].[ContactData] ([id])
GO

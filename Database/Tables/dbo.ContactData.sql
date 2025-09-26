CREATE TABLE [dbo].[ContactData]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[Linkedin] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Vkontakte] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PhoneNumber] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[SecondPhoneNumber] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Telegram] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Skype] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Email] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Location] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CreatedBy] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContactData] ADD CONSTRAINT [PK_ContactData] PRIMARY KEY CLUSTERED ([id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContactData] ADD CONSTRAINT [FK_ContactData_Admin] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[Admin] ([id])
GO

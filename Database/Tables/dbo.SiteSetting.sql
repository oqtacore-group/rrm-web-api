CREATE TABLE [dbo].[SiteSetting]
(
[Name] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Value] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[SiteSetting] ADD CONSTRAINT [PK_SiteSetting] PRIMARY KEY CLUSTERED ([Name]) ON [PRIMARY]
GO

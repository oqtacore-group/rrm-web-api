CREATE TABLE [dbo].[GoogleCalendarSetting]
(
[Name] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Value] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[GoogleCalendarSetting] ADD CONSTRAINT [PK_GoogleCalendarSetting] PRIMARY KEY CLUSTERED ([Name]) ON [PRIMARY]
GO

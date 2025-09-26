CREATE TABLE [dbo].[GoogleCalendarEvent]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[status] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[kind] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[starttime] [datetime2] (3) NULL,
[endtime] [datetime2] (3) NULL,
[created] [datetime2] (3) NULL,
[updated] [datetime2] (3) NULL,
[location] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[UIID] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[summary] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[user_email] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[event_id] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[completed] [bit] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[GoogleCalendarEvent] ADD CONSTRAINT [PK_GoogleCalendarEvent] PRIMARY KEY CLUSTERED ([id]) ON [PRIMARY]
GO

CREATE TABLE [dbo].[MeetupMember]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[member_id] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Name] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Date] [datetime2] (3) NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MeetupMember] ADD CONSTRAINT [PK_MeetupMember] PRIMARY KEY CLUSTERED ([id]) ON [PRIMARY]
GO

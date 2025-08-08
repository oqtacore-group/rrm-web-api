CREATE TABLE [dbo].[CandidateEventType]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CandidateEventType] ADD CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED ([id]) ON [PRIMARY]
GO

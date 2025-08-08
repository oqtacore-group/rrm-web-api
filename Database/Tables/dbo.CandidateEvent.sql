CREATE TABLE [dbo].[CandidateEvent]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[Date] [datetime2] (3) NOT NULL,
[CandidateId] [int] NOT NULL,
[Caption] [nvarchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[TypeId] [int] NOT NULL,
[Completed] [bit] NULL,
[CreatedBy] [int] NOT NULL,
[ZoomLink] [nvarchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ReminderSent] [bit] NULL,
[HashCode] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ReminderEarlySent] [bit] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CandidateEvent] ADD CONSTRAINT [PK_CandidateEvent] PRIMARY KEY CLUSTERED ([id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CandidateEvent] ADD CONSTRAINT [FK_CandidateEvent_Admin] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[Admin] ([id])
GO
ALTER TABLE [dbo].[CandidateEvent] ADD CONSTRAINT [FK_CandidateEvent_Candidate] FOREIGN KEY ([CandidateId]) REFERENCES [dbo].[Candidate] ([id])
GO
ALTER TABLE [dbo].[CandidateEvent] ADD CONSTRAINT [FK_CandidateEvent_CandidateEventType] FOREIGN KEY ([TypeId]) REFERENCES [dbo].[CandidateEventType] ([id])
GO

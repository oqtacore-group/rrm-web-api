CREATE TABLE [dbo].[ParsedLinkedinCandidateResume]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[ParsedId] [int] NOT NULL,
[Title] [nvarchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CompanyName] [nvarchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[StartDate] [date] NULL,
[EndDate] [date] NULL,
[LocationName] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ParsedLinkedinCandidateResume] ADD CONSTRAINT [PK_ParsedLinkedinCandidateResume] PRIMARY KEY CLUSTERED ([id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ParsedLinkedinCandidateResume] ADD CONSTRAINT [fk_parsed_linkedin_resume_id] FOREIGN KEY ([ParsedId]) REFERENCES [dbo].[ParsedLinkedinCandidateData] ([id]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ParsedLinkedinCandidateResume] ADD CONSTRAINT [FK_ParsedLinkedinCandidateResume_ParsedLinkedinCandidateData] FOREIGN KEY ([ParsedId]) REFERENCES [dbo].[ParsedLinkedinCandidateData] ([id])
GO

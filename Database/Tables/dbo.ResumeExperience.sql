CREATE TABLE [dbo].[ResumeExperience]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[CandidateId] [int] NOT NULL,
[PositionName] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CompanyName] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[StartDate] [date] NOT NULL,
[EndDate] [date] NOT NULL,
[CurrentJob] [bit] NOT NULL,
[Description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ResumeExperience] ADD CONSTRAINT [PK_ResumeExperience] PRIMARY KEY CLUSTERED ([id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ResumeExperience] ADD CONSTRAINT [fk_resume_exp_id] FOREIGN KEY ([CandidateId]) REFERENCES [dbo].[Candidate] ([id]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ResumeExperience] ADD CONSTRAINT [FK_ResumeExperience_Candidate] FOREIGN KEY ([CandidateId]) REFERENCES [dbo].[Candidate] ([id])
GO

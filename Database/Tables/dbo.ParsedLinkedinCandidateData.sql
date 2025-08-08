CREATE TABLE [dbo].[ParsedLinkedinCandidateData]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[CandidateId] [int] NOT NULL,
[DateBirth] [nvarchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[FirstName] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LastName] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Position] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[About] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LocationName] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LanguageSummary] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[SkillSummary] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IndustrySummary] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[DateParsed] [date] NOT NULL,
[Active] [bit] NOT NULL,
[Profile_url] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ParsedLinkedinCandidateData] ADD CONSTRAINT [PK_ParsedLinkedinUserData] PRIMARY KEY CLUSTERED ([id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ParsedLinkedinCandidateData] ADD CONSTRAINT [fk_parsed_linkedin_profile_id] FOREIGN KEY ([CandidateId]) REFERENCES [dbo].[Candidate] ([id]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ParsedLinkedinCandidateData] ADD CONSTRAINT [FK_ParsedLinkedinCandidateData_Candidate] FOREIGN KEY ([CandidateId]) REFERENCES [dbo].[Candidate] ([id])
GO

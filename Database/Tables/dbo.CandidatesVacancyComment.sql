CREATE TABLE [dbo].[CandidatesVacancyComment]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[CandidateId] [int] NOT NULL,
[VacancyId] [int] NOT NULL,
[DateAdded] [date] NOT NULL,
[Note] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CandidatesVacancyComment] ADD CONSTRAINT [PK_CandidatesVacancyComment] PRIMARY KEY CLUSTERED ([id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CandidatesVacancyComment] ADD CONSTRAINT [FK_CandidatesVacancyComment_Candidate] FOREIGN KEY ([CandidateId]) REFERENCES [dbo].[Candidate] ([id])
GO
ALTER TABLE [dbo].[CandidatesVacancyComment] ADD CONSTRAINT [FK_CandidatesVacancyComment_Vacancy] FOREIGN KEY ([VacancyId]) REFERENCES [dbo].[Vacancy] ([id])
GO

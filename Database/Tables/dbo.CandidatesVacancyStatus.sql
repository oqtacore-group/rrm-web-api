CREATE TABLE [dbo].[CandidatesVacancyStatus]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[CandidateId] [int] NOT NULL,
[VacancyId] [int] NOT NULL,
[VacancyStatusId] [int] NOT NULL,
[DateAdded] [date] NOT NULL,
[Note] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CreatedBy] [int] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CandidatesVacancyStatus] ADD CONSTRAINT [PK_CandidatesVacancyStatus] PRIMARY KEY CLUSTERED ([id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CandidatesVacancyStatus] ADD CONSTRAINT [fk_candidate_vacancy_status_id] FOREIGN KEY ([CandidateId]) REFERENCES [dbo].[Candidate] ([id]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CandidatesVacancyStatus] ADD CONSTRAINT [FK_CandidatesVacancyStatus_Admin] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[Admin] ([id])
GO
ALTER TABLE [dbo].[CandidatesVacancyStatus] ADD CONSTRAINT [FK_CandidatesVacancyStatus_Candidate] FOREIGN KEY ([CandidateId]) REFERENCES [dbo].[Candidate] ([id])
GO
ALTER TABLE [dbo].[CandidatesVacancyStatus] ADD CONSTRAINT [FK_CandidatesVacancyStatus_Vacancy] FOREIGN KEY ([VacancyId]) REFERENCES [dbo].[Vacancy] ([id])
GO
ALTER TABLE [dbo].[CandidatesVacancyStatus] ADD CONSTRAINT [FK_CandidatesVacancyStatus_VacancyStatusType] FOREIGN KEY ([VacancyStatusId]) REFERENCES [dbo].[VacancyStatusType] ([id])
GO
ALTER TABLE [dbo].[CandidatesVacancyStatus] ADD CONSTRAINT [fk_vacancy_vacancy_status_id] FOREIGN KEY ([VacancyId]) REFERENCES [dbo].[Vacancy] ([id]) ON DELETE CASCADE
GO

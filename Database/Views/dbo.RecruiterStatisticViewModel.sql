SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE VIEW [dbo].[RecruiterStatisticViewModel]
AS
SELECT        dbo.CandidatesVacancyStatus.id, dbo.CandidatesVacancyStatus.CandidateId, dbo.CandidatesVacancyStatus.VacancyId, dbo.CandidatesVacancyStatus.VacancyStatusId, 
                         dbo.VacancyStatusType.Name AS StatusName, dbo.CandidatesVacancyStatus.DateAdded, dbo.CandidatesVacancyStatus.CreatedBy, dbo.RecruiterSuccessCandidateStateViewModel.Success, 
                         dbo.RecruiterSuccessCandidateStateViewModel.Fail, dbo.Admin.Name AS AdminName
FROM            dbo.CandidatesVacancyStatus INNER JOIN
                         dbo.RecruiterSuccessCandidateStateViewModel ON dbo.CandidatesVacancyStatus.CandidateId = dbo.RecruiterSuccessCandidateStateViewModel.CandidateId AND 
                         dbo.CandidatesVacancyStatus.VacancyId = dbo.RecruiterSuccessCandidateStateViewModel.VacancyId INNER JOIN
                         dbo.Admin ON dbo.Admin.id = dbo.CandidatesVacancyStatus.CreatedBy INNER JOIN
                         dbo.VacancyStatusType ON dbo.CandidatesVacancyStatus.VacancyStatusId = dbo.VacancyStatusType.id
GO

SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW [dbo].[RecruiterSuccessCandidateStateViewModel]
AS
SELECT        CAST(SUM(CAST(dbo.VacancyStatusType.CountSuccess AS int)) AS bit) AS Success, CAST(SUM(CAST(dbo.VacancyStatusType.CountFail AS int)) AS bit) AS Fail, dbo.CandidatesVacancyStatus.CandidateId, 
                         dbo.CandidatesVacancyStatus.VacancyId
FROM            dbo.CandidatesVacancyStatus INNER JOIN
                         dbo.VacancyStatusType ON dbo.CandidatesVacancyStatus.VacancyStatusId = dbo.VacancyStatusType.id
GROUP BY dbo.CandidatesVacancyStatus.CandidateId, dbo.CandidatesVacancyStatus.VacancyId
GO

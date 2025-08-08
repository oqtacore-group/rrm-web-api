SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW [dbo].[VacancyStatusListViewModel]
AS
SELECT        dbo.CandidatesVacancyStatus.id, dbo.CandidatesVacancyStatus.CandidateId, dbo.CandidatesVacancyStatus.VacancyId, dbo.CandidatesVacancyStatus.VacancyStatusId, 
                         dbo.VacancyStatusType.Name AS StatusName, dbo.CandidatesVacancyStatus.DateAdded, dbo.CandidatesVacancyStatus.Note, dbo.Vacancy.Name AS VacancyName, dbo.Client.Name AS ClientName, 
                         dbo.Client.id AS ClientId, 'status' AS rowType, dbo.Candidate.Name as CandidateName
FROM            dbo.CandidatesVacancyStatus INNER JOIN
                         dbo.Vacancy ON dbo.CandidatesVacancyStatus.VacancyId = dbo.Vacancy.id INNER JOIN
                         dbo.VacancyStatusType ON dbo.CandidatesVacancyStatus.VacancyStatusId = dbo.VacancyStatusType.id INNER JOIN
                         dbo.Client ON dbo.Vacancy.ClientId = dbo.Client.id INNER JOIN
                         dbo.Candidate ON dbo.CandidatesVacancyStatus.CandidateId = dbo.Candidate.id
UNION
SELECT        dbo.CandidatesVacancyComment.id, dbo.CandidatesVacancyComment.CandidateId, dbo.CandidatesVacancyComment.VacancyId, 0 AS VacancyStatusId, 'Comment' AS StatusName, 
                         dbo.CandidatesVacancyComment.DateAdded, dbo.CandidatesVacancyComment.Note, dbo.Vacancy.Name AS VacancyName, dbo.Client.Name AS ClientName, dbo.Client.id AS ClientId, 
						 'comment' AS rowType, dbo.Candidate.Name as CandidateName

FROM            dbo.CandidatesVacancyComment INNER JOIN
                         dbo.Vacancy ON dbo.CandidatesVacancyComment.VacancyId = dbo.Vacancy.id INNER JOIN
                         dbo.Client ON dbo.Vacancy.ClientId = dbo.Client.id INNER JOIN
                         dbo.Candidate ON dbo.CandidatesVacancyComment.CandidateId = dbo.Candidate.id
GO

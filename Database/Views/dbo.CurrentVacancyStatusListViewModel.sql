SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE VIEW [dbo].[CurrentVacancyStatusListViewModel]
AS
SELECT        VacancyStatusListViewModel.id, VacancyStatusListViewModel.CandidateId, VacancyStatusListViewModel.VacancyId, VacancyStatusListViewModel.VacancyStatusId, VacancyStatusListViewModel.StatusName, 
                         VacancyStatusListViewModel.DateAdded, VacancyStatusListViewModel.Note, VacancyStatusListViewModel.VacancyName, VacancyStatusListViewModel.ClientName, VacancyStatusListViewModel.ClientId, 
                         dbo.VacancyStatusType.CountSuccess, VacancyStatusListViewModel.CandidateName
FROM            (SELECT        id, CandidateId, VacancyId, VacancyStatusId, StatusName, DateAdded, Note, VacancyName, ClientName, ClientId, rowType, CandidateName
                          FROM            dbo.VacancyStatusListViewModel AS VacancyStatusListViewModel_2
                          WHERE        (VacancyStatusId > 0)) AS VacancyStatusListViewModel INNER JOIN
                         dbo.VacancyStatusType ON VacancyStatusListViewModel.VacancyStatusId = dbo.VacancyStatusType.id
WHERE        (VacancyStatusListViewModel.id = ANY
                             (SELECT        MAX(id) AS LastStatusId
                               FROM            dbo.VacancyStatusListViewModel AS VacancyStatusListViewModel_1
                               GROUP BY CandidateId, VacancyId))
GO

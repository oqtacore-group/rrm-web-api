SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE VIEW [dbo].[VacancyStateListViewModel]
AS
SELECT        ISNULL(dbo.Vacancy.id, 0) AS VacancyId, dbo.Vacancy.Name, dbo.Client.Name AS ClientName, ISNULL(SUM(CAST(dbo.CurrentVacancyStatusListViewModel.CountSuccess AS int)), 0) AS SuccessCount, 
                         dbo.Vacancy.WorkplaceNumber, COUNT(DISTINCT dbo.CurrentVacancyStatusListViewModel.CandidateId) AS CandidateCount, dbo.Client.id AS ClientId, dbo.Vacancy.CreatedBy
FROM            dbo.Vacancy INNER JOIN
                         dbo.Client ON dbo.Vacancy.ClientId = dbo.Client.id FULL OUTER JOIN
                         dbo.CurrentVacancyStatusListViewModel ON dbo.CurrentVacancyStatusListViewModel.VacancyId = dbo.Vacancy.id
GROUP BY dbo.Vacancy.id, dbo.Vacancy.Name, dbo.Client.Name, dbo.Vacancy.WorkplaceNumber, dbo.Client.id, dbo.Vacancy.CreatedBy
GO

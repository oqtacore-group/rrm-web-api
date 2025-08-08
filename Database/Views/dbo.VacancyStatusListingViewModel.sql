SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW [dbo].[VacancyStatusListingViewModel]
AS
/****** Script for SelectTopNRows command from SSMS  ******/ SELECT dbo.Vacancy.id AS VacancyId, dbo.VacancyStatusType.id AS StatusId, dbo.VacancyStatusType.Name, dbo.VacancyStatusType.CountSuccess, 
                         dbo.VacancyStatusType.OrderId, ISNULL
                             ((SELECT        COUNT(*) AS Expr1
                                 FROM            dbo.CurrentVacancyStatusListViewModel
                                 WHERE        (VacancyId = dbo.Vacancy.id) AND (VacancyStatusId = dbo.VacancyStatusType.id)), 0) AS CandidateCount
FROM            dbo.VacancyStatusType CROSS JOIN
                         dbo.Vacancy
UNION
SELECT        id, 0 AS StatusId, 'All' AS Name, Cast(0 as bit) AS CountSuccess, 0 AS OrderId, ISNULL
                             ((SELECT        COUNT(DISTINCT CandidateId) AS Expr1
                                 FROM            dbo.[CurrentVacancyStatusListViewModel]
                                 WHERE        [CurrentVacancyStatusListViewModel].VacancyId = Vacancy.id), 0) AS CandidateCount
FROM            Vacancy
GO

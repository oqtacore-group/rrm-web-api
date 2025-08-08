SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW [dbo].[AllVacancyStatusListingViewModel]
AS
SELECT        dbo.VacancyStatusType.id AS StatusId, dbo.VacancyStatusType.Name, dbo.VacancyStatusType.CountSuccess, dbo.VacancyStatusType.OrderId, ISNULL
                             ((SELECT        COUNT(DISTINCT CandidateId) AS Expr1
                                 FROM            dbo.[CurrentVacancyStatusListViewModel]
                                 WHERE        (VacancyStatusId = dbo.VacancyStatusType.id)), 0) AS CandidateCount
FROM            dbo.VacancyStatusType
UNION
(SELECT        0 AS StatusId, 'All' AS Name, Cast(0 as bit) AS CountSuccess, 0 AS OrderId, ISNULL
                              ((SELECT        COUNT(DISTINCT Id) AS Expr1
                                  FROM            dbo.[Candidate]), 0) AS CandidateCount)
GO

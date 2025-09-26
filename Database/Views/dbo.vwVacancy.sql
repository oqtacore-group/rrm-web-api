SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO


CREATE VIEW [dbo].[vwVacancy]
AS 
	SELECT vacancy.[id]
      ,vacancy.[Name]
      ,[ClientId]
	  , client.Name AS ClientName
      ,[WorkplaceNumber]
      ,[Status]
      ,[SalaryLowerEnd]
      ,[SalaryHighEnd]
      ,[SalaryCurrency]
      ,[Experience]
      ,[Location]
      ,[LocalTime]
      ,[RelocationHelp]
      ,[RemoteWorkPlace]
      ,vacancy.[CreatedBy]
      ,[Opened]
      ,[Responsibility]
      ,[Skills]
      ,[PersonalQuality]
      ,[Languages]
      ,[Notes]
	  
	  FROM [dbo].[Vacancy] vacancy INNER JOIN dbo.Client 
	  ON vacancy.ClientId = client.id
GO

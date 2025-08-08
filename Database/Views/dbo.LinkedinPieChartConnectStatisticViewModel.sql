SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE VIEW [dbo].[LinkedinPieChartConnectStatisticViewModel]
AS
SELECT        SuccessConnectionList.AdminId, SuccessConnectionList.ProfileUrl, DATEDIFF(hour, ISNULL(AttemptConnectionList.Date, SuccessConnectionList.Date), SuccessConnectionList.Date) AS ActionDifference, 
                         ISNULL(AttemptConnectionList.Date, SuccessConnectionList.Date) AS Date, SuccessConnectionList.Date AS ActionDate
FROM            (SELECT        Date, ProfileUrl, AdminId
                          FROM            dbo.LinkedinConnectionAction
                          WHERE        (ActionId = 1)) AS SuccessConnectionList LEFT OUTER JOIN
                             (SELECT        Date, ProfileUrl, AdminId
                               FROM            dbo.LinkedinConnectionAction AS LinkedinConnectionAction_1
                               WHERE        (ActionId = 3)) AS AttemptConnectionList ON SuccessConnectionList.ProfileUrl = AttemptConnectionList.ProfileUrl AND SuccessConnectionList.AdminId = AttemptConnectionList.AdminId
GO

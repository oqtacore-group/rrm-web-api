SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE VIEW [dbo].[LinkedinMessagePieViewModel]
AS
SELECT        ISNULL(FirstMessageList.AdminId, 0) AS AdminId, ISNULL(FirstMessageList.ProfileUrl, '') AS ProfileUrl, DATEDIFF(hour, ConnectionList.Date, FirstMessageList.Date) AS ActionDifference, ConnectionList.Date, 
                         FirstMessageList.Date AS ActionDate
FROM            (SELECT        MIN(dbo.LinkedinMessage.Date) AS Date, dbo.LinkedinUser.ProfileUrl, dbo.LinkedinUser.AdminId
                          FROM            dbo.LinkedinMessage LEFT OUTER JOIN
                                                    dbo.LinkedinUser ON dbo.LinkedinMessage.UserId = dbo.LinkedinUser.id AND dbo.LinkedinMessage.ProfileUrl = dbo.LinkedinUser.ProfileUrl
                          GROUP BY dbo.LinkedinUser.ProfileUrl, dbo.LinkedinUser.AdminId) AS FirstMessageList LEFT OUTER JOIN
                             (SELECT        MIN(Date) AS Date, ProfileUrl, AdminId
                               FROM            dbo.LinkedinConnectionAction
                               WHERE        (ActionId = 3) OR
                                                         (ActionId = 1)
                               GROUP BY ProfileUrl, AdminId) AS ConnectionList ON FirstMessageList.ProfileUrl = ConnectionList.ProfileUrl AND FirstMessageList.AdminId = ConnectionList.AdminId
WHERE        (ConnectionList.Date IS NOT NULL)
GO

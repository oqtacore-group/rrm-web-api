SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW [dbo].[Amocrm_Linkedin_UserListViewModel]
AS
SELECT        dbo.LinkedinUser.id AS UserId, dbo.LinkedinUser.Name AS UserName, dbo.LinkedinUser.Date AS Date, dbo.LinkedinUser.ProfileUrl AS LinkedinProfile, dbo.LinkedinAdmin.Name AS AdminName, 
                         ISNULL(dbo.LinkedinUser.Amocrm_added, 0) AS Amocrm_added, ISNULL(dbo.LinkedinUser.Amocrm_Id, 0) AS Amocrm_Id, dbo.LinkedinAdmin.id AS AdminId
FROM            dbo.LinkedinAdmin INNER JOIN
                         dbo.LinkedinUser ON dbo.LinkedinAdmin.id = dbo.LinkedinUser.AdminId
WHERE        (dbo.LinkedinUser.ProfileUrl = ANY
                             (SELECT        ProfileUrl
                               FROM            dbo.LinkedinConnectionAction
                               WHERE        (ActionId = 1)))
GO

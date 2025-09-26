SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW [dbo].[LinkedinMessageViewodel]
AS
SELECT        dbo.LinkedinMessage.*, dbo.LinkedinUser.AdminId
FROM            dbo.LinkedinMessage INNER JOIN
                         dbo.LinkedinUser ON dbo.LinkedinMessage.UserId = dbo.LinkedinUser.id
GO

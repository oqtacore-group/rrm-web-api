SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW [dbo].[LinkedinCurrentConnectionStateViewModel]
AS
SELECT        orig.id, orig.Date, orig.ActionId, orig.ProfileUrl, orig.AdminId, orig.OccupationText, orig.MessageText, orig.LastUpdate
FROM            dbo.LinkedinConnectionAction AS orig LEFT OUTER JOIN
                         dbo.LinkedinConnectionAction AS dup ON orig.ProfileUrl = dup.ProfileUrl AND orig.LastUpdate < dup.LastUpdate
WHERE        (dup.ProfileUrl IS NULL)
GO

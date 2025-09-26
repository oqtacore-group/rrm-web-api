SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
CREATE VIEW [dbo].[LinkedinByAdminStatisticViewModel]
AS
SELECT        ISNULL(invite.Date, ISNULL(success.Date, ISNULL(dialog_data.Date, '2010-10-10'))) AS Date, invite.ConnectInviteCount, success.ConnectSuccessInviteCount, success_replied.ConnectSuccessRepliedCount, 
                         ISNULL(invite.AdminId, dialog_data.AdminId) AS AdminId, dialog_data.ActiveDialogCount, dialog_data.ActiveDialogWithAnswersCount, dialog_data.StartedDialogCount
FROM            (SELECT        CAST(Date AS Date) AS Date, COUNT(DISTINCT ProfileUrl) AS ConnectInviteCount, AdminId
                          FROM            dbo.LinkedinConnectionAction
                          WHERE        (ActionId = 3) OR
                                                    (ActionId = 1) AND (NOT (ProfileUrl = ANY
                                                        (SELECT        ProfileUrl
                                                          FROM            dbo.LinkedinConnectionAction AS LinkedinConnectionAction_4
                                                          WHERE        (ActionId = 3))))
                          GROUP BY CAST(Date AS Date), AdminId) AS invite FULL OUTER JOIN
                             (SELECT        CAST(Date AS Date) AS Date, COUNT(DISTINCT ProfileUrl) AS ConnectSuccessInviteCount, AdminId
                               FROM            dbo.LinkedinConnectionAction AS LinkedinConnectionAction_1
                               WHERE        (ActionId = 3) AND (ProfileUrl = ANY
                                                             (SELECT        ProfileUrl
                                                               FROM            dbo.LinkedinCurrentConnectionStateViewModel
                                                               WHERE        (ActionId = 1))) OR
                                                         (ActionId = 1) AND (NOT (ProfileUrl = ANY
                                                             (SELECT        ProfileUrl
                                                               FROM            dbo.LinkedinConnectionAction AS LinkedinConnectionAction_3
                                                               WHERE        (ActionId = 3)))) AND (ProfileUrl = ANY
                                                             (SELECT        ProfileUrl
                                                               FROM            dbo.LinkedinCurrentConnectionStateViewModel AS LinkedinCurrentConnectionStateViewModel_3
                                                               WHERE        (ActionId = 1)))
                               GROUP BY CAST(Date AS Date), AdminId) AS success ON invite.Date = success.Date AND invite.AdminId = success.AdminId LEFT OUTER JOIN
                             (SELECT        CAST(Date AS Date) AS Date, COUNT(DISTINCT ProfileUrl) AS ConnectSuccessRepliedCount, AdminId
                               FROM            dbo.LinkedinConnectionAction AS LinkedinConnectionAction_1
                               WHERE        (ActionId = 3) AND (ProfileUrl = ANY
                                                             (SELECT        ProfileUrl
                                                               FROM            dbo.LinkedinCurrentConnectionStateViewModel AS LinkedinCurrentConnectionStateViewModel_2
                                                               WHERE        (ActionId = 1))) AND (ProfileUrl = ANY
                                                             (SELECT        ProfileUrl
                                                               FROM            dbo.LinkedinMessage)) OR
                                                         (ActionId = 1) AND (NOT (ProfileUrl = ANY
                                                             (SELECT        ProfileUrl
                                                               FROM            dbo.LinkedinConnectionAction AS LinkedinConnectionAction_2
                                                               WHERE        (ActionId = 3)))) AND (ProfileUrl = ANY
                                                             (SELECT        ProfileUrl
                                                               FROM            dbo.LinkedinCurrentConnectionStateViewModel AS LinkedinCurrentConnectionStateViewModel_1
                                                               WHERE        (ActionId = 1))) AND (ProfileUrl = ANY
                                                             (SELECT        ProfileUrl
                                                               FROM            dbo.LinkedinMessage AS LinkedinMessage_4))
                               GROUP BY CAST(Date AS Date), AdminId) AS success_replied ON invite.Date = success_replied.Date AND invite.AdminId = success_replied.AdminId FULL OUTER JOIN
                             (SELECT        ISNULL(active_dialog.Date, ISNULL(start_dial.Date, active_withdialog.Date)) AS Date, active_dialog.ActiveDialogCount, active_withdialog.ActiveDialogWithAnswersCount, 
                                                         start_dial.StartedDialogCount, active_dialog.AdminId
                               FROM            (SELECT        COUNT(DISTINCT LinkedinMessage_3.UserId) AS ActiveDialogCount, CAST(LinkedinMessage_3.Date AS date) AS Date, dbo.LinkedinUser.AdminId
                                                         FROM            dbo.LinkedinMessage AS LinkedinMessage_3 LEFT OUTER JOIN
                                                                                   dbo.LinkedinUser ON LinkedinMessage_3.UserId = dbo.LinkedinUser.id
                                                         GROUP BY CAST(LinkedinMessage_3.Date AS date), dbo.LinkedinUser.AdminId) AS active_dialog FULL OUTER JOIN
                                                             (SELECT        COUNT(DISTINCT LinkedinMessage_2.UserId) AS StartedDialogCount, CAST(LinkedinMessage_2.Date AS date) AS Date, LinkedinUser_2.AdminId
                                                               FROM            dbo.LinkedinMessage AS LinkedinMessage_2 LEFT OUTER JOIN
                                                                                         dbo.LinkedinUser AS LinkedinUser_2 ON LinkedinMessage_2.UserId = LinkedinUser_2.id
                                                               WHERE        (LinkedinMessage_2.OrderId = 0)
                                                               GROUP BY CAST(LinkedinMessage_2.Date AS date), LinkedinUser_2.AdminId) AS start_dial ON active_dialog.Date = start_dial.Date AND 
                                                         active_dialog.AdminId = start_dial.AdminId FULL OUTER JOIN
                                                             (SELECT        COUNT(DISTINCT LinkedinMessage_1.UserId) AS ActiveDialogWithAnswersCount, CAST(LinkedinMessage_1.Date AS date) AS Date, LinkedinUser_1.AdminId
                                                               FROM            dbo.LinkedinMessage AS LinkedinMessage_1 LEFT OUTER JOIN
                                                                                         dbo.LinkedinUser AS LinkedinUser_1 ON LinkedinMessage_1.UserId = LinkedinUser_1.id
                                                               WHERE        (NOT (LinkedinMessage_1.Name = ANY
                                                                                             (SELECT        Name
                                                                                               FROM            dbo.LinkedinAdmin)))
                                                               GROUP BY CAST(LinkedinMessage_1.Date AS date), LinkedinUser_1.AdminId) AS active_withdialog ON active_dialog.Date = active_withdialog.Date AND 
                                                         active_dialog.AdminId = active_withdialog.AdminId) AS dialog_data ON dialog_data.AdminId = invite.AdminId AND dialog_data.Date = invite.Date
GO

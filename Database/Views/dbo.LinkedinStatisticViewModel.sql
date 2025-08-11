SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
CREATE VIEW [dbo].[LinkedinStatisticViewModel]
AS
SELECT        ISNULL(invite.Date, ISNULL(success.Date, ISNULL(dialog_data.Date, '2010-10-10'))) AS Date, invite.ConnectInviteCount, success.ConnectSuccessInviteCount, success_replied.ConnectSuccessRepliedCount, 
                         dialog_data.ActiveDialogCount, dialog_data.ActiveDialogWithAnswersCount, dialog_data.StartedDialogCount
FROM            (SELECT        CAST(Date AS Date) AS Date, COUNT(DISTINCT ProfileUrl) AS ConnectInviteCount
                          FROM            dbo.LinkedinConnectionAction
                          WHERE        (ActionId = 3) OR
                                                    (ActionId = 1) AND (NOT (ProfileUrl = ANY
                                                        (SELECT        ProfileUrl
                                                          FROM            dbo.LinkedinConnectionAction AS LinkedinConnectionAction_1
                                                          WHERE        (ActionId = 3))))
                          GROUP BY CAST(Date AS Date)) AS invite FULL OUTER JOIN
                             (SELECT        CAST(Date AS Date) AS Date, COUNT(DISTINCT ProfileUrl) AS ConnectSuccessInviteCount
                               FROM            dbo.LinkedinConnectionAction AS LinkedinConnectionAction_1
                               WHERE        (ActionId = 3) AND (ProfileUrl = ANY
                                                             (SELECT        ProfileUrl
                                                               FROM            dbo.LinkedinCurrentConnectionStateViewModel
                                                               WHERE        (ActionId = 1))) OR
                                                         (ActionId = 1) AND (NOT (ProfileUrl = ANY
                                                             (SELECT        ProfileUrl
                                                               FROM            dbo.LinkedinConnectionAction AS LinkedinConnectionAction_2
                                                               WHERE        (ActionId = 3)))) AND (ProfileUrl = ANY
                                                             (SELECT        ProfileUrl
                                                               FROM            dbo.LinkedinCurrentConnectionStateViewModel AS LinkedinCurrentConnectionStateViewModel_3
                                                               WHERE        (ActionId = 1)))
                               GROUP BY CAST(Date AS Date)) AS success ON invite.Date = success.Date LEFT OUTER JOIN
                             (SELECT        CAST(Date AS Date) AS Date, COUNT(DISTINCT ProfileUrl) AS ConnectSuccessRepliedCount
                               FROM            dbo.LinkedinConnectionAction AS LinkedinConnectionAction_1
                               WHERE        (ActionId = 3) AND (ProfileUrl = ANY
                                                             (SELECT        ProfileUrl
                                                               FROM            dbo.LinkedinCurrentConnectionStateViewModel AS LinkedinCurrentConnectionStateViewModel_2
                                                               WHERE        (ActionId = 1))) AND (ProfileUrl = ANY
                                                             (SELECT        ProfileUrl
                                                               FROM            dbo.LinkedinMessage)) OR
                                                         (ActionId = 1) AND (NOT (ProfileUrl = ANY
                                                             (SELECT        ProfileUrl
                                                               FROM            dbo.LinkedinConnectionAction AS LinkedinConnectionAction_3
                                                               WHERE        (ActionId = 3)))) AND (ProfileUrl = ANY
                                                             (SELECT        ProfileUrl
                                                               FROM            dbo.LinkedinCurrentConnectionStateViewModel AS LinkedinCurrentConnectionStateViewModel_1
                                                               WHERE        (ActionId = 1))) AND (ProfileUrl = ANY
                                                             (SELECT        ProfileUrl
                                                               FROM            dbo.LinkedinMessage AS LinkedinMessage_4))
                               GROUP BY CAST(Date AS Date)) AS success_replied ON invite.Date = success_replied.Date FULL OUTER JOIN
                             (SELECT        ISNULL(active_dialog.Date, ISNULL(start_dial.Date, active_withdialog.Date)) AS Date, active_dialog.ActiveDialogCount, active_withdialog.ActiveDialogWithAnswersCount, 
                                                         start_dial.StartedDialogCount
                               FROM            (SELECT        COUNT(DISTINCT LinkedinMessage_3.UserId) AS ActiveDialogCount, CAST(LinkedinMessage_3.Date AS date) AS Date
                                                         FROM            dbo.LinkedinMessage AS LinkedinMessage_3 LEFT OUTER JOIN
                                                                                   dbo.LinkedinUser ON LinkedinMessage_3.UserId = dbo.LinkedinUser.id
                                                         GROUP BY CAST(LinkedinMessage_3.Date AS date)) AS active_dialog FULL OUTER JOIN
                                                             (SELECT        COUNT(DISTINCT LinkedinMessage_2.UserId) AS StartedDialogCount, CAST(LinkedinMessage_2.Date AS date) AS Date
                                                               FROM            dbo.LinkedinMessage AS LinkedinMessage_2 LEFT OUTER JOIN
                                                                                         dbo.LinkedinUser AS LinkedinUser_2 ON LinkedinMessage_2.UserId = LinkedinUser_2.id
                                                               WHERE        (LinkedinMessage_2.OrderId = 0)
                                                               GROUP BY CAST(LinkedinMessage_2.Date AS date)) AS start_dial ON active_dialog.Date = start_dial.Date FULL OUTER JOIN
                                                             (SELECT        COUNT(DISTINCT LinkedinMessage_1.UserId) AS ActiveDialogWithAnswersCount, CAST(LinkedinMessage_1.Date AS date) AS Date
                                                               FROM            dbo.LinkedinMessage AS LinkedinMessage_1 LEFT OUTER JOIN
                                                                                         dbo.LinkedinUser AS LinkedinUser_1 ON LinkedinMessage_1.UserId = LinkedinUser_1.id
                                                               WHERE        (NOT (LinkedinMessage_1.Name = ANY
                                                                                             (SELECT        Name
                                                                                               FROM            dbo.LinkedinAdmin)))
                                                               GROUP BY CAST(LinkedinMessage_1.Date AS date)) AS active_withdialog ON active_dialog.Date = active_withdialog.Date) AS dialog_data ON dialog_data.Date = invite.Date
GO

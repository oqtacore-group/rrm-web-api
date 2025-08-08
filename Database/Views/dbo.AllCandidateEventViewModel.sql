SET QUOTED_IDENTIFIER ON
GO
SET ANSI_NULLS ON
GO


CREATE VIEW [dbo].[AllCandidateEventViewModel]
AS
	SELECT        [id], [starttime] AS Date,
								 (SELECT        TOP 1 id
								   FROM            Candidate
								   WHERE        Email = user_email) AS CandidateId, [summary] AS Caption, 0 AS [TypeId], ISNULL([location], '') AS Location, 'google_calendar' AS EventType, Completed, ISNULL([location], '') AS ZoomLink, 
									 (SELECT        TOP 1 [Name]
								   FROM            Candidate
								   WHERE        Email = user_email) AS CandidateName 
	FROM            [dbo].[GoogleCalendarEvent]
	WHERE        user_email = ANY
								 (SELECT        Email
								   FROM            Candidate)
	UNION
	SELECT        candidateEvent.[id], candidateEvent.[Date], candidateEvent.[CandidateId], candidateEvent.[Caption], candidateEvent.[TypeId], '' AS Location, 'rrm' AS EventType, candidateEvent.Completed, candidateEvent.[ZoomLink], candidate.Name AS CandidateName
	FROM           [dbo].[CandidateEvent] candidateEvent INNER JOIN dbo.Candidate candidate ON candidateEvent.CandidateId = candidate.id
	WHERE candidate.IsDeleted IS NULL OR candidate.IsDeleted = 0
GO

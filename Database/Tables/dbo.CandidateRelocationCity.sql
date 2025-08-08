CREATE TABLE [dbo].[CandidateRelocationCity]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[CandidateId] [int] NOT NULL,
[CityName] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CandidateRelocationCity] ADD CONSTRAINT [PK_CandidateRelocationCity] PRIMARY KEY CLUSTERED ([id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CandidateRelocationCity] ADD CONSTRAINT [IX_CandidateRelocationCity] UNIQUE NONCLUSTERED ([CandidateId], [CityName]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CandidateRelocationCity] ADD CONSTRAINT [fk_candidate_relocation_id] FOREIGN KEY ([CandidateId]) REFERENCES [dbo].[Candidate] ([id]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CandidateRelocationCity] ADD CONSTRAINT [FK_CandidateRelocationCity_Candidate] FOREIGN KEY ([CandidateId]) REFERENCES [dbo].[Candidate] ([id])
GO

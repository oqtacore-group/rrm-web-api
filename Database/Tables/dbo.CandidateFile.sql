CREATE TABLE [dbo].[CandidateFile]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[candidateId] [int] NOT NULL,
[fileName] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[fileUrl] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[DateAdded] [datetime2] (3) NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CandidateFile] ADD CONSTRAINT [PK_CandidateFile] PRIMARY KEY CLUSTERED ([id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CandidateFile] ADD CONSTRAINT [FK_CandidateFile_Candidate] FOREIGN KEY ([candidateId]) REFERENCES [dbo].[Candidate] ([id])
GO

CREATE TABLE [dbo].[HeadHunterData]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[CandidateId] [int] NOT NULL,
[Location] [nvarchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Skills] [nvarchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[About] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Employment] [nvarchar] (25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Relocation] [nvarchar] (25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Position] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[totalExperience] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CreatedBy] [int] NOT NULL,
[ProfileUrl] [nvarchar] (250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[HeadHunterData] ADD CONSTRAINT [PK_HeadHunterData] PRIMARY KEY CLUSTERED ([id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[HeadHunterData] ADD CONSTRAINT [fk_headhunter_data_id] FOREIGN KEY ([CandidateId]) REFERENCES [dbo].[Candidate] ([id]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[HeadHunterData] ADD CONSTRAINT [FK_HeadHunterData_Admin] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[Admin] ([id])
GO
ALTER TABLE [dbo].[HeadHunterData] ADD CONSTRAINT [FK_HeadHunterData_Candidate] FOREIGN KEY ([CandidateId]) REFERENCES [dbo].[Candidate] ([id])
GO

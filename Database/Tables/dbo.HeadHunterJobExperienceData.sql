CREATE TABLE [dbo].[HeadHunterJobExperienceData]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[HhId] [int] NOT NULL,
[startData] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[endData] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[companyName] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[position] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[description] [nvarchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[companyAreaTitle] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[startDate] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[endDate] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[HeadHunterJobExperienceData] ADD CONSTRAINT [PK_HeadHunterJobExperienceData] PRIMARY KEY CLUSTERED ([id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[HeadHunterJobExperienceData] ADD CONSTRAINT [fk_headhunter_data_job_exp_id] FOREIGN KEY ([HhId]) REFERENCES [dbo].[HeadHunterData] ([id]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[HeadHunterJobExperienceData] ADD CONSTRAINT [FK_HeadHunterJobExperienceData_HeadHunterData1] FOREIGN KEY ([HhId]) REFERENCES [dbo].[HeadHunterData] ([id])
GO

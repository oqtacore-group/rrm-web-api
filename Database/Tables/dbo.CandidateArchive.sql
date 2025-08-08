CREATE TABLE [dbo].[CandidateArchive]
(
[id] [int] NOT NULL,
[Name] [nvarchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[PhoneNumber] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Email] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[profileUrl] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ContactDataId] [int] NULL,
[Note] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Location] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Sex] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[DateOfBirth] [date] NULL,
[RelocationReady] [bit] NULL,
[RemoteWorkplaceReady] [bit] NULL,
[SalaryWish] [int] NULL,
[SalaryCurrency] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PhotoUrl] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[MainSkill] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Leader] [bit] NULL,
[HowKnowAboutVacancy] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CreatedBy] [int] NULL,
[HHurl] [nvarchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[lastjob_position] [nvarchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[lastjob_company] [nvarchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[salary] [nvarchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[resume_text] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[favorite] [bit] NULL,
[ActionDate] [datetime2] (3) NOT NULL,
[ActionType] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ActionBy] [int] NOT NULL,
[ActionId] [int] NOT NULL IDENTITY(1, 1)
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CandidateArchive] ADD CONSTRAINT [PK_CandidateArchive] PRIMARY KEY CLUSTERED ([ActionId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CandidateArchive] ADD CONSTRAINT [FK_CandidateArchive_Admin] FOREIGN KEY ([ActionBy]) REFERENCES [dbo].[Admin] ([id])
GO
ALTER TABLE [dbo].[CandidateArchive] ADD CONSTRAINT [FK_CandidateArchive_Admin1] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[Admin] ([id])
GO

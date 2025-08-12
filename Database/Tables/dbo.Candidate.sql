CREATE TABLE [dbo].[Candidate]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PhoneNumber] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Email] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[profileUrl] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ContactDataId] [int] NOT NULL,
[Note] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Location] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Sex] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[DateOfBirth] [date] NULL,
[RelocationReady] [bit] NULL,
[RemoteWorkplaceReady] [bit] NULL,
[SalaryWish] [int] NULL,
[SalaryCurrency] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PhotoUrl] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[MainSkill] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Leader] [bit] NULL,
[HowKnowAboutVacancy] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[CreatedBy] [int] NULL,
[HHurl] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[lastjob_position] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[lastjob_company] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[salary] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[resume_text] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[favorite] [bit] NULL,
[IsDeleted] [bit] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Candidate] ADD CONSTRAINT [PK_Candidate] PRIMARY KEY CLUSTERED ([id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Candidate] ADD CONSTRAINT [FK_Candidate_Admin] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[Admin] ([id])
GO
ALTER TABLE [dbo].[Candidate] ADD CONSTRAINT [FK_Candidate_Currency] FOREIGN KEY ([SalaryCurrency]) REFERENCES [dbo].[Currency] ([CurrencyName])
GO

CREATE TABLE [dbo].[VacancyArchive]
(
[id] [int] NOT NULL,
[Name] [nvarchar] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ClientId] [int] NOT NULL,
[WorkplaceNumber] [int] NOT NULL,
[Status] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[SalaryLowerEnd] [int] NULL,
[SalaryHighEnd] [int] NULL,
[SalaryCurrency] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Experience] [decimal] (4, 2) NULL,
[Location] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LocalTime] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[RelocationHelp] [bit] NULL,
[RemoteWorkPlace] [bit] NULL,
[CreatedBy] [int] NOT NULL,
[Opened] [bit] NOT NULL,
[Responsibility] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Skills] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PersonalQuality] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Languages] [nvarchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Notes] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ActionDate] [datetime2] (3) NOT NULL,
[ActionType] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ActionBy] [int] NOT NULL,
[ActionId] [int] NOT NULL IDENTITY(1, 1)
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[VacancyArchive] ADD CONSTRAINT [PK_VacancyArchive] PRIMARY KEY CLUSTERED ([ActionId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[VacancyArchive] ADD CONSTRAINT [FK_VacancyArchive_Admin] FOREIGN KEY ([ActionBy]) REFERENCES [dbo].[Admin] ([id])
GO
ALTER TABLE [dbo].[VacancyArchive] ADD CONSTRAINT [FK_VacancyArchive_Admin1] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[Admin] ([id])
GO

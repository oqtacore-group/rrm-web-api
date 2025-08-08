CREATE TABLE [dbo].[Vacancy]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ClientId] [int] NOT NULL,
[WorkplaceNumber] [int] NOT NULL,
[Status] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[SalaryLowerEnd] [int] NULL,
[SalaryHighEnd] [int] NULL,
[SalaryCurrency] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Experience] [decimal] (4, 2) NULL,
[Location] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LocalTime] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[RelocationHelp] [bit] NULL,
[RemoteWorkPlace] [bit] NULL,
[CreatedBy] [int] NOT NULL,
[Opened] [bit] NOT NULL,
[Responsibility] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Skills] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PersonalQuality] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Languages] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[Notes] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[IsDeleted] [bit] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Vacancy] ADD CONSTRAINT [PK_Vacancy] PRIMARY KEY CLUSTERED ([id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Vacancy] ADD CONSTRAINT [FK_Vacancy_Admin] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[Admin] ([id])
GO
ALTER TABLE [dbo].[Vacancy] ADD CONSTRAINT [FK_Vacancy_Client] FOREIGN KEY ([ClientId]) REFERENCES [dbo].[Client] ([id])
GO
ALTER TABLE [dbo].[Vacancy] ADD CONSTRAINT [FK_Vacancy_Currency] FOREIGN KEY ([SalaryCurrency]) REFERENCES [dbo].[Currency] ([CurrencyName])
GO

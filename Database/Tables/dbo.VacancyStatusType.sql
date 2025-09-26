CREATE TABLE [dbo].[VacancyStatusType]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[CountSuccess] [bit] NOT NULL,
[CountFail] [bit] NOT NULL,
[OrderId] [int] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[VacancyStatusType] ADD CONSTRAINT [PK_VacancyStatusType] PRIMARY KEY CLUSTERED ([id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[VacancyStatusType] ADD CONSTRAINT [IX_VacancyStatusType] UNIQUE NONCLUSTERED ([Name]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[VacancyStatusType] ADD CONSTRAINT [IX_VacancyStatusType_1] UNIQUE NONCLUSTERED ([OrderId]) ON [PRIMARY]
GO

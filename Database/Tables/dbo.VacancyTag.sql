CREATE TABLE [dbo].[VacancyTag]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[VacancyTag] ADD CONSTRAINT [PK_VacancyTag] PRIMARY KEY CLUSTERED ([id]) ON [PRIMARY]
GO

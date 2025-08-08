CREATE TABLE [dbo].[AmocrmFilter]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Active] [bit] NOT NULL,
[AmocrmName] [nvarchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[AmocrmTagId] [nvarchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[AmocrmUrlTag] [nvarchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[AmocrmUrlTagId] [nvarchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[FilterPhrase] [nvarchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PipelineId] [nvarchar] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AmocrmFilter] ADD CONSTRAINT [PK_AmocrmFilter] PRIMARY KEY CLUSTERED ([id]) ON [PRIMARY]
GO

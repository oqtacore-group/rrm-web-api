CREATE TABLE [dbo].[AmocrmFilterUserConnection]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[AmocrmId] [int] NOT NULL,
[FilterId] [int] NOT NULL,
[Date] [datetime2] (3) NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AmocrmFilterUserConnection] ADD CONSTRAINT [PK_AmocrmFilterUserConnection] PRIMARY KEY CLUSTERED ([id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AmocrmFilterUserConnection] ADD CONSTRAINT [IX_AmocrmFilterUserConnection] UNIQUE NONCLUSTERED ([FilterId], [AmocrmId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AmocrmFilterUserConnection] ADD CONSTRAINT [FK_AmocrmFilterUserConnection_AmocrmFilter] FOREIGN KEY ([FilterId]) REFERENCES [dbo].[AmocrmFilter] ([id])
GO

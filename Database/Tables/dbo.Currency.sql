CREATE TABLE [dbo].[Currency]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[CurrencyName] [nvarchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Currency] ADD CONSTRAINT [PK_Currency] PRIMARY KEY CLUSTERED ([id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Currency] ADD CONSTRAINT [IX_Currency] UNIQUE NONCLUSTERED ([CurrencyName]) ON [PRIMARY]
GO

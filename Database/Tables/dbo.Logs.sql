CREATE TABLE [dbo].[Logs]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[Date] [datetime2] (3) NOT NULL,
[Text] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Logs] ADD CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED ([id]) ON [PRIMARY]
GO

CREATE TABLE [dbo].[Admin]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[Name] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Description] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[AdminState] [bit] NOT NULL,
[AuthId] [nvarchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Email] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[ServiceAccount] [bit] NOT NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Admin] ADD CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED ([id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Admin] ADD CONSTRAINT [IX_Admin] UNIQUE NONCLUSTERED ([AuthId]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Admin] ADD CONSTRAINT [IX_Admin_1] UNIQUE NONCLUSTERED ([Email]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Admin] ADD CONSTRAINT [IX_Admin_2] UNIQUE NONCLUSTERED ([Name]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Admin] ADD CONSTRAINT [FK_Admin_AspNetUsers] FOREIGN KEY ([AuthId]) REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Admin] ADD CONSTRAINT [FK_Admin_AspNetUsers1] FOREIGN KEY ([Email]) REFERENCES [dbo].[AspNetUsers] ([Email])
GO

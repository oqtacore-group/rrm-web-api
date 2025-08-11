CREATE TABLE [dbo].[AspNetUsers]
(
[Id] [nvarchar] (128) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[Email] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[EmailConfirmed] [bit] NOT NULL,
[PasswordHash] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[SecurityStamp] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PhoneNumber] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[PhoneNumberConfirmed] [bit] NOT NULL,
[TwoFactorEnabled] [bit] NOT NULL,
[LockoutEndDateUtc] [datetime2] (3) NULL,
[LockoutEnabled] [bit] NOT NULL,
[AccessFailedCount] [int] NOT NULL,
[UserName] [nvarchar] (256) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
[AdminRight] [bit] NULL,
[NormalizedUserName] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[NormalizedEmail] [nvarchar] (max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[ConcurrencyStamp] [nvarchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[LockoutEnd] [datetimeoffset] NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUsers] ADD CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED ([Id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AspNetUsers] ADD CONSTRAINT [IX_AspNetUsers] UNIQUE NONCLUSTERED ([Email]) ON [PRIMARY]
GO

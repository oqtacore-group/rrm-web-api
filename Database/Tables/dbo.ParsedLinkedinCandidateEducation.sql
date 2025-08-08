CREATE TABLE [dbo].[ParsedLinkedinCandidateEducation]
(
[id] [int] NOT NULL IDENTITY(1, 1),
[ParsedId] [int] NOT NULL,
[YearStart] [int] NULL,
[YearEnd] [int] NULL,
[schoolName] [nvarchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[fieldOfStudy] [nvarchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[degreeName] [nvarchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
[grade] [nvarchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ParsedLinkedinCandidateEducation] ADD CONSTRAINT [PK_ParsedLinkedinCandidateEducation] PRIMARY KEY CLUSTERED ([id]) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ParsedLinkedinCandidateEducation] ADD CONSTRAINT [fk_parsed_linkedin_id] FOREIGN KEY ([ParsedId]) REFERENCES [dbo].[ParsedLinkedinCandidateData] ([id]) ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ParsedLinkedinCandidateEducation] ADD CONSTRAINT [FK_ParsedLinkedinCandidateEducation_ParsedLinkedinCandidateData] FOREIGN KEY ([ParsedId]) REFERENCES [dbo].[ParsedLinkedinCandidateData] ([id])
GO

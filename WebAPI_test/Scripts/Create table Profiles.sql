USE [WebAPI_test]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE Profiles(
	[ProfileID] [uniqueidentifier] NOT NULL CONSTRAINT [df_Profiles_ProfileID]  DEFAULT (newsequentialid()) ,
	[Name] [nvarchar](255) NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [df_Profile_IsActive]  DEFAULT ((1)),

 CONSTRAINT [pk_Profiles] PRIMARY KEY CLUSTERED 
(
	[ProfileID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]
GO






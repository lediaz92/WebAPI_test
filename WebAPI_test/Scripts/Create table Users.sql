USE [WebAPI_test]
GO

/****** Object:  Table [dbo].[Request]    Script Date: 29/8/2020 11:32:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE Users(
	[UserID] [uniqueidentifier] NOT NULL CONSTRAINT [df_Users_UserID]  DEFAULT (newsequentialid()),
	[User] [nvarchar](255) NULL,
	[Password] [nvarchar](255) NULL,
	[ProfileID] [uniqueidentifier] NOT NULL FOREIGN KEY(ProfileID) REFERENCES Profiles(ProfileID),
	[IsActive] [bit] NOT NULL CONSTRAINT [df_Users_IsActive]  DEFAULT ((1)),

 CONSTRAINT [pk_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]





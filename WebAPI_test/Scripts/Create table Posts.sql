USE [WebAPI_test]
GO

/****** Object:  Table [dbo].[Request]    Script Date: 29/8/2020 11:32:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE Posts(
	[PostID] [uniqueidentifier] NOT NULL CONSTRAINT [df_Posts_PostID]  DEFAULT (newsequentialid()),
	[Title] [nvarchar](255) NULL,
	[Text] [nvarchar](255) NULL,
	[Status] [int] NOT NULL CONSTRAINT [df_Posts_Status]  DEFAULT ((0)),
	[PublishedDate] [datetime] NULL,
	[IsActive] [bit] NOT NULL CONSTRAINT [df_Posts_IsActive]  DEFAULT ((1)),
	[CreatedByID] [uniqueidentifier] NOT NULL FOREIGN KEY([CreatedByID]) REFERENCES Users(UserID),
	[CreatedDate] [datetime] NOT NULL CONSTRAINT [df_Posts_CreatedDate]  DEFAULT (getdate()) ,
	[UpdatedByID] [uniqueidentifier] NOT NULL FOREIGN KEY([UpdatedByID]) REFERENCES Users(UserID),
	[UpdatedDate] [datetime] NOT NULL CONSTRAINT [df_Posts_UpdatedDate]  DEFAULT (getdate()),

 CONSTRAINT [pk_Posts] PRIMARY KEY CLUSTERED 
(
	[PostID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]




USE [WebAPI_test]
GO

/****** Object:  Table [dbo].[Request]    Script Date: 29/8/2020 11:32:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE Comments(
	[CommentID] [uniqueidentifier] NOT NULL CONSTRAINT [df_Comments_CommentID]  DEFAULT (newsequentialid()),
	[Comment] [nvarchar](500) NULL,
	[UserName] [nvarchar](255) NULL,
	[PostID] [uniqueidentifier] NOT NULL FOREIGN KEY(PostID) REFERENCES Posts(PostID),
	[IsActive] [bit] NOT NULL CONSTRAINT [df_Comments_IsActive]  DEFAULT ((1)),
	[CreatedDate] [datetime] NOT NULL CONSTRAINT [df_Comments_CreatedDate]  DEFAULT (getdate()) 

 CONSTRAINT [pk_Comments] PRIMARY KEY CLUSTERED 
(
	[CommentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

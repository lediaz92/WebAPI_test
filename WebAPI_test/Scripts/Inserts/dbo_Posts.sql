﻿--
-- Script was generated by Devart dbForge Data Pump for SQL Server, Version 1.5.89.0
-- Product home page: http://www.devart.com/dbforge/sql/data-pump
-- Script date 1/9/2020 15:11:52
-- Server version: 15.00.2070
--

SET DATEFORMAT ymd
SET ARITHABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER, ANSI_NULLS, NOCOUNT ON
SET NUMERIC_ROUNDABORT, IMPLICIT_TRANSACTIONS, XACT_ABORT OFF
GO

INSERT WebAPI_test.dbo.Posts(PostID, Title, Text, Status, PublishedDate, IsActive, CreatedByID, CreatedDate, UpdatedByID, UpdatedDate) VALUES ('5a63656e-18ea-ea11-bbc0-74d435ee802a', N'Titulo1', N'Texto1', 1, NULL, CONVERT(bit, 'True'), 'c389d563-17ea-ea11-bbc0-74d435ee802a', '2020-08-29 13:55:25.047', 'c389d563-17ea-ea11-bbc0-74d435ee802a', '2020-08-29 13:55:25.047')
INSERT WebAPI_test.dbo.Posts(PostID, Title, Text, Status, PublishedDate, IsActive, CreatedByID, CreatedDate, UpdatedByID, UpdatedDate) VALUES ('5b63656e-18ea-ea11-bbc0-74d435ee802a', N'Titulo2', N'Texto2', 2, NULL, CONVERT(bit, 'True'), 'c389d563-17ea-ea11-bbc0-74d435ee802a', '2020-08-29 13:55:25.047', '1915c3a2-17ea-ea11-bbc0-74d435ee802a', '2020-08-31 21:45:44.740')
INSERT WebAPI_test.dbo.Posts(PostID, Title, Text, Status, PublishedDate, IsActive, CreatedByID, CreatedDate, UpdatedByID, UpdatedDate) VALUES ('5c63656e-18ea-ea11-bbc0-74d435ee802a', N'Titulo3', N'Texto3', 2, NULL, CONVERT(bit, 'True'), 'c389d563-17ea-ea11-bbc0-74d435ee802a', '2020-08-29 13:55:25.047', '1915c3a2-17ea-ea11-bbc0-74d435ee802a', '2020-08-31 21:45:50.597')
INSERT WebAPI_test.dbo.Posts(PostID, Title, Text, Status, PublishedDate, IsActive, CreatedByID, CreatedDate, UpdatedByID, UpdatedDate) VALUES ('5d63656e-18ea-ea11-bbc0-74d435ee802a', N'Titulo4', N'Texto4', 3, '2020-08-29 13:55:25.047', CONVERT(bit, 'True'), 'c389d563-17ea-ea11-bbc0-74d435ee802a', '2020-08-29 13:55:25.047', '1915c3a2-17ea-ea11-bbc0-74d435ee802a', '2020-08-29 13:55:25.047')
INSERT WebAPI_test.dbo.Posts(PostID, Title, Text, Status, PublishedDate, IsActive, CreatedByID, CreatedDate, UpdatedByID, UpdatedDate) VALUES ('5e63656e-18ea-ea11-bbc0-74d435ee802a', N'Titulo5', N'Texto5', 3, '2020-08-29 13:55:25.047', CONVERT(bit, 'True'), 'c389d563-17ea-ea11-bbc0-74d435ee802a', '2020-08-29 13:55:25.047', '1915c3a2-17ea-ea11-bbc0-74d435ee802a', '2020-08-31 21:28:31.253')
GO
IF OBJECT_ID('dbo.GetPlayer') IS NOT NULL
	DROP PROCEDURE dbo.GetPlayer
GO 

CREATE PROCEDURE dbo.GetPlayer
	@username NVARCHAR(255)
AS

	IF NOT EXISTS(SELECT 1 FROM dbo.Player WHERE Name = @username)
		RETURN 1

	SELECT TOP (1) Id FROM dbo.Player WHERE Name = @username

RETURN 0
GO
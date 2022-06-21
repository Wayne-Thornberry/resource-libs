IF OBJECT_ID('dbo.RegisterPlayer') IS NOT NULL
	DROP PROCEDURE dbo.RegisterPlayer
GO 

CREATE PROCEDURE dbo.RegisterPlayer
	@username NVARCHAR(255)
AS

	IF EXISTS(SELECT 1 FROM dbo.Player WHERE Name = @username)
		RETURN 1

	BEGIN TRAN
	    INSERT INTO dbo.Player
	    (
	        Name,
	        LastSeen
	    )
	    VALUES
	    (   @username,          -- Name - nvarchar(255)
	        SYSDATETIME() -- LastSeen - datetime2(7)
	        )

	COMMIT TRAN
	
	SELECT TOP(1) Id FROM dbo.Player WHERE @username = @username

	RETURN 0
GO
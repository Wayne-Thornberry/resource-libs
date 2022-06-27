IF OBJECT_ID('dbo.GetSaveFile') IS NOT NULL
	DROP PROCEDURE dbo.GetSaveFile
GO 

CREATE PROCEDURE dbo.GetSaveFile
	@id BIGINT,
	@identity NVARCHAR(255) = NULL,
	@username NVARCHAR(255) = NULL
AS
	DECLARE @playerId BIGINT,
			 @saveId BIGINT

	IF NOT EXISTS(SELECT 1 FROM dbo.SaveFile WHERE [Identity] = @identity)
	BEGIN
		IF NOT EXISTS(SELECT 1 FROM dbo.Player WHERE Name = @username)
			RETURN 1
		SELECT @playerId = Id FROM dbo.Player WHERE Name = @username 

		SELECT @saveId = Id FROM dbo.[Save] WHERE PlayerId = @playerId
		
		SELECT [Identity], [Value] FROM dbo.SaveFile WHERE SaveId = @saveId  
	END
	ELSE 
	BEGIN
		SELECT [Identity], [Value] FROM dbo.SaveFile WHERE [Identity] = @identity
	END
    
	  
RETURN 0
GO
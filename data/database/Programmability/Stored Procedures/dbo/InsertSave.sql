IF OBJECT_ID('dbo.InsertSave') IS NOT NULL
	DROP PROCEDURE dbo.InsertSave
GO 

CREATE PROCEDURE dbo.InsertSave 
	@playerId BIGINT 
AS

BEGIN TRAN

	IF NOT EXISTS( SELECT 1 FROM dbo.[Save] WHERE PlayerId = @playerId) 
	BEGIN
		INSERT INTO dbo.[Save]
		( 
			[PlayerId]
		)
		VALUES
		( -- SaveID - bigint 
			@playerId
		) 
	END

	SELECT Id FROM dbo.[Save] WHERE PlayerId = @playerId

COMMIT TRAN

RETURN 0
GO
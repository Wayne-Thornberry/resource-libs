IF OBJECT_ID('dbo.InsertSaveFile') IS NOT NULL
	DROP PROCEDURE dbo.InsertSaveFile
GO 

CREATE PROCEDURE dbo.InsertSaveFile
	@data NVARCHAR(MAX),
	@playerId BIGINT
AS

BEGIN TRAN

	IF EXISTS( SELECT 1 FROM dbo.SaveFile WHERE PlayerId = @playerId)
		BEGIN
			UPDATE dbo.SaveFile SET [Value] = @data WHERE PlayerId = @playerId
		END
	ELSE 
		BEGIN
			INSERT INTO dbo.SaveFile
			(
				[Value],
				[PlayerId]
			)
		VALUES
		( -- SaveID - bigint
			@data, -- SaveData - nvarchar(max)
			@playerId
		) 
	END

	SELECT Id FROM dbo.SaveFile WHERE PlayerId = @playerId

COMMIT TRAN

RETURN 0
GO
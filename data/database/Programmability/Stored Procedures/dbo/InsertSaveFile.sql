IF OBJECT_ID('dbo.InsertSaveFile') IS NOT NULL
	DROP PROCEDURE dbo.InsertSaveFile
GO 

CREATE PROCEDURE dbo.InsertSaveFile
	@id NVARCHAR(255),
	@data NVARCHAR(MAX), 
	@playerId BIGINT = NULL
AS

BEGIN TRAN
	DECLARE @saveId BIGINT 

	IF EXISTS( SELECT 1 FROM dbo.[SaveFile] WHERE [Identity] = @id)
		BEGIN
			UPDATE dbo.SaveFile SET [Value] = @data WHERE [Identity] = @id
		END
	ELSE 
	BEGIN
		IF NOT EXISTS( SELECT 1 FROM dbo.[Save] WHERE PlayerId = @playerId)
		BEGIN
			EXEC dbo.InsertSave @playerId = @playerId -- bigint 
		END
		
		SELECT @saveId = Id FROM dbo.[Save] WHERE [PlayerId] = @playerId
	
		INSERT INTO dbo.SaveFile
			(
				[Identity],
				[Value],
				[SaveId]
			)
		VALUES
		( -- SaveID - bigint
		    @id,
			@data, -- SaveData - nvarchar(max)
			@saveId
		) 
	END

	SELECT TOP(1) [Identity], [SaveId] FROM dbo.SaveFile WHERE [Identity] = @id

COMMIT TRAN

RETURN 0
GO
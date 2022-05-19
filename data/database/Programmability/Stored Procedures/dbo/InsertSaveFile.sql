IF OBJECT_ID('dbo.InsertSaveFile') IS NOT NULL
	DROP PROCEDURE dbo.InsertSaveFile
GO 

CREATE PROCEDURE dbo.InsertSaveFile
	@data NVARCHAR(MAX) 
AS

BEGIN TRAN

INSERT INTO dbo.SaveFile
(
    Value
)
VALUES
( -- SaveID - bigint
    @data -- SaveData - nvarchar(max)
    ) 

COMMIT TRAN

RETURN 0
GO
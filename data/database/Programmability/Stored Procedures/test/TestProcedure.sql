IF OBJECT_ID('dbo.TestProcedure') IS NOT NULL
	DROP PROCEDURE dbo.TestProcedure
GO 

CREATE PROCEDURE dbo.TestProcedure
	@TestInput NVARCHAR(255)
AS

SELECT 1 

GO;
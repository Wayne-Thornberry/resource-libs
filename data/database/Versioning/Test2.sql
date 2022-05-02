
IF OBJECT_ID('dbo.Test2') IS NOT NULL
	DROP TABLE dbo.Test2

create table Test2(
    TestId BIGINT primary key,
    TestName NVARCHAR(255)
);

IF OBJECT_ID('dbo.Test3') IS NOT NULL
	DROP TABLE dbo.Test3

create table Test3(
    TestId BIGINT primary key,
    TestName NVARCHAR(255)
);

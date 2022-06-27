USE OnlineGame;
GO
 
CREATE TABLE dbo.[Player] (
	[Id] BIGINT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(255) NOT NULL,
	[LastSeen] DATETIME2 NOT NULL,
)

CREATE TABLE dbo.[Instance](
	[Id] BIGINT PRIMARY KEY IDENTITY,  
)

CREATE TABLE dbo.[InstanceSetting](
	[SettingName] NVARCHAR(255) PRIMARY KEY,
	[SettingValue] NVARCHAR(255) NOT NULL, 
	[InstanceId] BIGINT NOT NULL,
	FOREIGN KEY ([InstanceId]) REFERENCES dbo.[Instance](Id)
)

-- Identity has one identity type 1-1
CREATE TABLE dbo.[IdentityType] (
	[Id] INT PRIMARY KEY IDENTITY,
	[Type] INT NOT NULL,
	[Description] NVARCHAR(255)
)

-- Players have multiple identities 1-*
CREATE TABLE dbo.[Identity] (
	[Value] NVARCHAR(255) PRIMARY KEY,
	[Type] INT NOT NULL,
	[PlayerId] BIGINT NOT NULL,
	FOREIGN KEY ([Type]) REFERENCES dbo.[IdentityType](Id),
	FOREIGN KEY ([PlayerId]) REFERENCES dbo.[Player](Id)
)

-- All players have a save. 1 to 1
CREATE TABLE dbo.[Save] (
	[Id] BIGINT PRIMARY KEY IDENTITY, 
	[PlayerId] BIGINT NOT NULL,
	FOREIGN KEY ([PlayerId]) REFERENCES dbo.[Player](Id)
)

-- A save has multiple save files. 1 to *
CREATE TABLE dbo.[SaveFile] (
	[Id] BIGINT PRIMARY KEY IDENTITY,
	[Identity] NVARCHAR(255),
	[Value] NVARCHAR(MAX) NOT NULL, 
	[SaveId] BIGINT NOT NULL,
	FOREIGN KEY ([SaveId]) REFERENCES dbo.[Save](Id)
)

-- Table in which contains players who are banned from the instance. Players here will not be allowed on the server unless DISABLE_BLACKLIST = true
CREATE TABLE dbo.[Deny](
	[Id] BIGINT PRIMARY KEY IDENTITY,
	[DenyFrom] DATETIME2 NOT NULL,
	[DenyTo] DATETIME2 NOT NULL,
	[Reason] NVARCHAR(MAX),
	[PlayerId] BIGINT NOT NULL,
	FOREIGN KEY ([PlayerId]) REFERENCES dbo.[Player](Id)
)

-- Table in which contains players who are allowed on the instance, a whitelist effecitvly. Only works if the sys parameter ENABLE_WHITELIST = true
CREATE TABLE dbo.[Allow](
	[Id] BIGINT PRIMARY KEY IDENTITY,
	[AllowedWhen] DATETIME2 NOT NULL,  
	[PlayerId] BIGINT NOT NULL,
	FOREIGN KEY ([PlayerId]) REFERENCES dbo.[Player](Id)
) 
 
-- Served to the server, for use of server usage
CREATE TABLE dbo.[SystemParameter]( 
	[Id] BIGINT PRIMARY KEY IDENTITY,
	[Key] NVARCHAR(MAX) NOT NULL,
	[Value] NVARCHAR(MAX) NOT NULL,
)

-- Served to the client for use of client modules
CREATE TABLE dbo.[Tunable]( 
	[Key] NVARCHAR(255) PRIMARY KEY,
	[Value] NVARCHAR(MAX) NOT NULL,
)
 
CREATE TABLE dbo.[LogType] (
	[Id] INT PRIMARY KEY IDENTITY,
	[Type] INT NOT NULL,
	[Description] NVARCHAR(255)
)

CREATE TABLE dbo.[Log]( 
	[Id] BIGINT PRIMARY KEY IDENTITY,
	[TimeStamp] DATETIME2 NOT NULL,  
	[Type] INT NOT NULL,  
	[Value] NVARCHAR(MAX) NOT NULL,
	FOREIGN KEY ([Type]) REFERENCES dbo.[LogType](Id)
)




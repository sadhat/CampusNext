CREATE TABLE [dbo].[Profile]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FacebookName] NVARCHAR(50) NOT NULL, 
    [CampusName] NVARCHAR(50) NULL, 
    [Status] INT NULL DEFAULT 1, 
    [Email] NVARCHAR(100) NULL 
)

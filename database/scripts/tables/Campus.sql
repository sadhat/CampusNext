CREATE TABLE [dbo].[Campus]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [CampusCode] NVARCHAR(50) NOT NULL, 
    [LogoUrl] VARCHAR(100) NOT NULL, 
    [Rank] INT NULL 
)

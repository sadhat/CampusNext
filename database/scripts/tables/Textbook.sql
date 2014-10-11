CREATE TABLE [dbo].[Textbook]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(250) NOT NULL, 
    [ISBN] NVARCHAR(20) NULL, 
    [Description] NVARCHAR(2000) NOT NULL, 
	[Authors] NVARCHAR(150) NULL,
	[Course] NVARCHAR(100) NULL,
    [Price] MONEY NULL, 
    [CurrencyType] CHAR(4) NULL DEFAULT 'us', 
    [UserId] VARCHAR(100) NOT NULL, 
    [CampusCode] NCHAR(10) NULL, 
    [CreatedDate] DATETIME NULL DEFAULT GETDATE(), 
    [ModifiedDate] DATETIME NULL DEFAULT GETDATE(), 
    [LikeCount] INT NOT NULL DEFAULT 0, 
    [ContactInfo] NVARCHAR(100) NULL
)

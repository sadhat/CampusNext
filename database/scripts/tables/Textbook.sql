CREATE TABLE [dbo].[Textbook]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(250) NOT NULL, 
    [ISBN] NVARCHAR(20) NULL, 
    [Description] NVARCHAR(2000) NOT NULL, 
    [Price] MONEY NULL, 
    [CurrencyType] CHAR(4) NOT NULL DEFAULT 'us'
)

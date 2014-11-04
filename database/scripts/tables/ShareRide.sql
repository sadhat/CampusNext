CREATE TABLE [dbo].[ShareRide]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [FromLocation] NVARCHAR(500) NOT NULL, 
    [ToLocation] NVARCHAR(500) NOT NULL, 
    [IsRoundtrip] BIT NOT NULL DEFAULT 0, 
    [StartDateTime] NVARCHAR(300) NOT NULL, 
    [ReturnDateTime] NVARCHAR(300) NULL,
	[UserId] VARCHAR(100) NOT NULL, 
    [CampusCode] NCHAR(10) NULL, 
    [CreatedDate] DATETIME NULL DEFAULT GETDATE(), 
    [ModifiedDate] DATETIME NULL DEFAULT GETDATE(), 
    [LikeCount] INT NOT NULL DEFAULT 0, 
    [AdditionalInfo] NVARCHAR(2000) NULL 
)

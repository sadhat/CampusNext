﻿CREATE TABLE [dbo].[Message]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [MessageInJson] NVARCHAR(MAX) NOT NULL, 
    [SentDateTime] DATETIME NULL
)

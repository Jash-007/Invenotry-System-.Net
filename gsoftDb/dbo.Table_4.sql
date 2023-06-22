CREATE TABLE [dbo].[mstProduct]
(
	[ProductId] INT Identity (1,1) NOT NULL , 
    [ProductName] VARCHAR(128) NOT NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1,
	Primary Key Clustered ([ProductId] Asc)
)

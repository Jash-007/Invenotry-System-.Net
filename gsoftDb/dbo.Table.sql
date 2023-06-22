CREATE TABLE [dbo].[mstBrand]
(
	[BrandId] INT Identity (1,1) NOT NULL, 
    [BrandName] VARCHAR(128) NOT NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1,
	Primary Key Clustered ([BrandId] Asc)
)

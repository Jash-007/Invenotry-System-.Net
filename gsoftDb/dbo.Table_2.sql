CREATE TABLE [dbo].[mstTax]
(
	[TaxId] INT Identity(1,1) NOT NULL , 
    [TaxName] VARCHAR(32) NOT NULL, 
    [SGSTPrc] DECIMAL(18, 4) NOT NULL, 
    [CGSTPrc] DECIMAL(18, 4) NOT NULL, 
    [IGSTPrc] DECIMAL(18, 4) NOT NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1,
	Primary Key Clustered ([TaxId] Asc)
)

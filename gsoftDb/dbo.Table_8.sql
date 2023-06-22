CREATE TABLE [dbo].[trnPurchaseItem]
(
	[PurchaseItemId] INT Identity (1,1) NOT NULL, 
    [Qty] INT NOT NULL, 
    [PurcRate] DECIMAL(18, 4) NOT NULL,
	[Amount] DECIMAL(18, 4) NOT NULL, 
    [SGSTPrc] DECIMAL(18, 4) NULL, 
    [SGSTAmount] DECIMAL(18, 4) NULL, 
    [CGSTPrc] DECIMAL(18, 4) NULL, 
    [CGSTAmount] DECIMAL(18, 4) NULL, 
    [IGSTPrc] DECIMAL(18, 4) NULL, 
    [IGSTAmount] DECIMAL(18, 4) NULL, 
    [TotalAmt] DECIMAL(18, 4) NOT NULL, 
    [MRP] DECIMAL(18, 4) NOT NULL, 
    [SalesRate] DECIMAL(18, 4) NULL, 
    [PurchaseId] INT NULL, 
    [ItemId] INT NULL, 
    [TaxId] INT NULL, 
    Primary Key Clustered ([PurchaseItemId] Asc),
	FOREIGN KEY (PurchaseId) REFERENCES trnPurchase(PurchaseId),
	FOREIGN KEY (ItemId) REFERENCES mstItem(ItemId),
	FOREIGN KEY (TaxId) REFERENCES mstTax(TaxId)
)

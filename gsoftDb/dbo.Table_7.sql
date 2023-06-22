CREATE TABLE [dbo].[mstItem]
(
	[ItemId] INT Identity(1,1) NOT NULL, 
    [ItemName] VARCHAR(128) NOT NULL, 
    [ShortName] VARCHAR(32) NULL, 
    [DiscountPrc] DECIMAL(18, 4) NULL, 
    [DiscountFromDate] DATETIME NULL, 
    [DiscountToDate] DATETIME NULL, 
    [DefaultQty] INT NOT NULL DEFAULT 1, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [ProductID] INT NULL, 
    [DepartmentID] INT NULL, 
    [BrandId] INT NULL, 
    [TaxId] INT NULL,
	Primary key Clustered([ItemId] Asc),
	FOREIGN KEY (ProductId) REFERENCES mstProduct(ProductId),
	FOREIGN KEY (DepartmentId) REFERENCES mstDepartment(DepartmentID),
	FOREIGN KEY (BrandId) REFERENCES mstBrand(BrandId),
	FOREIGN KEY (TaxId) REFERENCES mstTax(TaxId)
)

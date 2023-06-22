CREATE TABLE [dbo].[trnPurchase]
(
	[PurchaseId] INT Identity(1,1) NOT NULL,
	[PurchaseType] VARCHAR(10) NULL, 
    [VoucherNo] VARCHAR(16) NOT NULL, 
    [VoucherDate] DATETIME NOT NULL, 
    [InvoiceType] VARCHAR(10) NOT NULL, 
    [TotalAmount] DECIMAL(18, 4) NOT NULL, 
    [Remarks] VARCHAR(250) NULL, 
    [AccountId] INT NOT NULL, 
    Primary Key Clustered ([PurchaseId] Asc),
	FOREIGN KEY (AccountId) REFERENCES mstAccount(AccountId)
)

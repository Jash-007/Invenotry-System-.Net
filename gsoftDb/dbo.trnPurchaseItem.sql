CREATE TABLE [dbo].[trnPurchaseItem] (
    [PurchaseItemId] INT             IDENTITY (1, 1) NOT NULL,
    [Qty]            INT             NOT NULL,
    [PurcRate]       DECIMAL (18, 4) NOT NULL,
    [Amount]         DECIMAL (18, 4) NOT NULL,
    [SGSTPrc]        DECIMAL (18, 4) NULL,
    [SGSTAmount]     DECIMAL (18, 4) NULL,
    [CGSTPrc]        DECIMAL (18, 4) NULL,
    [CGSTAmount]     DECIMAL (18, 4) NULL,
    [IGSTPrc]        DECIMAL (18, 4) NULL,
    [IGSTAmount]     DECIMAL (18, 4) NULL,
    [TotalAmt]       DECIMAL (18, 4) NOT NULL,
    [MRP]            DECIMAL (18, 4) NOT NULL,
    [SalesRate]      DECIMAL (18, 4) NULL,
    [PurchaseId]     INT             NULL,
    [ItemId]         INT             NULL,
    [TaxId]          INT             NULL,
    PRIMARY KEY CLUSTERED ([PurchaseItemId] ASC),
    FOREIGN KEY ([PurchaseId]) REFERENCES [dbo].[trnPurchase] ([PurchaseId]),
    FOREIGN KEY ([ItemId]) REFERENCES [dbo].[mstItem] ([ItemId]),
    FOREIGN KEY ([TaxId]) REFERENCES [dbo].[mstTax] ([TaxId])
);


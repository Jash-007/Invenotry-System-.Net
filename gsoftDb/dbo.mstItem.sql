CREATE TABLE [dbo].[mstItem] (
    [ItemId]           INT             IDENTITY (1, 1) NOT NULL,
    [ItemName]         VARCHAR (128)   NOT NULL,
    [ShortName]        VARCHAR (32)    NULL,
    [DiscountPrc]      DECIMAL (18, 4) NULL,
    [DiscountFromDate] DATETIME        NULL,
    [DiscountToDate]   DATETIME        NULL,
    [DefaultQty]       INT             DEFAULT ((1)) NOT NULL,
    [IsActive]         BIT             DEFAULT ((1)) NOT NULL,
    [ProductID]        INT             NULL,
    [DepartmentID]     INT             NULL,
    [BrandId]          INT             NULL,
    [TaxId]            INT             NULL,
    PRIMARY KEY CLUSTERED ([ItemId] ASC),
    FOREIGN KEY ([ProductID]) REFERENCES [dbo].[mstProduct] ([ProductId]),
    FOREIGN KEY ([DepartmentID]) REFERENCES [dbo].[mstDepartment] ([DepartmentId]),
    FOREIGN KEY ([BrandId]) REFERENCES [dbo].[mstBrand] ([BrandId]),
    FOREIGN KEY ([TaxId]) REFERENCES [dbo].[mstTax] ([TaxId])
);


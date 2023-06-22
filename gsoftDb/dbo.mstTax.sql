CREATE TABLE [dbo].[mstTax] (
    [TaxId]    INT             IDENTITY (1, 1) NOT NULL,
    [TaxName]  VARCHAR (32)    NOT NULL,
	[TaxPrc]   DECIMAL(18,4) NOT NULL,
    [SGSTPrc]  DECIMAL (18, 4) NOT NULL,
    [CGSTPrc]  DECIMAL (18, 4) NOT NULL,
    [IGSTPrc]  DECIMAL (18, 4) NOT NULL,
    [IsActive] BIT             DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([TaxId] ASC)
);


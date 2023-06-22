CREATE TABLE [dbo].[mstCompany]
(
	[CompanyId] INT identity(1,1) NOT NULL , 
    [CompanyName] VARCHAR(128) NOT NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1, 
    [StateId] INT NOT NULL,
	Primary Key Clustered ([CompanyID] Asc),
	CONSTRAINT FK_CompnayState FOREIGN KEY (StateID)
    REFERENCES mstState(StateId)
)

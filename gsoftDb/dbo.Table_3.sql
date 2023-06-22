CREATE TABLE [dbo].[mstBsgrGroup]
(
	[BsgrId] INT Identity (1,1) NOT NULL , 
    [GroupName] VARCHAR(120) NOT NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1,
	Primary Key Clustered ([BsgrId] Asc)
)

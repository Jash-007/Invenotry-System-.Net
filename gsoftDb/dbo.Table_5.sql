CREATE TABLE [dbo].[mstState]
(
	[StateId] INT Identity(1,1) NOT NULL , 
    [StateName] VARCHAR(128) NOT NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1,
	Primary Key Clustered ([StateId] Asc)
)

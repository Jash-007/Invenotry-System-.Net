CREATE TABLE [dbo].[mstDepartment]
(
	[DepartmentId] INT Identity(1,1) NOT NULL , 
    [DepartmentName] VARCHAR(128) NOT NULL, 
    [IsActive] BIT NOT NULL DEFAULT 1,
	Primary Key Clustered ([DepartmentId] Asc)
)

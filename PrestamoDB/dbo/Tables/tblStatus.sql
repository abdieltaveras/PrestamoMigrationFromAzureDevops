CREATE TABLE [dbo].[tblStatus]
(
	[IdStatus] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [IdTipoStatus] INT NULL,
	[Tipo] VARCHAR(50) NULL,
    [Concepto] VARCHAR(50) NULL, 
    [Estado] INT NULL 
)

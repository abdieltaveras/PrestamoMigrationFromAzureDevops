CREATE TABLE [dbo].[tblColores]
(
	[IdColor]INT NOT NULL PRIMARY KEY  identity(1,1),
	[Nombre] NVARCHAR(50) NOT NULL,
	[IdNegocio] INT NOT NULL,
	[InsertadoPor] varchar(100) not null,
	[FechaInsertado] DateTime not null default getdate(), 
    [ModificadoPor] VARCHAR(100) NULL, 
    [FechaModificado] DATETIME NULL, 
    [AnuladoPor] VARCHAR(100) NULL, 
    [FechaAnulado] DATETIME NULL
)
